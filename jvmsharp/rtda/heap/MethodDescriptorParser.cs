using System;
using System.Collections.Generic;

namespace jvmsharp.rtda.heap
{
    struct MethodDescriptor
    {
        internal List<string> parameterTypes;//存储参数类型的数组
        internal string returnType;//返回类型
    }

    class MethodDescriptorParser
    {
        internal string descriptor;
        internal int offset = 0;
        internal MethodDescriptor md;

        public MethodDescriptor parseMethodDescriptor(String descriptor)
        {
            this.md = new MethodDescriptor();
            md.parameterTypes = new List<string>();
            this.descriptor = descriptor;   //descriptor的例子：([Ljava/lang/Object;)V
            char ch = Convert.ToChar(readUint8());//ch取自descriptor的第一个char
            if (ch != '(') causePanic();//startParams()，descriptor以(开头则继续
            for (;;) // parseParamTypes();
            {
                string t = parseFieldType();
                if (t != null)
                    md.parameterTypes.Add(t);
                else break;
            }

            char ch2 = Convert.ToChar(readUint8());//endParams();
            if (ch2 != ')')
                causePanic();
            var t2 = parseFieldType();//  parseReturnType();
            if (t2 != null)
                md.returnType = t2;
            else causePanic();
            if (offset != descriptor.Length)// finish();
                causePanic();

            return md;
        }

        string parseFieldType()
        {
            char ch = Convert.ToChar(readUint8());
            switch (ch)
            {
                case 'B':
                case 'C':
                case 'D':
                case 'F':
                case 'I':
                case 'J':
                case 'S':
                case 'Z':
                case 'V': return ch.ToString();//普通类型，返回对应的string
                case 'L':
                    return parseObjectType();
                case '[':
                    return parseArrayType();
                default:
                    unreadUint8();
                    return null;
            }
        }

        void unreadUint8()
        {
            offset--;
        }

        string parseObjectType()//返回例子：Ljava/lang/Object;
        {
            var unread = this.descriptor.Substring(offset);
            var semicolonIndex = unread.IndexOf(';');//strings.IndexRune(unread, ';')
            if (semicolonIndex == -1)
            {
                causePanic();
                return null;
            }
            else
            {
                var objStart = offset - 1;
                var objEnd = offset + semicolonIndex + 1;
                offset = objEnd;
                string newdescriptor = this.descriptor.Substring(objStart, objEnd - objStart);
                return newdescriptor;
            }
        }

        string parseArrayType()
        {
            int arrStart = offset - 1;
            parseFieldType();
            int arrEnd = offset;
            var descriptor = this.descriptor.Substring(arrStart, arrEnd - arrStart);
            return descriptor;
        }

        byte readUint8()
        {
            char b = descriptor[offset];
            this.offset++;
            return Convert.ToByte(b);
        }

        void causePanic()
        {
            throw new Exception("BAD descriptor: " + descriptor);
        }
    }
}
