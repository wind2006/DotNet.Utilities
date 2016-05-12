namespace YanZhiwei.DotNet.RestSharp.Utilities
{
    using global::RestSharp;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// RestSharp 帮助类
    /// </summary>
    /// 时间：2016-04-06 13:52
    /// 备注：
    public static class RestSharpHelper
    {
        #region Methods

        /// <summary>
        /// 异步获取内容
        /// <para>var client = new RestClient("http://example.org");</para>
        /// <para>var request = new RestRequest("product/42", Method.GET);</para>
        /// <para>var content = await client.GetContentAsync(request);</para>
        /// </summary>
        /// <param name="client">RestClient</param>
        /// <param name="request">IRestRequest</param>
        /// <returns></returns>
        /// 时间：2016-04-06 14:04
        /// 备注：
        public static Task<string> GetContentAsync(this RestClient client, IRestRequest request)
        {
            return client.SelectAsync(request, r => r.Content);
        }

        /// <summary>
        /// 异步获取IRestResponse
        /// <para>var client = new RestClient("http://example.org");</para>
        /// <para>var request = new RestRequest("product/42", Method.GET);</para>
        /// <para>var response = await client.GetResponseAsync(request);</para>
        /// </summary>
        /// <param name="client">RestClient</param>
        /// <param name="request">IRestRequest</param>
        /// <returns></returns>
        /// 时间：2016-04-06 14:05
        /// 备注：
        public static Task<IRestResponse> GetResponseAsync(this RestClient client, IRestRequest request)
        {
            return client.SelectAsync(request, r => r);
        }

        private static Task<T> SelectAsync<T>(this RestClient client, IRestRequest request, Func<IRestResponse, T> selector)
        {
            var _tcs = new TaskCompletionSource<T>();
            RestRequestAsyncHandle _restResponse = client.ExecuteAsync(request, r =>
            {
                if (r.ErrorException == null)
                {
                    _tcs.SetResult(selector(r));
                }
                else
                {
                    _tcs.SetException(r.ErrorException);
                }
            });
            return _tcs.Task;
        }

        #endregion Methods
    }
}