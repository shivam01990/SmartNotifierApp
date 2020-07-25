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

        public string NotificationTrimMessage
        {
            get
            {
                return (NotificationMessage ?? string.Empty).Length > 150 ? NotificationMessage.Substring(0, 149) + "..." : NotificationMessage;
            }
        }

        public bool IsMessageTrimed
        {
            get
            {
                return (NotificationMessage ?? string.Empty).Length > 150 ? true : false;
            }
        }
        public NotificationType NotificationTypeOf { get; set; }
        public MessageType NotificationMessageType { get; set; }
        public DateTime NotifyOn { get; set; }

    }
}
