using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.Common
{
    public class ConsoleEventLogs
    {
        public int EventID { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? CreatedOn { get; set; }

        public EventType EventType { get; set; }
    }
}
