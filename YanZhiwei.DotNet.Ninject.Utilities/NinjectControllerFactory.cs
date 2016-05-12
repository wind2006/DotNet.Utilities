using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace YanZhiwei.DotNet.Ninject.Utilities
{
    /// <summary>
    /// 在ASP.NET MVC中使用Ninject
    /// </summary>
    /// 日期：2015-10-23 16:27
    /// 备注：在application_start中使用
    /// eg: ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bindFactory">委托，eg:ninjectKernel.Bind'IProductRepository'().To'FakeProductRepository'();</param>
        /// 日期：2015-10-23 16:28
        /// 备注：
        public NinjectControllerFactory(Action<IKernel> bindFactory)
        {
            ninjectKernel = new StandardKernel();
            bindFactory(ninjectKernel);
        }

        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>
        /// The controller instance.
        /// </returns>
        /// 日期：2015-10-23 16:30
        /// 备注：
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
    }
}