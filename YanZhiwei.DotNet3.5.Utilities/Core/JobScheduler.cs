namespace YanZhiwei.DotNet3._5.Utilities.Core
{
    using System.Collections;

    using YanZhiwei.DotNet2.Utilities.Core;

    /// <summary>
    /// 任务管理中心
    /// 使用它可以管理一个或则多个同时运行的任务
    /// </summary>
    public static class JobScheduler
    {
        #region Fields

        /*
         *参考：
         *1. http://www.cnblogs.com/Googler/archive/2010/06/05/1752213.html
         */
        /// <summary>
        /// 任务线程安全集合
        /// </summary>
        private static ThreadSafeList<Job> taskScheduler;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static JobScheduler()
        {
            taskScheduler = new ThreadSafeList<Job>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 任务总数
        /// </summary>
        public static int Count
        {
            get { return taskScheduler.Count; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="task">Task</param>
        public static void DeRegister(Job task)
        {
            lock (taskScheduler)
            {
                taskScheduler.Remove(task);
            }
        }

        /// <summary>
        /// 查找任务
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <returns>Task</returns>
        public static Job Find(string name)
        {
            return taskScheduler.Find(task => task.Name == name);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator</returns>
        public static IEnumerator GetEnumerator()
        {
            return taskScheduler.GetEnumerator();
        }

        /// <summary>
        /// 注册任务
        /// </summary>
        /// <param name="task">Task</param>
        public static void Register(Job task)
        {
            lock (taskScheduler)
            {
                taskScheduler.Add(task);
            }
        }

        /// <summary>
        /// 终止任务
        /// </summary>
        public static void TerminateAllTask()
        {
            lock (taskScheduler)
            {
                taskScheduler.ForEach(task => task.Close());
                taskScheduler.Clear();
                taskScheduler.TrimExcess();
            }
        }

        #endregion Methods
    }
}