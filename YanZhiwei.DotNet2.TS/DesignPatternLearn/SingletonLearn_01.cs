namespace YanZhiwei.DotNet2.TS.DesignPatternLearn
{
    /// <summary>
    /// 即时加载的单例模式
    /// </summary>
    public class SingletonLearn_01
    {
        private static SingletonLearn_01 instance;

        public static SingletonLearn_01 Instance
        {
            get
            {
                if (instance == null)
                    instance = new SingletonLearn_01();
                return instance;
            }
        }
    }
}