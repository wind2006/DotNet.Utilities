using System;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class UnCheckedLearn
    {
        public static void Demo1()
        {
            int _result = unchecked(int.MaxValue * 2);
            Console.WriteLine(_result);
            //unchecked关键字用来忽略溢出异常
            //生成==>高级==》'检查运算上溢/下溢'
        }

        public static void Demo2()
        {
            /*
             *   class Person2
             *   {
             *   public string Name { get; set; }
             *   public string Title { get; set; }
             *   public override int GetHashCode()
             *   {
             *   return unchecked(Name.GetHashCode() + Title.GetHashCode());
             *   }
             *   }
             */
            /*
             * 有时候我们不需要准确的计算结果，我们只是需要那么一个数而已，至于溢出不溢出的关系不大，比如说生成一个对象的HashCode，比如说根据一个算法计算出一个相对随机数，这都是不需要准确结果的。如下代码片段
             */
        }
    }
}