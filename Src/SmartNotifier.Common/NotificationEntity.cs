using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.Common
{
    public class NotificationEntity
    {
        public string NotificationMessage { get; set; }
        public NotificationType NotificationTypeOf { get; set; }
        public MessageType NotificationMessageType { get; set; }
        public DateTime NotifyOn { get; set; }

    }
}
