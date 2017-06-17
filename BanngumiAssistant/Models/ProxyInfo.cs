using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanngumiAssistant.Models
{
    public class ProxyInfo
    {
        public string Address;

        public short Port;

        public override string ToString()
        {
            return string.Format("{}:{}", Address, Port);
        }
    }
}
