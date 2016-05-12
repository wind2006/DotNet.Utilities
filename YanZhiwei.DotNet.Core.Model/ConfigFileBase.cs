namespace YanZhiwei.DotNet.Core.Model
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 配置文件基类
    /// </summary>
    /// 时间：2015-12-31 9:22
    /// 备注：
    public abstract class ConfigFileBase
    {
        /*
        1. 微软的SQL SERVER提供了两种索引：聚集索引（clustered index，也称聚类索引、簇集索引）和非聚集索引（nonclustered index，也称非聚类索引、非簇集索引）

          我们的汉语字典的正文本身就是一个聚集索引。比如，我们要查“安”字，就会很自然地翻开字典的前几页，因为“安”的拼音是“an”，而按照拼音排序汉字的字典是以英文字母“a”开头并以“z”结尾的，那么“安”字就自然地排在字典的前部。如果您翻完了所有以“a”开头的部分仍然找不到这个字，那么就说明您的字典中没有这个字；同样的，如果查“张”字，那您也会将您的字典翻到最后部分，因为“张”的拼音是“zhang”。也就是说，字典的正文部分本身就是一个目录，您不需要再去查其他目录来找到您需要找的内容。我们把这种正文内容本身就是一种按照一定规则排列的目录称为“聚集索引”。

           如果您认识某个字，您可以快速地从自动中查到这个字。但您也可能会遇到您不认识的字，不知道它的发音，这时候，您就不能按照刚才的方法找到您要查的字，而需要去根据“偏旁部首”查到您要找的字，然后根据这个字后的页码直接翻到某页来找到您要找的字。但您结合“部首目录”和“检字表”而查到的字的排序并不是真正的正文的排序方法，比如您查“张”字，我们可以看到在查部首之后的检字表中“张”的页码是672页，检字表中“张”的上面是“驰”字，但页码却是63页，“张”的下面是 “弩”字，页面是390页。很显然，这些字并不是真正的分别位于“张”字的上下方，现在您看到的连续的“驰、张、弩”三字实际上就是他们在非聚集索引中的排序，是字典正文中的字在非聚集索引中的映射。我们可以通过这种方式来找到您所需要的字，但它需要两个过程，先找到目录中的结果，然后再翻到您所需要的页码。
我们把这种目录纯粹是目录，正文纯粹是正文的排序方式称为“非聚集索引”序方式称为“非聚集索引”

        */

        #region Constructors

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// 时间：2015-12-31 9:23
        /// 备注：
        public ConfigFileBase()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 是否分区索引，所设置分区索引，将验证分区索引名称
        /// </summary>
        public virtual bool ClusteredByIndex
        {
            get; set;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 保存
        /// </summary>
        /// 时间：2015-12-31 9:23
        /// 备注：
        public virtual void Save()
        {
        }

        /// <summary>
        /// 更新配置文件内节点Id
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="nodeList">配置节点结合</param>
        /// 时间：2015-12-31 9:23
        /// 备注：
        public virtual void UpdateNodeList<T>(IEnumerable<T> nodeList)
            where T : ConfigNodeBase
        {
            //重写id(index)
            foreach (T node in nodeList)
            {
                if (node.Id > 0)
                    continue;

                node.Id = nodeList.Max(n => n.Id) + 1;
            }
        }

        #endregion Methods
    }
}