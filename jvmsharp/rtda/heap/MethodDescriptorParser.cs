using System;
using System.Collections.Generic;

namespace jvmsharp.rtda.heap
{
    class MethodDescriptor
    {
        internal List<string> parameterTypes=new List<string>();//存储参数类型的数组
        internal string returnType;//返回类型

        public void addParameterType(string t)
        {
            parameterTypes.Add(t);
        }
    }

    class MethodDescriptorParser
    {
        internal string raw;
        internal int offset;
        internal MethodDescriptor parsed;

        public MethodDescriptor parseMethodDescriptor(String descriptor)
        {
            var parser = new MethodDescriptorParser();
            return parser.parse(descriptor);
        }

        private MethodDescriptor parse(string descriptor)
        {
            raw = descriptor;
            parsed = new MethodDescriptor();
            startParams();
            parseParamTypes();
            endParams();
            parseReturnType();
            finish();

            return parsed;
        }

        private void finish()
        {
            if (offset != raw.Length)
            {
                causePanic();
            }
        }

        private void parseReturnType()
        {
            if (readUint8() == 'V')
            {
                parsed.returnType = "V";
                return;
            }
            unreadUint8();
            var t = parseFieldType();
            if (t != "")
            {
                parsed.returnType = t;
                return;
            }
            causePanic();
        }

        private void endParams()
        {
            if (readUint8() != ')')
            {
                causePanic();
            }
        }

        private void parseParamTypes()
        {
            for (;;)
            {
                var t = parseFieldType();
                if (t != "")
                {
                    parsed.addParameterType(t);
                }
                else break;
            }
        }

        private void startParams()
        {
            if (readUint8() != '(')
            {
                causePanic();
            }
        }

        string parseFieldType()
        {
            char ch = readUint8();
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
                    return "";
            }
        }

        void unreadUint8()
        {
            offset--;
        }

        string parseObjectType()//返回例子：Ljava/lang/Object;
        {
            var unread = this.raw.Substring(offset);
            var semicolonIndex = unread.IndexOf(';');//strings.IndexRune(unread, ';')
            if (semicolonIndex == -1)
            {
                causePanic();
                return "";
            }
            else
            {
                var objStart = offset - 1;
                var objEnd = offset + semicolonIndex + 1;
                offset = objEnd;
                string descriptor = raw.Substring(objStart, objEnd - objStart);
                return descriptor;
            }
        }

        string parseArrayType()
        {
            int arrStart = offset - 1;
            parseFieldType();
            int arrEnd = offset;
            var descriptor = this.raw.Substring(arrStart, arrEnd - arrStart);
            return descriptor;
        }

        char readUint8()
        {
            char b = raw[offset];
            this.offset++;
            return b;
        }

        void causePanic()
        {
            throw new Exception("BAD raw: " + raw);
        }
    }
}
