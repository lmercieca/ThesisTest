// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileAttribute.cs" company="chadit.wordpress.com">
//   2012
// </copyright>
// <summary>
//   Use this aspect to look for slow method exection ***Warning *** like any profiling, this might increase overhead
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
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class ProfileAttribute : OnMethodBoundaryAspect
    {
        // *public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        // *private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.StartNew();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();

            // only display if the method took longer than 2 seconds to execute
            if (sw.Elapsed.TotalMilliseconds <= 2000)
            {
                return;
            }

            var messageOut = string.Format("{0} Executed in {1} seconds", args.Method.Name, sw.ElapsedMilliseconds / 1000);
            Trace.WriteLine(messageOut);

            // *this.Logger.Trace(messageOut);
        }
    }
}
