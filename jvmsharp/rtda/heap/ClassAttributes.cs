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
        string sourceFile;
        string signature;
        byte[] annotationData;// RuntimeVisibleAnnotations_attribute
        EnclosingMethod enclosingMethod;

        EnclosingMethod EnclosingMethod()
        {
            return enclosingMethod;
        }
    }

}
