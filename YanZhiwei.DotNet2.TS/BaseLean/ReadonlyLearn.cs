using System;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class ReadonlyLearn
    {
        /*
         *参考：
         * readonly 关键字与 const 关键字不同：
         * const 字段只能在该字段的声明中初始化。readonly 字段可以在声明或构造函数中初始化。
         * 因此，根据所使用的构造函数，readonly 字段可能具有不同的值。另外，const 字段是编译时 常数，
         * 而 readonly 字段可用于运行时常数。readonly 只能在声明时或者构造函数里面初始化，并且不能在 static 修饰的构造函数里面。
         */
        public readonly double PI;//只读字段，容易和只读属性混淆，只读属性指只有get块的属性

        //和常量相似，只读字段不能在赋值后改变。然而，和常量不同，赋值给只读字段可以在运行时决定。因此在构造函数作用域范围内给只读字段赋值是合法的
        public readonly DateTime Now = new DateTime(2014, 10, 10);//readonly 能任意类型

        public ReadonlyLearn(double _pi)
        {
            PI = _pi;
        }

        public void ChangePI()
        {
            // PI = 3.14; //只能构造函数或变量初始值指定项中
        }

        public void Show_PIValue()
        {
            Console.WriteLine(PI);
        }
    }
}