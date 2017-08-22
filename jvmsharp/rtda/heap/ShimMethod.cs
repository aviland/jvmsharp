using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.rtda.heap
{
    class ShimMethod
    {
        Class _shimClass = new Class("~shim");
        byte[] _returnCode = { 0xb1 }; // return
        byte[] _athrowCode = { 0xbf }; // athrow
        Method _returnMethod;

        public ShimMethod()
        {
            _returnMethod = new Method();
            _returnMethod.accessFlags = AccessFlags.ACC_STATIC;
            _returnMethod.name = "<return>";
            _returnMethod.clas = _shimClass;
            _returnMethod.code = _returnCode;
        }

        internal  Method ShimReturnMethod()
        {
            return _returnMethod;
        }
    }
}
