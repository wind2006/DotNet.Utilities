namespace YanZhiwei.DotNet3._5.Utilities.WebForm.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// 模拟Get，Post请求
    /// </summary>
    public class WebRequest
    {
        #region Fields

        /// <summary>
        /// accept
        /// </summary>
        private string accept = "*/*";

        /// <summary>
        /// 是否允许重定向
        /// </summary>
        private bool allowAutoRedirect = true;

        /// <summary>
        /// contentType
        /// </summary>
        private string contentType = "application/x-www-form-urlencoded";

        /// <summary>
        /// 设置cookie
        /// </summary>
        private CookieContainer cookie;

        /// <summary>
        /// 过期时间
        /// </summary>
        private int time = 5000;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 获得响应中的图像
        /// </summary>
        /// <param name="url">链接</param>
        /// <returns>若发生异常则返回NULL</returns>
        public Stream GetResponseImage(string url)
        {
            Stream _responseStream = null;
            try
            {
                HttpWebRequest _request = (HttpWebRequest)System.Net.WebRequest.Create(url);
                _request.KeepAlive = true;
                _request.Method = "GET";
                _request.AllowAutoRedirect = allowAutoRedirect;
                _request.CookieContainer = cookie;
                _request.ContentType = this.contentType;
                _request.Accept = this.accept;
                _request.Timeout = time;
                Encoding _encoding = Encoding.GetEncoding("UTF-8");
                this.SetCertificatePolicy();
                HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
                _responseStream = _response.GetResponseStream();
                return _responseStream;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// get请求获取返回的html
        /// </summary>
        /// <param name="url">无参URL</param>
        /// <param name="querydata">参数</param>
        /// <returns></returns>
        public string HttpGet(string url, Dictionary<string, string> querydata)
        {
            if (querydata != null && querydata.Count > 0)
            {
                url += "?" + string.Join("&", querydata.Select(it => it.Key + "=" + it.Value).ToArray());
            }

            return HttpGet(url);
        }

        /// <summary>
        /// get请求获取返回的html
        /// </summary>
        /// <param name="url">链接</param>
        /// <returns>html</returns>
        public string HttpGet(string url)
        {
            HttpWebRequest _request = (HttpWebRequest)System.Net.WebRequest.Create(url);
            _request.Method = "GET";
            _request.ContentType = "text/html;charset=UTF-8";
            _request.CookieContainer = cookie;
            _request.Accept = this.accept;
            _request.Timeout = time;
            this.SetCertificatePolicy();
            HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
            Stream _responseStream = _response.GetResponseStream();
            StreamReader _streamReader = new StreamReader(_responseStream, Encoding.GetEncoding("utf-8"));
            string _readerString = _streamReader.ReadToEnd();
            _streamReader.Close();
            _responseStream.Close();
            return _readerString;
        }

        /// <summary>
        /// post请求返回html
        /// </summary>
        /// <param name="url">连接</param>
        /// <param name="postParamters">参数</param>
        /// <returns>html</returns>
        public string HttpPost(string url, Dictionary<string, string> postParamters)
        {
            string _parameters = null;
            if (postParamters != null && postParamters.Count > 0)
            {
                _parameters = string.Join("&", postParamters.Select(it => it.Key + "=" + it.Value).ToArray());
            }
            HttpWebRequest _request = (HttpWebRequest)System.Net.WebRequest.Create(url);
            _request.AllowAutoRedirect = allowAutoRedirect;
            _request.Method = "POST";
            _request.Accept = accept;
            _request.ContentType = this.contentType;
            _request.Timeout = time;
            _request.ContentLength = _parameters == null ? 0 : Encoding.UTF8.GetByteCount(_parameters);
            if (cookie != null)
                _request.CookieContainer = cookie; //cookie信息由CookieContainer自行维护
            Stream _requestStream = _request.GetRequestStream();
            StreamWriter _streamWriter = new StreamWriter(_requestStream, Encoding.GetEncoding("gb2312"));
            _streamWriter.Write(_parameters);
            _streamWriter.Close();
            HttpWebResponse _response = null;
            try
            {
                this.SetCertificatePolicy();
                _response = (HttpWebResponse)_request.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (_response != null)
            {
                Stream _responseStream = _response.GetResponseStream();
                StreamReader _streamReader = new StreamReader(_responseStream, Encoding.GetEncoding("utf-8"));
                string _readerString = _streamReader.ReadToEnd();
                _streamReader.Close();
                _responseStream.Close();
                return _readerString;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// POST文件
        /// </summary>
        /// <param name="url">链接</param>
        /// <param name="file">文件路径</param>
        /// <param name="postParamters"></param>
        /// <returns>Html</returns>
        public string HttpUploadFile(string url, string file, Dictionary<string, string> postParamters)
        {
            return HttpUploadFile(url, file, postParamters, Encoding.UTF8);
        }

        /// <summary>
        /// POST文件
        /// </summary>
        /// <param name="url">链接</param>
        /// <param name="file">文件路径</param>
        /// <param name="postParamters">参数</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public string HttpUploadFile(string url, string file, Dictionary<string, string> postParamters, Encoding encoding)
        {
            return HttpUploadFile(url, new string[] { file }, postParamters, encoding);
        }

        /// <summary>
        /// POST文件
        /// </summary>
        /// <param name="url">链接</param>
        /// <param name="files">文件路径</param>
        /// <param name="postParamters">参数</param>
        /// <returns>Html</returns>
        public string HttpUploadFile(string url, string[] files, Dictionary<string, string> postParamters)
        {
            return HttpUploadFile(url, files, postParamters, Encoding.UTF8);
        }

        /// <summary>
        /// POST文件
        /// </summary>
        /// <param name="url">链接</param>
        /// <param name="files">文件路径</param>
        /// <param name="postParamters">参数</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public string HttpUploadFile(string url, string[] files, Dictionary<string, string> postParamters, Encoding encoding)
        {
            string _boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] _boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + _boundary + "\r\n");
            byte[] _endbytes = Encoding.ASCII.GetBytes("\r\n--" + _boundary + "--\r\n");

            //1.HttpWebRequest
            HttpWebRequest _request = (HttpWebRequest)System.Net.WebRequest.Create(url);
            _request.ContentType = "multipart/form-data; boundary=" + _boundary;
            _request.Method = "POST";
            _request.KeepAlive = true;
            _request.Accept = this.accept;
            _request.Timeout = this.time;
            _request.AllowAutoRedirect = this.allowAutoRedirect;
            if (cookie != null)
                _request.CookieContainer = cookie;
            _request.Credentials = CredentialCache.DefaultCredentials;

            using (Stream stream = _request.GetRequestStream())
            {
                //1.1 key/value
                string _formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                if (postParamters != null)
                {
                    foreach (string key in postParamters.Keys)
                    {
                        stream.Write(_boundarybytes, 0, _boundarybytes.Length);
                        string formitem = string.Format(_formdataTemplate, key, postParamters[key]);
                        byte[] formitembytes = encoding.GetBytes(formitem);
                        stream.Write(formitembytes, 0, formitembytes.Length);
                    }
                }

                //1.2 file
                string _headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                byte[] _buffer = new byte[4096];
                int _bytesRead = 0;
                for (int i = 0; i < files.Length; i++)
                {
                    stream.Write(_boundarybytes, 0, _boundarybytes.Length);
                    string _header = string.Format(_headerTemplate, "file" + i, Path.GetFileName(files[i]));
                    byte[] _headerbytes = encoding.GetBytes(_header);
                    stream.Write(_headerbytes, 0, _headerbytes.Length);
                    using (FileStream fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                    {
                        while ((_bytesRead = fileStream.Read(_buffer, 0, _buffer.Length)) != 0)
                        {
                            stream.Write(_buffer, 0, _bytesRead);
                        }
                    }
                }

                //1.3 form end
                stream.Write(_endbytes, 0, _endbytes.Length);
            }
            //2.WebResponse
            HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
            using (StreamReader stream = new StreamReader(_response.GetResponseStream()))
            {
                return stream.ReadToEnd();
            }
        }

        /// <summary>
        /// 设置accept(默认:*/*)
        /// </summary>
        /// <param name="accept"></param>
        public void SetAccept(string accept)
        {
            this.accept = accept;
        }

        /// <summary>
        /// 设置contentType(默认:application/x-www-form-urlencoded)
        /// </summary>
        /// <param name="contentType"></param>
        public void SetContentType(string contentType)
        {
            this.contentType = contentType;
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookie"></param>
        public void SetCookie(CookieContainer cookie)
        {
            this.cookie = cookie;
        }

        /// <summary>
        /// 是否允许重定向(默认:true)
        /// </summary>
        /// <param name="allowAutoRedirect"></param>
        public void SetIsAllowAutoRedirect(bool allowAutoRedirect)
        {
            this.allowAutoRedirect = allowAutoRedirect;
        }

        /// <summary>
        /// 设置请求过期时间（单位：毫秒）（默认：5000）
        /// </summary>
        /// <param name="time"></param>
        public void SetTimeOut(int time)
        {
            this.time = time;
        }

        /// <summary>
        /// 远程证书验证，固定返回true
        /// </summary>
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        //注册证书验证回调事件，在请求之前注册
        private void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }

        #endregion Methods

        #region Other

        /*
         *参考
         *1. http://www.cnblogs.com/sunkaixuan/p/4992576.html

         *知识
          1. get方法传输的数据的写法： Request.QueryString["name"];
          2. post方法传输的数据的写法:Request.Form["name"];
          3. get和post方法传送数据的代码写法:Request.Params["name"];
          4. get是从服务器上获取数据，post是向服务器传送数据;
          5. get是把参数数据队列加到提交表单的ACTION属性所指的URL中，值和表单内各个字段一一对应，在URL中可以看到。post是通过HTTP post机制，将表单内各个字段与其内容放置在HTML HEADER内一起传送到ACTION属性所指的URL地址。用户看不到这个过程。
          6. 对于get方式，服务器端用Request.QueryString获取变量的值，对于post方式，服务器端用Request.Form获取提交的数据。
          7. get传送的数据量较小，不能大于2KB。post传送的数据量较大，一般被默认为不受限制。但理论上，IIS4中最大量为80KB，IIS5中为100KB。
          8. get安全性非常低，post安全性较高。但是执行效率却比Post方法好。
          9. get方式的安全性较Post方式要差些，包含机密信息的话，建议用Post数据提交方式；
          10.在做数据查询时，建议用Get方式；而在做数据添加、修改或删除时，建议用Post方式；
          */

        #endregion Other
    }
}