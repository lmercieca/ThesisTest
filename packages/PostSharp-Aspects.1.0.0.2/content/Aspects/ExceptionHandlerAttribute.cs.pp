// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlerAttribute.cs" company="PostSharp">
//   2012
// </copyright>
// <summary>
//   Adds Exception handling aspect to method -- Slightly modified from the example on SharpCrafters site
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Aspects
{
    using System;
    using System.Diagnostics;

    using PostSharp.Aspects;
	
	// uncomment * for your logging preference
    // *using NLog;
    // *using log4net;

    /// <summary>
    /// Wraps the methods in a try catch block
    /// </summary>
    [Serializable]
    public class ExceptionHandlerAttribute : OnExceptionAspect
    {
        // *public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        // *public static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnException(MethodExecutionArgs args)
        {
            if (args.Instance != null)
            {
                string messageOut = string.Format(
                    "\n\n ----------{0} had an error ----------\n@ {1}:\n\n {2}\n\nStackTrace:{3}",
                    args.Method.Name,
                    DateTime.Now,
                    args.Exception.Message,
                    args.Exception.StackTrace);

                    if (args.Exception.InnerException != null)
                    {
                        messageOut += string.Format("\n ---------- InnerException --------\n : {0}", args.Exception.InnerException);
                    }

                Trace.WriteLine(messageOut);
            }

            args.FlowBehavior = FlowBehavior.Continue;

            // * Logger.Error(messageOut);
            // throw new Exception("There was an error logged, please contact your administrator");
        }
    }
}
