// -----------------------------------------------------------------------
// <copyright file="TraceAttribute.cs" company="PostSharp">
// 2012
// </copyright>
// <summary> 
// Help file location: http://doc.sharpcrafters.com/postsharp-2.1/Default.aspx##PostSharp-2.1.chm/html/b3b73c15-a14c-470e-a740-c0d916676d03.htm
// ***Warning *** like any profiling, this might increase overhead
// </summary>
// -----------------------------------------------------------------------

namespace $rootnamespace$.Aspects
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using PostSharp.Aspects;

    // uncomment * for your logging preference
    // *using NLog;
    // *using log4net;

    /// <summary>
    /// This code implements an aspect that writes a trace message before and after the execution of the methods to which the aspect is applied.
    /// The example demonstrates the use of OnMethodBoundaryAspect, and shows how to use RuntimeInitialize(MethodBase) to improve runtime performance.
    /// </summary>
    [Serializable]
    public class TraceAttribute : OnMethodBoundaryAspect
    {
        // *public static readonly Logger Logger = LogManager.GetCurrentClassLogger(); 
        // *private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // This field is initialized and serialized at build time, then deserialized at runtime.
        private readonly string category;

        // These fields are initialized at runtime. They do not need to be serialized.
        [NonSerialized]
        private string enteringMessage;
        [NonSerialized]
        private string exitingMessage;

        // Default constructor, invoked at build time.
        public TraceAttribute()
        {
        }

        // Constructor specifying the tracing category, invoked at build time.
        public TraceAttribute(string category)
        {
            this.category = category;
        }

        // Invoked at runtime before that target method is invoked.
        public override void OnEntry(MethodExecutionArgs args)
        {
            Trace.WriteLine(this.enteringMessage, this.category);

            // *this.Logger.Trace(this.enteringMessage, this.category);
        }

        // Invoked at runtime after the target method is invoked (in a finally block).
        public override void OnExit(MethodExecutionArgs args)
        {
            Trace.WriteLine(this.exitingMessage, this.category);

            // *this.Logger.Trace(this.exitingMessage, this.category);
        }

        // Invoked only once at runtime from the static constructor of type declaring the target method.
        public override void RuntimeInitialize(MethodBase method)
        {
            if (method.DeclaringType == null)
            {
                return;
            }

            var methodName = string.Format("{0}{1}", method.DeclaringType.FullName, method.Name);
            this.enteringMessage = string.Format("Trace: Entering {0}", methodName);
            this.exitingMessage = string.Format("Trace: Exiting {0}", methodName);
        }
    }
}