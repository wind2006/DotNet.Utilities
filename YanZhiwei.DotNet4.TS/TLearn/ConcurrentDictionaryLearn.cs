using System;
using System.Collections.Concurrent;

namespace YanZhiwei.DotNet4.TS
{
    public class ConcurrentDictionaryLearn
    {
        private static ConcurrentDictionaryLearn instance = null;

        public static ConcurrentDictionaryLearn Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConcurrentDictionaryLearn();
                }
                return instance;
            }
        }

        /*
         * 参考：
         * ConcurrentDictionary<TKey, TValue> 提供了多个便捷的方法，这些方法使代码在尝试添加或移除数据之前无需先检查键是否存在。
         * ConcurrentDictionary<TKey, TValue> 是为多线程方案而设计的。 无需在代码中使用锁定即可在集合中添加或移除项。 但始终有可能出现以下情况：
         * 一个线程检索一个值，而另一个线程通过为同一个键赋予一个新值来立即更新集合。
         * 而且，尽管 ConcurrentDictionary<TKey, TValue> 的所有方法都是线程安全的，但并非所有方法都是原子的，尤其是 GetOrAdd 和 AddOrUpdate。
         * 传递给这些方法的用户委托将在词典的内部锁之外调用。（这样做是为了防止未知代码阻止所有线程。）因此，可能发生以下事件序列：
         * 1) threadA 调用 GetOrAdd，未找到项，通过调用 valueFactory 委托创建要添加的新项。
         * 2) threadB 并发调用 GetOrAdd，其 valueFactory 委托受到调用，并且它在 threadA 之前到达内部锁，并将它的新键-值对添加到词典中。
         * 3) threadA 的用户委托完成，线程到达锁，但现在发现该项已存在
         * 4) threadA 执行“Get”，返回之前由 threadB 添加的数据。
         * 因此，无法保证 GetOrAdd 返回的数据与线程的 valueFactory 创建的数据相同。 调用 AddOrUpdate 时可能发生相似的事件序列。
         */

        public void Demo1()
        {
            try
            {
                ConcurrentDictionary<string, int> _concurrentDictionary = new ConcurrentDictionary<string, int>();
                if (_concurrentDictionary.TryAdd("Yan", 1))
                {
                    _concurrentDictionary.AddOrUpdate("Yan", 2, (key, value) =>
                    {
                        string aa = "1";
                        return 3;
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message.Trim());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}