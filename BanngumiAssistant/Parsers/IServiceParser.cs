using BanngumiAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanngumiAssistant.Parsers
{
    public interface IServiceParser
    {
        List<SearchResultItem> Parse(string content);
    }
}
