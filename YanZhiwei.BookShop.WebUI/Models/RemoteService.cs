using System.Threading;
using System.Threading.Tasks;

namespace YanZhiwei.BookShop.WebUI.Models
{
    public class RemoteService
    {
        public async Task<string> GetRemoteDataAsync()
        {
            return await Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(10000);
                return "Hello from the other side of the world";
            });
        }
    }
}