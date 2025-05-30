using Realtime_D3.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realtime_D3.Shared.TableInfo
{
    public class CompleteEntreeHistoryNotification : BaseNotification
    {
        public EntreeLogNotificationData data { get; set; } = new EntreeLogNotificationData();
    }
}
