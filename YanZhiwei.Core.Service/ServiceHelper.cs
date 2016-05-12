using Castle.DynamicProxy;
using System;
using YanZhiwei.DotNet2.Utilities.Core;

namespace YanZhiwei.DotNet.Core.Service
{
    public class ServiceHelper
    {
        /// <summary>
        /// 暂时使用引用服务方式，可以改造成注入，或使用WCF服务方式
        /// </summary>
        public readonly ServiceFactory serviceFactory = null;

        public ServiceHelper(ServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        /// <summary>
        /// 创建服务根据BLL接口
        /// </summary>
        public T CreateService<T>() where T : class
        {
            var service = serviceFactory.CreateService<T>();

            //拦截，可以写日志....
            var generator = new ProxyGenerator();
            var dynamicProxy = generator.CreateInterfaceProxyWithTargetInterface<T>(
                service, new InvokeInterceptor());

            return dynamicProxy;
        }
    }

    internal class InvokeInterceptor : IInterceptor
    {
        public InvokeInterceptor()
        {
        }

        /// <summary>
        /// 拦截方法
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                if (exception is BusinessException)
                    throw;

                var message = new
                {
                    exception = exception.Message,
                    exceptionContext = new
                    {
                        method = invocation.Method.ToString(),
                        arguments = invocation.Arguments,
                        returnValue = invocation.ReturnValue
                    }
                };

                //   Log4NetHelper.Error(LoggerType.ServiceExceptionLog, message, exception);
                throw;
            }
        }
    }
}