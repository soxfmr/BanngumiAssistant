using BanngumiAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BanngumiAssistant.Providers
{
    public class AnimGardenServiceProvider
    {
        const string ANIM_GARDEN_URL = "http://share.dmhy.org/topics/list";

        const string ANIM_GARDEN_KEYWORD = "keyword";

        protected ProxyInfo mProxyIinfo;

        public AnimGardenServiceProvider() : this(null) {}

        public AnimGardenServiceProvider(ProxyInfo proxyInfo)
        {
            mProxyIinfo = proxyInfo;
        }

        public async Task<string> SearchAsync(string keyword)
        {
            string Ret = null;
            HttpClientHandler clientHandler = null;

            if (mProxyIinfo != null)
            {
                WebProxy proxy = new WebProxy(mProxyIinfo.ToString(), true);
                clientHandler = new HttpClientHandler()
                {
                    Proxy = proxy
                };
            }
            else
            {
                // Default HTTP client without proxy
                clientHandler = new HttpClientHandler();
            }

            try {
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    string baseURL = !string.IsNullOrEmpty(keyword) ?
                            string.Format("{0}?{1}={2}", ANIM_GARDEN_URL, ANIM_GARDEN_KEYWORD, HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(keyword))) : 
                            ANIM_GARDEN_URL;

                    using (HttpResponseMessage response = await client.GetAsync(baseURL))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Ret = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            } catch (System.IO.IOException ex) {
            }

            return Ret;
        }
    }
}
