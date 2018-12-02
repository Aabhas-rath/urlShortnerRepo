using CutURL.Entities;
using System.Threading.Tasks;

namespace CutURL.BusinessLayer
{
    public interface IURLManager
    {
        Task<URLDetails> ShortenUrl(string longUrl, string ip, string customUrl = "");
        Task<Statistics> Click(string customUrl);
        Task<URLDetails> FetchStatistics(string customUrl);
    }
}
