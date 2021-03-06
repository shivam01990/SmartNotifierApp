﻿using SmartNotifier.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SmartNotifier.Service
{
    //[ServiceContract(CallbackContract = typeof(IServiceCallback))]
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        DateTime? GetLastRestartTime();

        [OperationContract]
        SystemInforamtion GetSystemInforamtion();

        [OperationContract]
        DriveInformation GetDriveInforamtion();

        [OperationContract]
        List<ConsoleProcesses> GetConsoleProcesses();

        [OperationContract]
        ConsoleInformation GetConsoleInformation();

        [OperationContract]
        List<ConsoleEventLogs> GetConsoleEventLogInformation(int loginterval);
    }

    public interface IServiceCallback
    {
        [OperationContract]
        void NotifyNotificationBack(string Message);
    }
}
