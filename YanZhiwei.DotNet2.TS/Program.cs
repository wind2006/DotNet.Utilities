using System;
using YanZhiwei.DotNet2.TS.BaseLean;

namespace YanZhiwei.DotNet2.TS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Console.WriteLine("===============关于ManualResetEvent开始===============");
            //ManualResetEventLearn.Instance.Demo1();
            //ManualResetEventLearn.Instance.Demo2();
            //ManualResetEventLearn.Instance.Demo3();
            //Console.WriteLine("===============关于ManualResetEvent结束===============");
            //Console.WriteLine("===============关于Monitor开始===============");
            //MonitorLearn.Demo1();
            //MonitorLearn.Demo2();
            //Console.WriteLine("===============关于Monitor结束===============");
            //Console.WriteLine("===============关于ReaderWriterLock开始===============");
            //ReaderWriterLockLearn.Instance.Demo1();
            //Console.WriteLine("===============关于ReaderWriterLock结束===============");
            //Console.WriteLine("===============关于Mutex开始===============");
            //MutexLearn.Demo1();
            //MutexLearn.Demo2();
            //Console.WriteLine("===============关于Mutex结束===============");
            //Console.WriteLine("===============关于Interlocked开始===============");
            //InterlockedLearn.Demo1();
            //Console.WriteLine("===============关于Interlocked结束===============");

            //Console.WriteLine("===============关于ThreadPool开始===============");
            //ThreadPoolLearn.Demo1();
            //ThreadPoolLearn.Demo2();
            //ThreadPoolLearn.Demo3();
            //ThreadPoolLearn.Demo4();
            //ThreadPoolLearn.Demo5();
            //Console.WriteLine("===============关于ThreadPool结束===============");
            //Console.WriteLine("===============关于IComparable开始===============");
            //IComparableLearn.Demo1();
            //Console.WriteLine("===============关于ReadonlyLearn开始===============");
            // ReadonlyLearn _ReadonlyHelper = new ReadonlyLearn(22);
            //ReadonlyLearn _ReadonlyHelper = new ReadonlyLearn(22);
            //_ReadonlyHelper.Show_PIValue();
            //Console.WriteLine("===============关于EqualsLearn开始===============");
            //EqualsLearn _eqaulsHelper = new EqualsLearn();
            //_eqaulsHelper.Demo1();
            //Console.WriteLine("===============关于ParameterPassLearn开始===============");
            //ParameterPassLearn _passParameter = new ParameterPassLearn();
            //int _value = 10;
            //_passParameter.Demo1(_value);
            //Console.WriteLine("传参后的数值：" + _value);
            //_passParameter.Demo2(ref _value);
            //Console.WriteLine("ref 传参后的数值：" + _value);
            //Console.WriteLine("===============关于ObserverPatternLearn开始===============");
            //PetTracker _petTracker = new PetTracker();
            //_petTracker.InstanceTrack();
            //PetTracker _petTracker = new PetTracker();
            //_petTracker.TrackEvent += (new MakerAlertSupplier()).MakeAlert;
            //_petTracker.TrackEvent += (new ShowAlertSupplier()).ShowAlert;
            //_petTracker.InstanceTrack();
            //PetTracker2 _petTracker2 = new PetTracker2();
            //_petTracker2.PetName = "旺财";
            //MakerAlertSupplier2 _alertSupplier = new MakerAlertSupplier2(100);
            //ShowAlertSupplier2 _showSupplier = new ShowAlertSupplier2(101);
            //_petTracker2.TrackEvent += _alertSupplier.MakeAlert;
            //_petTracker2.TrackEvent += _showSupplier.ShowAlert;
            //Console.WriteLine("请输入距离：");
            //_petTracker2.CurDistance = Convert.ToInt16(Console.ReadLine());
            //Console.WriteLine("===============关于BaseThisLearn开始===============");
            //Console.WriteLine("--------base-------------");
            //SubCircle _subCircle = new SubCircle();
            //SubCircle _subCircle2 = new SubCircle(2.2);
            //Console.WriteLine("--------this-------------");
            //SubCircle _subCircle3 = new SubCircle(9);
            //Console.WriteLine("===============关于StrutLearn开始===============");
            //StrutLearn.Demo1();
            //StrutLearn.Demo2();
            //Console.WriteLine("===============关于ClassLearn开始===============");
            //ClassLearn.Demo1();
            //ClassLearn.Demo2();
            //ClassLearn.Demo3();
            //Console.WriteLine("===============关于ThreadLearn_01开始===============");
            //ThreadLearn_01.Demo1();
            //Console.WriteLine("===============关于ThreadLearn_02开始===============");
            //ThreadLearn_02.Demo1();
            Console.WriteLine("===============关于CheckedLearn开始===============");
            CheckedLearn.Demo1();
            Console.ReadLine();
        }
    }
}