using System.Collections.Generic;

namespace YanZhiwei.DotNet4.Framework.Data.Example.Contract
{
    public interface INorthwindService
    {
        Customer GetArticle(int id);

        IEnumerable<Customer> GetArticleList(CustomerRequest request = null);

        void SaveArticle(Customer article);

        void DeleteArticle(List<int> ids);
    }
}