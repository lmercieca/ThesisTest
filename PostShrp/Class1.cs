using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace PostShrp
{
    public class Class1
    {
        [Serializable]        
        public class LoggingAspect : OnMethodBoundaryAspect
        {
            public override void OnEntry(MethodExecutionArgs args)
            {
                Console.WriteLine("The {0} method has been entered.", args.Method.Name);
            }
        }

    }
}
