// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspectCacheAttribute.cs" company="TODO: CompanyName">
//      TODO: Update copyright text.
// </copyright>
// <summary>
//      TODO: Add summary
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Aspects
{
    using System;
    using System.Reflection;
    using System.Text;

    using PostSharp.Aspects;
    using PostSharp.Extensibility;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class AspectCacheAttribute : MethodInterceptionAspect
    {
       [NonSerialized]
        private static readonly ICache Cache;

        private string methodName;

        static AspectCacheAttribute()
        {
            if (!PostSharpEnvironment.IsPostSharpRunning)
            {
                // one minute cache
                Cache = new StaticMemoryCache(new TimeSpan(0, 1, 0));

                // use an IoC container/service locator here in practice
            }
        }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            this.methodName = method.Name;
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var key = this.BuildCacheKey(args.Arguments);
            if (Cache[key] != null)
            {
                args.ReturnValue = Cache[key];
            }
            else
            {
                var returnVal = args.Invoke(args.Arguments);
                args.ReturnValue = returnVal;
                Cache[key] = returnVal;
            }
        }

        private string BuildCacheKey(Arguments arguments)
        {
            var sb = new StringBuilder();
            sb.Append(this.methodName);
            foreach (var argument in arguments.ToArray())
            {
                sb.Append(argument == null ? "_" : argument.ToString());
            }

            return sb.ToString();
        }
    }
}