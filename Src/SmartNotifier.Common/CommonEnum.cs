using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.Common
{
    public enum NotificationType
    {
        RestartNotification,
        DiskSpaceNotification
    }

    public enum MessageType
    {
        Warninig,
        Information,
        Error
    }
}
