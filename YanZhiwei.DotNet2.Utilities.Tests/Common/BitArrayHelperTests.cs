using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace YanZhiwei.DotNet2.Utilities.Common.Tests
{
    [TestClass()]
    public class BitArrayHelperTests
    {
        [TestMethod()]
        public void ToBinaryStringTest()
        {
            Assert.AreEqual("00000000", BitArrayHelper.ToBinaryString(new BitArray(8)));
        }

        [TestMethod()]
        public void ToBytesTest()
        {
            BitArray _array = new BitArray(8);
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = true;
            }
            byte[] _expected = new byte[1] { 0xFF };
            CollectionAssert.AreEqual(_expected, BitArrayHelper.ToBytes(_array));
        }

        [TestMethod()]
        public void ReverseTest()
        {
            BitArray _array = new BitArray(8);
            for (int i = 0; i < 5; i++)
            {
                _array[i] = true;
            }
            byte[] _expected = new byte[1] { 0x1F };
            CollectionAssert.AreEqual(_expected, BitArrayHelper.Reverse(_array).ToBytes());
        }
    }
}