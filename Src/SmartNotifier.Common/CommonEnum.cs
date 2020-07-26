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
        DiskSpaceNotification,
        ServiceAndProcesses,
        ApplicationEventLog,
        GeneralNotification
    }

    public enum MessageType
    {
        Warninig,
        Information,
        Error
    }

    public enum ProcessStatus
    {
        Running,
        Stopped,
        NotResponding,
        NA
    }

    public enum EventType
    {
        Error,
        CriticalError
    }
}
