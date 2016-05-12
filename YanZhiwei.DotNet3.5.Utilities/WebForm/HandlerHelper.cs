namespace YanZhiwei.DotNet3._5.Utilities.WebForm
{
    using System.Net;
    using System.Web;

    using YanZhiwei.DotNet3._5.Utilities.Common;
    using YanZhiwei.DotNet3._5.Utilities.Model;

    /// <summary>
    /// HttpHandler帮助类
    /// </summary>
    /// 创建时间:2015-06-08 11:46
    /// 备注说明:<c>null</c>
    public static class HandlerHelper
    {
        #region Methods

        /// <summary>
        /// 创建文件全路径
        /// <para>eg: context.CreateFilePath("images/1616/LampGroup/lampGroup.jpg")</para>
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="file">The file.eg:"images/1616/LampGroup/lampGroup.jpg"</param>
        /// <returns></returns>
        /// 创建时间:2015-06-09 11:15
        /// 备注说明:<c>null</c>
        public static string CreateFilePath(this HttpContext context, string file)
        {
            string _fullPath = @"http://" + context.Request.Url.Authority
                + (context.Request.ApplicationPath == "/" ? "/" : context.Request.ApplicationPath + "/")
                + file;
            return _fullPath;
        }

        /// <summary>
        /// 创建Response响应
        /// <para>eg:context.CreateResponse(_zNodeList, HttpStatusCode.OK);</para>
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="obj">对象</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="errorCode">T错误代码</param>
        /// 时间：2016-05-04 17:32
        /// 备注：
        public static void CreateResponse(this HttpContext context, object obj, HttpStatusCode statusCode, int errorCode)
        {
            JsonResult _jsonResult = new JsonResult();
            _jsonResult.StatusCode = (int)statusCode;
            _jsonResult.Message = obj;
            _jsonResult.ErrorCode = errorCode;
            string _jsonString = SerializationHelper.JsonSerialize<JsonResult>(_jsonResult);
            context.Response.Write(_jsonString);
            context.ApplicationInstance.CompleteRequest();
        }

        /// <summary>
        /// 创建Response响应
        /// <para>eg:context.CreateResponse(_zNodeList, HttpStatusCode.OK);</para>
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="obj">对象</param>
        /// <param name="statusCode">状态码</param>
        /// 时间：2016-05-04 17:32
        /// 备注：
        public static void CreateResponse(this HttpContext context, object obj, HttpStatusCode statusCode)
        {
            CreateResponse(context, obj, statusCode, 0);
        }

        #endregion Methods
    }
}