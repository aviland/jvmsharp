﻿using System;
using cf = jvmsharp.classfile;
using Constant = System.Object;

namespace jvmsharp.rtda.heap
{
    class ConstantPool//运行时常量池
    {
        public Class clas;//存储当前载入的class文件的类
        public Constant[] consts;//存储运行时常量信息

        public ConstantPool() { }

        public ConstantPool( Class clas,Constant[] consts)
        {
            this.clas = clas;
            this.consts = consts;
        }

        public ConstantPool newConstantPool( ref Class clas,ref  classfile.ConstantPool cfCp)//将ConstantInfo转换为ConstantPool
        {
            int cpCount = cfCp.constantInfos.Length;
            ConstantPool rtcp = new ConstantPool( clas, new Constant[cpCount]);//初始化运行时常量池
            for (int i = 1; i < cpCount; i++)
            {
                cf.ConstantInfo cpInfo = cfCp.constantInfos[i];
    //   Console.WriteLine("ConstantInfo:"+cpInfo.GetType().Name);
                switch (cpInfo.GetType().Name)
                {
                    case "ConstantIntegerInfo":
                        rtcp.consts[i] = ((cf.ConstantIntegerInfo)cpInfo).Value();//运行时常量池可以直接存储int
                        break;
                    case "ConstantFloatInfo":
                        rtcp.consts[i] = ((cf.ConstantFloatInfo)cpInfo).Value();//运行时常量池可以直接存储float
                        break;
                    case "ConstantLongInfo":
                        rtcp.consts[i] = ((cf.ConstantLongInfo)cpInfo).Value();//运行时常量池可以直接存储long
                        i++;
                        break;
                    case "ConstantDoubleInfo":
                        rtcp.consts[i] = ((cf.ConstantDoubleInfo)cpInfo).Value();//运行时常量池可以直接存储double
                        i++;
                        break;
                    case "ConstantStringInfo":
                        rtcp.consts[i] = ((cf.ConstantStringInfo)cpInfo).String();//运行时常量池可以直接存储string
                        break;
                    case "ConstantClassInfo":
                        rtcp.consts[i] = new ClassRef().newClassRef(ref rtcp, (cf.ConstantClassInfo)cpInfo);//运行时常量池可以存储类符号引用
                        break;
                    case "ConstantFieldrefInfo":
                        cf.ConstantFieldrefInfo fieldrefInfo = (cf.ConstantFieldrefInfo)cpInfo;
                        rtcp.consts[i] = new FieldRef().newConstantFieldRef(ref rtcp,ref fieldrefInfo);//运行时常量池可以存储字段符号引用
                        break;
                    case "ConstantMethodrefInfo":
                        rtcp.consts[i] = new MethodRef().newMethodRef(ref rtcp, (cf.ConstantMethodrefInfo)cpInfo);
                        break;
                    case "ConstantInterfaceMethodrefInfo":
                        cf.ConstantInterfaceMethodrefInfo methodrefInfo = (classfile.ConstantInterfaceMethodrefInfo)cpInfo;
                        rtcp.consts[i] = new InterfaceMethodref().newInterfaceMethodref(ref rtcp, methodrefInfo);
                        break;
                    default: //throw new Exception("Unknown:"+ cpInfo.GetType().Name); 
                        break;
                }
            }
            return rtcp;
        }

        public Constant GetConstant(uint index)
        {
            // todo
            Constant c = consts[index];
            if (c != null)
                return c;
            throw new Exception("No constants at index " + index);
        }
    }
}
