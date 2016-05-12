using System;
using System.IO;
using System.Linq;
using YanZhiwei.DotNet2.Utilities.Common;

namespace cdz360Tools.Services.Models
{
    public class Cdz360PackageData
    {
        /// <summary>
        /// 同步字
        /// </summary>
        public byte SynWord
        {
            get;
            set;
        }

        /// <summary>
        /// 长度域（命令字+用户数据长度）（V2 双字节）
        /// </summary>
        public byte[] DataLength
        {
            get;
            set;
        }

        /// <summary>
        /// 命令字
        /// </summary>
        public byte CmdWord
        {
            get;
            set;
        }

        /// <summary>
        /// 命令字的参数
        /// </summary>
        public byte[] CmdParms
        {
            get;
            set;
        }

        /// <summary>
        /// 校验码（V1单字节）
        /// </summary>
        public byte CRC
        {
            get;
            set;
        }

        public byte[] ToBytes()
        {
            byte[] _byte;
            try
            {
                using (MemoryStream mem = new MemoryStream())
                {
                    BinaryWriter writer = new BinaryWriter(mem);
                    writer.Write(SynWord);//帧起始字节 1
                    writer.Write(CmdWord);//指令控制位 1
                    if (DataLength == null)//计算长度
                        DataLength = ByteHelper.ToBytes(CmdParms.Length, 2).Reverse().ToArray();
                    writer.Write(DataLength);//数据长度 2

                    //用户数据可能为空
                    if (CmdParms != null)//数据域 N
                    {
                        writer.Write(CmdParms);
                    }
                    if (CRC == 0x00)//计算CRC
                        CRC = GetCRC(CmdWord, DataLength, CmdParms);
                    writer.Write(CRC);//校验位

                    _byte = mem.ToArray();
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return _byte;
        }

        /// <summary>
        /// 初始化数据包对象（同步字1,2；结束码)
        /// </summary>
        /// <returns></returns>
        public static Cdz360PackageData InitPackage()
        {
            Cdz360PackageData package = new Cdz360PackageData();
            package.SynWord = PackageConst.SynWord;

            return package;
        }

        /// <summary>
        /// 根据字节数组进行分析合法性判断，并在返回参数中生成CTU通讯包对象和合法性判断的信息
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="package">CTU通讯包对象</param>
        /// <param name="error">包对象生成合法性与否的信息</param>
        /// <returns></returns>
        public static bool BuileObjFromBytes(byte[] buffer, out Cdz360PackageData package, out byte[] afterPackage, out PackageErrorEnum error)
        {
            bool _flag = false;

            afterPackage = null;
            package = InitPackage();
            error = PackageErrorEnum.Normal;
            try
            {
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        //读取同步字 1
                        package.SynWord = reader.ReadByte();

                        //验证第一个同步字合法性
                        if (package.SynWord != PackageConst.SynWord)
                        {
                            error = PackageErrorEnum.SynWordError;
                            return false;
                        }
                        //读取指令控制位 1
                        package.CmdWord = reader.ReadByte();

                        //数据长度 2
                        package.DataLength = reader.ReadBytes(2);

                        //验证长度域
                        //长度域包括命令字2个字节所以长度域必须大于等于2
                        ushort datalength = BitConverter.ToUInt16(package.DataLength.Reverse().ToArray(), 0);
                        if (datalength < 2)
                        {
                            error = PackageErrorEnum.DataLengthError;
                            return false;
                        }

                        //验证整个包的长度合法性
                        //包的长度 = 当前位置+数据域 + CRC（1）
                        long _calPackLengh = reader.BaseStream.Position + datalength + 1;
                        if (buffer.Length != _calPackLengh)
                        {
                            //实际收到的包比包格式定义的要大，截取有效数据包
                            if (buffer.Length > _calPackLengh)
                            {
                                afterPackage = new Byte[_calPackLengh];
                                Array.Copy(buffer, 0, afterPackage, 0, _calPackLengh);
                            }
                            else
                            {
                                error = PackageErrorEnum.PackageLengthError;
                                return false;
                            }
                        }

                        //命令字的参数 = 总长度
                        package.CmdParms = reader.ReadBytes(datalength);

                        package.CRC = reader.ReadByte();

                        //验证CRC校验码
                        if (!CheckCRC(package.CmdWord, package.DataLength, package.CmdParms, package.CRC))
                        {
                            error = PackageErrorEnum.CRCError;
                            return false;
                        }
                        else {
                            _flag = true;
                        }
                    }
                }
            }
            catch
            {
                error = PackageErrorEnum.ExceptionError;
                return false;
            }
            return _flag;
        }

        /// <summary>
        /// 计算Crc
        /// </summary>
        /// 时间：2016-04-13 11:00
        /// 备注：
        public static Byte GetCRC(byte cmdWord, byte[] dataLength, byte[] cmdParam)
        {
            Byte _cal = 0;
            uint _totol = 0;
            _totol += 0x68;
            _totol += cmdWord;
            foreach (Byte b in dataLength)
            {
                _totol += b;
            }

            //用户数据有可能为空（例如心跳包返回）
            if (cmdParam != null)
            {
                foreach (Byte b in cmdParam)
                {
                    _totol += b;
                }
            }
            //溢出不抛出异常，取1个byte
            unchecked
            {
                _cal = (Byte)_totol;
            }

            return _cal;
        }

        /// <summary>
        /// CRC验证码判断
        /// </summary>
        /// 时间：2016-04-13 11:00
        /// 备注：
        private static bool CheckCRC(Byte cmdword, byte[] dataLength, Byte[] cmdparms, Byte crc)
        {
            return GetCRC(cmdword, dataLength, cmdparms) == crc;
        }
    }
}