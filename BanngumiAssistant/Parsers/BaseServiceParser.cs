using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BanngumiAssistant.Models;

namespace BanngumiAssistant.Parsers
{
    public abstract class BaseServiceParser : IServiceParser
    {
        protected CancellationTokenSource cancellationTokenSrc;

        public virtual List<SearchResultItem> Parse(string content)
        {
            throw new NotImplementedException();
        }

        public Task<List<SearchResultItem>> ParseAsync(string content)
        {
            return Task.Run(() =>
            {
                return Parse(content);
            });
        }
    }
}
