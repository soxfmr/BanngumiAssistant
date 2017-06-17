using BanngumiAssistant.Models;
using BanngumiAssistant.Parsers;
using BanngumiAssistant.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BanngumiAssistant.Controllers
{
    public class MainController
    {
        private AnimGardenParser parser;
        private AnimGardenServiceProvider provider;

        public MainController()
        {
            parser = new AnimGardenParser();
            provider = new AnimGardenServiceProvider();
        }

        public async Task<List<SearchResultItem>> GetIndexAsync()
        {
            return await GetResultAsync(null);
        }

        public async Task<List<SearchResultItem>> GetResultAsync(string keyword)
        {
            List<SearchResultItem> Ret = null;

            string content = await provider.SearchAsync(keyword);
            if (! string.IsNullOrEmpty(content))
            {
                Ret = await parser.ParseAsync(content);
                if (! string.IsNullOrEmpty(keyword) && Ret != null)
                {
                    Ret = Ret.Where(item =>
                    {
                        return Regex.Match(item.Title, keyword, RegexOptions.Multiline).Success;
                    }).ToList();
                }

            }

            return Ret;
        }
    }
}
