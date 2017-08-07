namespace jvmsharp.rtda.heap
{
    struct EnclosingMethod
    {
      //  string className;
 //       string methodName;
   //     string methodDescriptor;
    }

  abstract  class ClassAttributes:AccessFlags
    {
     internal   string sourceFile;
        internal string signature;
        internal byte[] annotationData;// RuntimeVisibleAnnotations_attribute
        EnclosingMethod enclosingMethod;

        EnclosingMethod EnclosingMethod()
        {
            return enclosingMethod;
        }
    }

}
