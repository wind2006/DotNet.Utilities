namespace YanZhiwei.DotNet3._5.Utilities.WebForm.Core
{
    using DotNet2.Utilities.Common;
    using DotNet2.Utilities.Enums;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Web;

    /// <summary>
    /// ASP.NET 图片上传
    /// </summary>
    public class WebUploadImage
    {
        #region Constructors

        /// <summary>
        /// 构造方法
        /// </summary>
        public WebUploadImage()
        {
            SetAllowFormat = ImageHelper.AllowExt;   //允许图片格式
            SetAllowSize = 5;       //允许上传图片大小,默认为5MB
            SetPositionWater = SetWaterPosition.bottomRight;
            SetCutImage = true;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 允许图片格式
        /// </summary>
        public string SetAllowFormat
        {
            get; set;
        }

        /// <summary>
        /// 允许上传图片大小
        /// </summary>
        public double SetAllowSize
        {
            get; set;
        }

        /// <summary>
        /// 是否剪裁图片，默认true
        /// </summary>
        public bool SetCutImage
        {
            get; set;
        }

        /// <summary>
        /// 是否限制最大宽度，默认为true
        /// </summary>
        public bool SetLimitWidth
        {
            get; set;
        }

        /// <summary>
        /// 最大宽度尺寸，默认为600
        /// </summary>
        public int SetMaxWidth
        {
            get; set;
        }

        /// <summary>
        /// 限制图片最小宽度，0表示不限制
        /// </summary>
        public int SetMinWidth
        {
            get; set;
        }

        /// <summary>
        /// 图片水印
        /// </summary>
        public string SetPicWater
        {
            get; set;
        }

        /// <summary>
        /// 水印图片的位置 0居中、1左上角、2右上角、3左下角、4右下角
        /// </summary>
        public SetWaterPosition SetPositionWater
        {
            get; set;
        }

        /// <summary>
        /// 缩略图高度多个逗号格开（例如:200,100）
        /// </summary>
        public string SetSmallImgHeight
        {
            get; set;
        }

        /// <summary>
        /// 缩略图宽度多个逗号格开（例如:200,100）
        /// </summary>
        public string SetSmallImgWidth
        {
            get; set;
        }

        /// <summary>
        /// 文字水印字符
        /// </summary>
        public string SetWordWater
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="postedFile">HttpPostedFile控件</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="imgWidth">图片宽度</param>
        /// <param name="imgHeight">图片高度</param>
        /// <param name="cMode">剪切类型</param>
        /// <returns>返回上传信息</returns>
        public UploadImageMessage FileCutSaveAs(HttpPostedFile postedFile, string savePath, int imgWidth, int imgHeight, ImageCutMode cMode)
        {
            UploadImageMessage _uploadImageMsg = new UploadImageMessage();
            try
            {
                //获取上传文件的扩展名
                string _fileEx = Path.GetExtension(postedFile.FileName);
                if (!FileHelper.CheckValidExt(SetAllowFormat, _fileEx))
                {
                    AddUploadImageMessage(_uploadImageMsg, 2);
                    return _uploadImageMsg;
                }

                //获取上传文件的大小
                double _fileSize = postedFile.ContentLength / 1024.0 / 1024.0;

                if (_fileSize > SetAllowSize)
                {
                    AddUploadImageMessage(_uploadImageMsg, 3);
                    return _uploadImageMsg;  //超过文件上传大小
                }
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                string _newFileName = DateTime.Now.FormatDate(12),
                       _fName = "s" + _newFileName + _fileEx,
                       _fullPath = Path.Combine(savePath, _fName);
                postedFile.SaveAs(_fullPath);

                string _cfileName = DateTime.Now.FormatDate(12),
                       _cfName = _cfileName + _fileEx;
                _uploadImageMsg.IsError = false;
                _uploadImageMsg.FileName = _cfName;
                string _cFullPath = Path.Combine(savePath, _cfName);
                _uploadImageMsg.FilePath = _cFullPath;
                _uploadImageMsg.WebPath = "/" + _cFullPath.Replace(HttpContext.Current.Server.MapPath("~/"), "").Replace("\\", "/");
                ImageHelper.CreateSmallPhoto(_fullPath, imgWidth, imgHeight, _cFullPath, cMode);
                //if (File.Exists(_fullPath))
                //{
                //    File.Delete(_fullPath);
                //}
                if (_fileSize > 100)
                {
                    ImageHelper.CompressPhoto(_cFullPath, 100);
                }
            }
            catch (Exception ex)
            {
                AddUploadImageMessage(_uploadImageMsg, ex.Message);
            }
            return _uploadImageMsg;
        }

        /// <summary>
        /// 通用图片上传类
        /// </summary>
        /// <param name="postedFile">HttpPostedFile控件</param>
        /// <param name="savePath">保存路径</param>
        /// <returns>返回上传信息</returns>
        public UploadImageMessage FileSaveAs(HttpPostedFile postedFile, string savePath)
        {
            UploadImageMessage _uploadImageMsg = new UploadImageMessage();
            try
            {
                if (string.IsNullOrEmpty(postedFile.FileName))
                {
                    AddUploadImageMessage(_uploadImageMsg, 4);
                    return _uploadImageMsg;
                }

                int _randomNumber = RandomHelper.NextNumber(1000, 9999);
                string _fileName = DateTime.Now.FormatDate(12) + _randomNumber,
                       _fileEx = Path.GetExtension(postedFile.FileName);
                if (!FileHelper.CheckValidExt(SetAllowFormat, _fileEx))
                {
                    AddUploadImageMessage(_uploadImageMsg, 2);
                    return _uploadImageMsg;
                }
                double _fileSize = postedFile.ContentLength / 1024.0 / 1024.0;
                if (_fileSize > SetAllowSize)
                {
                    AddUploadImageMessage(_uploadImageMsg, 3);
                    return _uploadImageMsg;
                }
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                _uploadImageMsg.FileName = _fileName + _fileEx;
                string _fullPath = savePath.Trim('\\') + "\\" + _uploadImageMsg.FileName;
                _uploadImageMsg.WebPath = "/" + _fullPath.Replace(HttpContext.Current.Server.MapPath("~/"), "").Replace("\\", "/");
                _uploadImageMsg.FilePath = _fullPath;
                _uploadImageMsg.Size = _fileSize;
                postedFile.SaveAs(_fullPath);

                Bitmap _sourceBmp = new Bitmap(_fullPath);
                int _sourceWidth = _sourceBmp.Width,
                    _sourceHeight = _sourceBmp.Height;
                _sourceBmp.Dispose();

                if (SetMinWidth > 0)
                {
                    if (_sourceWidth < SetMinWidth)
                    {
                        AddUploadImageMessage(_uploadImageMsg, 7);
                        return _uploadImageMsg;
                    }
                }

                if (SetLimitWidth && _sourceWidth > SetMaxWidth)
                {
                    int _width = SetMaxWidth;
                    int _height = _width * _sourceHeight / _sourceWidth;

                    string _tempFile = savePath + Guid.NewGuid().ToString() + _fileEx;
                    File.Move(_fullPath, _tempFile);
                    ImageHelper.CreateSmallPhoto(_tempFile, _width, _height, _fullPath);
                    File.Delete(_tempFile);
                }

                if (_fileEx.ToLower() != ".gif")
                {
                    ImageHelper.CompressPhoto(_fullPath, 100);
                }
                if (string.IsNullOrEmpty(SetSmallImgWidth))
                {
                    _uploadImageMsg.Message = "上传成功,无缩略图";
                    return _uploadImageMsg;
                }

                string[] _widthArray = SetSmallImgWidth.Split(',');
                string[] _heightArray = SetSmallImgHeight.Split(',');
                if (_widthArray.Length != _heightArray.Length)
                {
                    AddUploadImageMessage(_uploadImageMsg, 6);
                    return _uploadImageMsg;
                }

                for (int i = 0; i < _widthArray.Length; i++)
                {
                    if (Convert.ToInt32(_widthArray[i]) <= 0 || Convert.ToInt32(_heightArray[i]) <= 0)
                        continue;

                    string _descFile = savePath.TrimEnd('\\') + '\\' + _fileName + "_" + i.ToString() + _fileEx;

                    //判断图片高宽是否大于生成高宽。否则用原图
                    if (_sourceWidth > Convert.ToInt32(_widthArray[i]))
                    {
                        if (SetCutImage)
                        {
                            ImageHelper.CreateSmallPhoto(_fullPath, Convert.ToInt32(_widthArray[i]), Convert.ToInt32(_heightArray[i]), _descFile);
                        }
                        else {
                            ImageHelper.CreateSmallPhoto(_fullPath, Convert.ToInt32(_widthArray[i]), Convert.ToInt32(_heightArray[i]), _descFile, ImageCutMode.CutNo);
                        }
                    }
                    else
                    {
                        if (SetCutImage)
                        {
                            ImageHelper.CreateSmallPhoto(_fullPath, _sourceWidth, _sourceHeight, _descFile);
                        }
                        else {
                            ImageHelper.CreateSmallPhoto(_fullPath, _sourceWidth, _sourceHeight, _descFile, ImageCutMode.CutNo);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(SetPicWater))
                    ImageHelper.AttachPng(SetPicWater, _fullPath, SetWaterPosition.bottomRight);
                if (!string.IsNullOrEmpty(SetWordWater))
                    ImageHelper.AttachText(SetWordWater, _fullPath);
            }
            catch (Exception ex)
            {
                AddUploadImageMessage(_uploadImageMsg, ex.Message);
            }
            return _uploadImageMsg;
        }

        /// <summary>
        /// 增加上传错误信息
        /// </summary>
        /// <param name="rm">UploadImageMessage</param>
        /// <param name="code">错误代码</param>
        private void AddUploadImageMessage(UploadImageMessage rm, int code)
        {
            rm.IsError = true;
            rm.Message = GetCodeMessage(code);
        }

        /// <summary>
        /// 增加上传错误信息
        /// </summary>
        /// <param name="rm">UploadImageMessage</param>
        /// <param name="message">错误信息</param>
        private void AddUploadImageMessage(UploadImageMessage rm, string message)
        {
            rm.IsError = true;
            rm.Message = message;
        }

        /// <summary>
        /// 图片上传错误编码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetCodeMessage(int code)
        {
            Dictionary<int, string> _uploadImageCode = new Dictionary<int, string>(){
            {0,"系统配置错误"},
            {1,"上传图片成功"},
            {2,string.Format( "对不起，上传格式错误！请上传{0}格式图片",SetAllowFormat)},
            {3,string.Format("超过文件上传大小,不得超过{0}M",SetAllowSize)},
            {4,"未上传文件"},
            {5,""},
            {6,"缩略图长度和宽度配置错误"},
            {7,"检测图片宽度限制"}
             };
            return _uploadImageCode[code];
        }

        #endregion Methods
    }
}