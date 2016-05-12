namespace YanZhiwei.DotNet3._5.Interfaces
{
    using System;

    /// <summary> 
    /// 计划的接口 
    /// </summary> 
    public interface ISchedule
    {
        #region Properties

        /// <summary> 
        /// 初始化执行时间于现在时间的时间刻度差【毫秒】
        /// </summary> 
        long DueTime
        {
            get;
        }

        /// <summary> 
        /// 最初计划执行时间 
        /// </summary> 
        DateTime ExecutionTime
        {
            get;
        }

        /// <summary> 
        /// 循环的周期 
        /// </summary> 
        long Period
        {
            get;
        }

        #endregion Properties
    }
}