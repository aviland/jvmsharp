namespace jvmsharp.rtda.heap
{
    class ExceptionHandler
    {
        internal int startPc;
        internal int endPc;
        internal int handlerPc;
        internal ClassRef catchType;

        public ExceptionHandler(int startPc, int endPc, int handlerPc, ClassRef catchType)
        {
            this.startPc = startPc;
            this.endPc = endPc;
            this.handlerPc = handlerPc;
            this.catchType = catchType;
        }
    }

    class ExceptionTable
    {
        internal ExceptionHandler[] exceptionTables;

        public ExceptionTable(ref classfile.ExceptionTableEntry[] entries, ref ConstantPool cp)
        {
            exceptionTables = new ExceptionHandler[entries.Length];
            for (int i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                exceptionTables[i] = new ExceptionHandler(entry.startPc, entry.endPc, entry.handlerPc, getCatchType((entry.catchType), cp));
            }
        }

        private ClassRef getCatchType(uint index, ConstantPool cp)
        {
            return index == 0 ? null : (ClassRef)cp.GetConstant(index);
        }

        internal ExceptionHandler findExceptionHandler(Class exClass, int pc)
        {
            foreach (ExceptionHandler handler in exceptionTables)
            {
                if (pc >= handler.startPc && pc < handler.endPc)
                {
                    if (handler.catchType==null)
                        return handler;
                    var catchClass = handler.catchType.ResolvedClass();
                    if (catchClass.Equals(exClass) || catchClass.IsSuperClassOf(exClass))
                        return handler;
                }
            }
            return null;
        }
    }
}
