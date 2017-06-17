using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanngumiAssistant.Utils
{
    public interface IDriver<T>
    {
        void Set(string key, T value);
        T Get(string key, T defaultValue = default(T));
        bool HasKey(string key);
        void Clear();
    }
}
