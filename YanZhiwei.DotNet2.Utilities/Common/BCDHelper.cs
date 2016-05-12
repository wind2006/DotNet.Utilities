namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Text;

    /// <summary>
    /// BCD码帮助类
    /// </summary>
    public static class BCDHelper
    {
        #region Methods

        /*
         *知识：
         *1.BCD码（二到十进制编码）
         *把十进制数的每一位分别写成二进制形式的编码，称为二进制编码的十进制数，
         *即二到十进制编码或BCD（Binary Coded Decimal）编码。
         *用四位二进制表示一位十进制会多出6种状态，这些多余状态码称为BCD码中的非法码。
         *BCD码与二进制之间的转换不是直接进行的，当需要将BCD码转换成二进制码时，要先将BCD码转换成十进制码，然后再转换成二进制码；
         *当需要将二进制转换成BCD码时，要先将二进制转换成十进制码，然后再转换成BCD码。
         *编码过程，将数字69进行BCD编码（注：BCD编码低位在前，后面将不再注释）。
         *BCD码各位的数值范围为2#0000～2#1001，对应于十进制数0～9。BCD码不能使用十六进制的A~F（2#1010～2#1111）这6个数字。
         *BCD码本质上是十进制数，因此相邻两位逢十进一。
         *1.1 BCD码编码方法很多，通常采用8421编码，这种编码方法最自然简单。
         *    其方法使用四位二进制数表示一位十进制数，从左到右每一位对应的权分别是
         *    23、22、21、20，即8、4、2、1。
         *    例如十进制数1975的8421码可以这样得出1975（D）=0001 1001 0111 0101（BCD）
         *
         *BCD码：用四个二进制位表示一个十进制数字；最常用的是8421 BCD码；
          压缩型BCD码：一个字节可存放一个两位十进制数，其中高四位存放十位数字，低四位存放个位数字。
          如：56的压缩型8421 BCD码是0101 0110；
          非压缩型BCD码：一个字节可存放一个一位十进制数，其中高字节为0，低字节的低四位存放个位。
          如：5的非压缩型BCD码是0000 0101，必须存放在一个字节中，56的非压缩型BCD码是*********** ***********，必须存放在一个字中。

         *1.3 编码过程，将数字69进行BCD编码（注：BCD编码低位在前，后面将不再注释）。
         *1.3.1 将6，9分别转换成二进制表示：6（00000110）9（00001001），大家可以看到，最大的数字9也只要4个位，在传输过程中白白浪费了4个位；
         *1.3.2 将69合并为一个字节，分别取6，9二进制编码的低4位，按照低位在前的原则，将9的低四位放前面6的低四位放后面得出新的字节二进制编码是10010110；
         *1.3.3 完成编码过程，69的BCD编码结果为10010110。
         *
         *1.4解码过程：将69的BCD码10010110进行解码。
         *1.4.1 将10010110的高4位与低4位拆分开，得到两个二进制数1001和0110；
         *1.4.2 分别将1001和0110的前面补充4位0000得到两个8位的二进制数00001001，00000110；
         *1.4.3 因为编码时低位在前，所以我们将两个二进制数编排顺序为00000110 000010001；
         *参考：
         *2.1 http://stackoverflow.com/questions/7107026/converting-an-int-to-a-bcd-byte-array
         *2.2 http://www.emoticode.net/c-sharp/convert-bytes-into-a-binary-coded-decimal-bcd-string.html
         *2.3 http://www.emoticode.net/c-sharp/convert-a-binary-coded-decimal-bcd-string-into-bytes.html
         *
         *
         */

        /// <summary>
        /// 转为bcd码Byte描述
        /// 其中高四位存放十位数字，低四位存放个位数字。
        /// </summary>
        /// <param name="bcdNumber">数字</param>
        /// <returns>Byte描述</returns>
        public static byte Parse8421BCDNumber(this int bcdNumber)
        {
            byte _bcd = (byte)(bcdNumber % 10);
            bcdNumber /= 10;
            _bcd |= (byte)((bcdNumber % 10) << 4);
            bcdNumber /= 10;
            return _bcd;
        }

        /// <summary>
        /// Int转为bcd码Byte数组描述
        /// </summary>
        /// <param name="bcdNumber">数字</param>
        /// <param name="isLittleEndian">是否低位在前</param>
        /// <returns>Byte数组</returns>
        public static byte[] Parse8421BCDNumber(this int bcdNumber, bool isLittleEndian)
        {
            string _bcdString = bcdNumber.ToString();
            if (!_bcdString.IsBinaryCodedDecimal())
            {
                _bcdString = _bcdString.PadLeft(_bcdString.Length + 1, '0');
            }

            return Parse8421BCDString(_bcdString, isLittleEndian);
        }

        /// <summary>
        /// 字符串转为bcd码Byte数组描述
        /// <para>eg:CollectionAssert.AreEqual(new byte[2] { 0x01, 0x10 }, BCDHelper.ToBinaryCodedDecimal("0110", false));</para>
        /// <para>eg:CollectionAssert.AreEqual(new byte[2] { 0x10, 0x01 }, BCDHelper.ToBinaryCodedDecimal("0110", true));</para>
        /// </summary>
        /// <param name="bcdString">bcd字符串</param>
        /// <param name="isLittleEndian">是否低位在前高位在后</param>
        /// <returns>Byte数组</returns>
        public static byte[] Parse8421BCDString(this string bcdString, bool isLittleEndian)
        {
            byte[] _bytes = null;
            if (bcdString.IsBinaryCodedDecimal())
            {
                char[] _chars = bcdString.ToCharArray();
                int _len = _chars.Length / 2;
                _bytes = new byte[_len];
                if (isLittleEndian)
                {
                    for (int i = 0; i < _len; i++)
                    {
                        byte _highNibble = byte.Parse(_chars[2 * (_len - 1) - 2 * i].ToString());
                        byte _lowNibble = byte.Parse(_chars[2 * (_len - 1) - 2 * i + 1].ToString());
                        _bytes[i] = (byte)((byte)(_highNibble << 4) | _lowNibble);
                    }
                }
                else
                {
                    for (int i = 0; i < _len; i++)
                    {
                        byte _highNibble = byte.Parse(_chars[2 * i].ToString());
                        byte _lowNibble = byte.Parse(_chars[2 * i + 1].ToString());
                        _bytes[i] = (byte)((byte)(_highNibble << 4) | _lowNibble);
                    }
                }
            }

            return _bytes;
        }

        /// <summary>
        /// 将byte数组转为BCD字符串描述
        /// <para>eg: Assert.AreEqual("1001", BCDHelper.ToBinaryCodedDecimal(new byte[2] { 0x01, 0x10 }, true));</para>
        /// <para>eg: Assert.AreEqual("0110", BCDHelper.ToBinaryCodedDecimal(new byte[2] { 0x01, 0x10 }, false));</para>
        /// </summary>
        /// <param name="data">Byte数组</param>
        /// <param name="isLittleEndian">是否低位在前高位在后</param>
        /// <returns>BCD描述</returns>
        public static string To8421BCDString(this byte[] data, bool isLittleEndian)
        {
            StringBuilder _builder = new StringBuilder(data.Length * 2);
            if (isLittleEndian)
            {
                for (int i = data.Length - 1; i >= 0; i--)
                {
                    byte _bcdByte = data[i];
                    int _idHigh = _bcdByte >> 4;
                    int _idLow = _bcdByte & 0x0F;
                    _builder.Append(string.Format("{0}{1}", _idHigh, _idLow));
                }
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    byte _bcdByte = data[i];
                    int _idHigh = _bcdByte >> 4;
                    int _idLow = _bcdByte & 0x0F;
                    _builder.Append(string.Format("{0}{1}", _idHigh, _idLow));
                }
            }

            return _builder.ToString();
        }

        /// <summary>
        ///将byte数组转为BCD字符串描述
        /// </summary>
        /// <param name="data">Byte数组</param>
        /// <param name="startIndex">目标数组开始索引</param>
        /// <param name="endIndex">目标数组的结束索引</param>
        /// <param name="isLittleEndian">是否低位在前高位在后</param>
        /// <returns>BCD描述</returns>
        /// 时间：2016-04-14 15:42
        /// 备注：
        public static string To8421BCDString(this byte[] data, int startIndex, int endIndex, bool isLittleEndian)
        {
            byte[] _array = ArrayHelper.Copy(data, startIndex, endIndex);
            return To8421BCDString(_array, isLittleEndian);
        }

        #endregion Methods
    }
}