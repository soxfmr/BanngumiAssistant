using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanngumiAssistant.Models;
using System.Text.RegularExpressions;

namespace BanngumiAssistant.Parsers
{
    public class AnimGardenParser : BaseServiceParser
    {
        public override List<SearchResultItem> Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            Match matchBody = Regex.Match(content, "<tbody>(.+?)</tbody>", RegexOptions.Singleline | 
                RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (matchBody == null || ! matchBody.Success)
            {
                return null;
            }

            SearchResultItem item;
            List<SearchResultItem> Ret = new List<SearchResultItem>();

            Regex pattern = new Regex("<a.*?target=\"_blank\" >(?<title>.+?)</a>.*?<a class=\"download-arrow arrow-magnet\".*?href=\"(?<magnet>.+?)\"",
                RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match match in pattern.Matches(matchBody.Groups[1].Value))
            {
                item = new SearchResultItem();
                item.Title = RemoveHighlight(match.Groups["title"].Value).Trim();
                item.MagnetLink = match.Groups["magnet"].Value.Trim();
                Ret.Add(item);
            }

            return Ret;
        }

        private string RemoveHighlight(string value)
        {
            return Regex.Replace(value, "<.*?>", String.Empty);
        }
    }
}
