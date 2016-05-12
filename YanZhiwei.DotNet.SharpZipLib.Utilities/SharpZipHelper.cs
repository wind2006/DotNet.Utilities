using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet.SharpZipLib.Utilities
{
    /// <summary>
    /// 文件压缩帮助类
    /// </summary>
    public class SharpZipHelper
    {
        /// <summary>
        /// 实现压缩功能(压缩密码与压缩文件描述均为空)
        /// </summary>
        /// <param name="filenameToZip">要压缩文件(绝对文件路径)</param>
        /// <param name="zipedfiledname">压缩(绝对文件路径)</param>
        /// <param name="compressionLevel">压缩比</param>
        public static void MakeZipFile(string[] filenameToZip, string zipedfiledname, int compressionLevel)
        {
            MakeZipFile(filenameToZip, zipedfiledname, compressionLevel, string.Empty, string.Empty);
        }

        /**/

        /// <summary>
        /// 实现压缩功能
        /// </summary>
        /// <param name="filenameToZip">要压缩文件(绝对文件路径)</param>
        /// <param name="zipedfiledname">压缩(绝对文件路径)</param>
        /// <param name="compressionLevel">压缩比</param>
        /// <param name="password">加密密码</param>
        /// <param name="comment">压缩文件描述</param>
        public static void MakeZipFile(string[] filenameToZip, string zipedfiledname, int compressionLevel,
            string password, string comment)
        {
            File.Delete(zipedfiledname);
            FileStream _fileSream = File.Open(zipedfiledname, FileMode.Create);
            ZipOutputStream _newzipstream = new ZipOutputStream(_fileSream);
            try
            {
                _newzipstream.SetLevel(compressionLevel);

                if (!string.IsNullOrEmpty(password))
                    _newzipstream.Password = password;

                if (!string.IsNullOrEmpty(comment))
                    _newzipstream.SetComment(comment);

                foreach (string filename in filenameToZip)
                {
                    if (string.IsNullOrEmpty(filename)) continue;
                    FileStream _newstream = File.OpenRead(filename);//打开预压缩文件
                    byte[] _setbuffer = new byte[_newstream.Length];
                    _newstream.Read(_setbuffer, 0, _setbuffer.Length);//读入文件

                    ZipEntry _newEntry = new ZipEntry(Path.GetFileName(filename));
                    _newEntry.Size = _newstream.Length;
                    _newstream.Close();
                    _newzipstream.PutNextEntry(_newEntry);//压入
                    _newzipstream.Write(_setbuffer, 0, _setbuffer.Length);
                }
            }
            catch (Exception ex)
            {
                //出现异常
                File.Delete(zipedfiledname);
                throw ex;
            }
            finally
            {
                _newzipstream.Finish();
                _newzipstream.Close();
                _fileSream.Close();
            }
        }

        /**/

        /// <summary>
        /// 实现解压操作(密码为空,默认解压当压缩文件同级目录)
        /// </summary>
        /// <param name="zipfilename">要解压文件Zip(物理路径)</param>
        public static void UnMakeZipFile(string zipfilename)
        {
            UnMakeZipFile(zipfilename, string.Empty, string.Empty);
        }

        /**/

        /// <summary>
        /// 实现解压操作
        /// </summary>
        /// <param name="zipfilename">要解压文件Zip(物理路径)</param>
        /// <param name="UnZipDir">解压目的路径(物理路径)</param>
        /// <param name="password">解压密码</param>
        public static void UnMakeZipFile(string zipfilename, string UnZipDir, string password)
        {
            FileStream _fileStream = File.OpenRead(zipfilename);
            ZipInputStream _zipStream = new ZipInputStream(_fileStream);

            if (!string.IsNullOrEmpty(password))
                _zipStream.Password = password;
            try
            {
                ZipEntry _zipEntry;
                //获取Zip中单个File
                while ((_zipEntry = _zipStream.GetNextEntry()) != null)
                {
                    if (string.IsNullOrEmpty(UnZipDir))
                        UnZipDir = FileHelper.GetExceptEx(zipfilename) + "\\";
                    if (!Directory.Exists(UnZipDir))
                        Directory.CreateDirectory(UnZipDir);//创建目的目录

                    //获得目的目录信息
                    string _Driectoryname = Path.GetDirectoryName(UnZipDir);
                    string _pathname = Path.GetDirectoryName(_zipEntry.Name);//获得子级目录
                    string _filename = Path.GetFileName(_zipEntry.Name);//获得子集文件名

                    //处理文件盘符问题
                    _pathname = _pathname.Replace(":", "$");//处理当前压缩出现盘符问题
                    _Driectoryname = _Driectoryname + "\\" + _pathname;

                    //创建
                    Directory.CreateDirectory(_Driectoryname);

                    //解压指定子目录
                    if (_filename != string.Empty)
                    {
                        FileStream _newstream = File.Create(_Driectoryname + "\\" + _filename);
                        int _size = 1024;

                        byte[] _bytes = new byte[_size];
                        while (true)
                        {
                            _size = _zipStream.Read(_bytes, 0, _bytes.Length);
                            if (_size > 0)
                            {
                                //写入数据
                                _newstream.Write(_bytes, 0, _size);
                            }
                            else
                            {
                                _newstream.Close();
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                _zipStream.Close();
                _fileStream.Close();
            }
        }
    }
}