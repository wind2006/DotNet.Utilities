namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Collections;
    using System.Text;

    /// <summary>
    /// BitArray 帮助类
    /// </summary>
    public static class BitArrayHelper
    {
        #region Methods

        /// <summary>
        /// 逆序
        /// </summary>
        /// <param name="bits">需要操作的BitArray.</param>
        /// <returns>逆序后的BitArray</returns>
        public static BitArray Reverse(this BitArray bits)
        {
            int _length = bits.Length;
            int _mid = _length / 2;
            for (int i = 0; i < _mid; i++)
            {
                bool _bit = bits[i];
                bits[i] = bits[_length - i - 1];
                bits[_length - i - 1] = _bit;
            }

            return bits;
        }

        /// <summary>
        /// 转换成十六进制字符串
        /// </summary>
        /// <param name="bits">需要操作的BitArray</param>
        /// <param name="trueValue">当条件成立的值</param>
        /// <param name="falseValue">当条件不成立的值</param>
        /// <returns>十六进制字符串</returns>
        public static string ToBinaryString(this BitArray bits, char trueValue, char falseValue)
        {
            StringBuilder _builder = new StringBuilder();
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i])
                {
                    _builder.Append(trueValue);
                }
                else
                {
                    _builder.Append(falseValue);
                }
            }

            string _bitArrayString = _builder.ToString();
            return _bitArrayString;
        }

        /// <summary>
        /// 转成是十六进制字符串
        /// </summary>
        /// <param name="bits">需要操的在BitArray</param>
        /// <returns>十六进制字符串</returns>
        public static string ToBinaryString(this BitArray bits)
        {
            return bits.ToBinaryString('1', '0');
        }

        /// <summary>
        /// 转成成byte数组
        /// </summary>
        /// <param name="bits">需要操的在BitArray</param>
        /// <returns>byte数组</returns>
        public static byte[] ToBytes(this BitArray bits)
        {
            int _length = bits.Count / 8;
            if (bits.Count % 8 != 0)
            {
                _length++;
            }

            byte[] _bytes = new byte[_length];
            int _byteIndex = 0, _bitIndex = 0;
            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                {
                    _bytes[_byteIndex] |= (byte)(1 << (7 - _bitIndex));
                }

                _bitIndex++;
                if (_bitIndex == 8)
                {
                    _bitIndex = 0;
                    _byteIndex++;
                }
            }

            return _bytes;
        }

        #endregion Methods
    }
}