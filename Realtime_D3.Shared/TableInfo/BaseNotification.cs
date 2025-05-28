using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realtime_D3.Shared.TableInfo
{
    public abstract class BaseNotification
    {
        public string table { get; set; } = "";
        public string action { get; set; } = "";
    }
}
