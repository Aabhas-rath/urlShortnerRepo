using System;
using System.Collections.Generic;
using CutURL.Entities;
using CutURL.DatabaseLayer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using System.Text.RegularExpressions;

namespace CutURL.BusinessLayer
{
    public class URLManager:IURLManager
    {
        /// <summary>
        /// This Function Redirects oncoming connections to the Orignal Urls as mapped in the DB. also stores some information along the way
        /// </summary>
        /// <param name="customUrl">Short Url in question</param>
        /// <returns>Stastics Object</returns>
        public Task<Statistics> Click(string customUrl)
        {
            return Task.Run(() =>
            {
                using (DBContext ctx = new DBContext())
                {
                    #region Checks if Mapping present in DB

                    URLDetails url = ctx.URLDetails.Where(u => u.CustomUrl == customUrl).FirstOrDefault();
                    if (url == null)
                    {
                        throw new ShortUrlNotFoundException();
                    }
                    #endregion

                    #region increments Clicks counter
                    url.ClickCounter = url.ClickCounter + 1;

                    #endregion

                    Statistics stat = new Statistics()
                    {
                        ClickDate = DateTime.Now,
                        ShortUrl = url
                    };

                    ctx.Statistics.Add(stat);

                    ctx.SaveChanges();

                    return stat;
                }
            });
        }

        /// <summary>
        /// Driving Function of the Process, Validates Orignal URL and Custom URL(If present) and assigns a suitable short Url
        /// </summary>
        /// <param name="orignalUrl">Long URL in question</param>
        /// <param name="ip">Ip of the request</param>
        /// <param name="customUrl">Custom URL if present</param>
        /// <returns>URLDetails Object</returns>
        public Task<URLDetails> ShortenUrl(string orignalUrl,string ip, string customUrl = "")
        {
            return Task.Run(() =>
            {
                using (var ctx = new DBContext())
                {
                    URLDetails url;

                    #region Check If Orignal Url Present in DB if Present give the last short url

                    url = ctx.URLDetails.Where(u => u.OrignalUrl == orignalUrl).FirstOrDefault();
                    if (url != null)
                    {
                        return url;
                    }

                    #endregion
                    #region Validation for Orignal URL

                    if (!orignalUrl.StartsWith("http://") && !orignalUrl.StartsWith("https://"))
                    {
                        throw new ArgumentException("Invalid URL format");
                    }
                    Uri urlCheck = new Uri(orignalUrl);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCheck);
                    request.Timeout = 10000;
                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (Exception)
                    {
                        throw new ShortUrlNotFoundException();
                    }

                    #endregion

                    #region Custom URL Process
                    if (!string.IsNullOrEmpty(customUrl))
                    {
                        if (ctx.URLDetails.Where(u => u.CustomUrl == customUrl).Any())
                        {
                            throw new ShorturlConflictException();
                        }
                        if (customUrl.Length > 20 || !Regex.IsMatch(customUrl, @"^[A-Za-z\d_-]+$"))
                        {
                            throw new ArgumentException("Malformed or too long segment");
                        }
                    }
                    else
                    {
                        customUrl = this.NewUrl();
                    }
                    #endregion

                    if (string.IsNullOrEmpty(customUrl))
                    {
                        throw new ArgumentException("Segment is empty");
                    }

                    url = new URLDetails()
                    {
                        CreatedOn = DateTime.Now,
                        OrignalUrl = orignalUrl,
                        Ip = ip,
                        ClickCounter = 0,
                        CustomUrl = customUrl
                    };

                    ctx.URLDetails.Add(url);

                    ctx.SaveChanges();

                    return url;
                }
            });
        }

        /// <summary>
        /// Fetches the Statictics for a link created before or just now
        /// </summary>
        /// <param name="customUrl">Link Segment in question</param>
        /// <returns>URLDetail Object containing information</returns>
        public Task<URLDetails> FetchStatistics(string customUrl)
        {
            return Task.Run( () => 
            {
                using (DBContext ctx = new DBContext())
                {
                    URLDetails details = ctx.URLDetails.Where(u => u.CustomUrl == customUrl).FirstOrDefault();
                    if (details == null)
                    {
                        throw new ShortUrlNotFoundException();
                    }
                    return details;
                }
            });
        }

        /// <summary>
        /// Creates a new short URl Segment which is unique and mapped to Long URL in DB
        /// </summary>
        /// <returns></returns>
        private string NewUrl()
        {
            using (var ctx = new DBContext())
            {
                int i = 0;
                while (true)
                {
                    string CustomUrl = Guid.NewGuid().ToString().Substring(0, 6);
                    if (!ctx.URLDetails.Where(u => u.CustomUrl == CustomUrl).Any())
                    {
                        return CustomUrl;
                    }
                    if (i > 30)
                    {
                        break;
                    }
                    i++;
                }
                return string.Empty;
            }
        }
    }
}
