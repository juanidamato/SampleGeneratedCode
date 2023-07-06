using AutoMapper.Configuration.Annotations;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using SampleGeneratedCodeApplication.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCodeApplication.Commons.Attributes
{
    public class TraceAndTimeAttibuteInterceptor : IInterceptor
    {
        private readonly ILogger<TraceAndTimeAttibuteInterceptor> _logger;

        public TraceAndTimeAttibuteInterceptor(ILogger<TraceAndTimeAttibuteInterceptor> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            string methodName="";
            if (invocation.Method.ReflectedType is not null 
                && invocation.Method.ReflectedType.Name is not null)
            {
                methodName = invocation.Method.ReflectedType.Name + ".";
            }
            methodName = methodName+invocation.Method.Name;
            System.Diagnostics.Stopwatch? watch=null;
            try
            {
                _logger.LogInformation($"Enter to method:{@methodName}");
                if (GlobalVariables.EnableTraceTime)
                {
                   watch = System.Diagnostics.Stopwatch.StartNew();
                   watch.Start();
                }
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Exception in method:{@methodName}");
            }
            finally
            {
                if (GlobalVariables.EnableTraceTime)
                {
                    watch!.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    _logger.LogInformation($"Time taken for method:{@methodName} {@elapsedMs} ms");

                }
                _logger.LogInformation($"Exit from method:{@methodName}");
            }
        }
    }
}
