﻿using System;
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
        void MonitorNotifications();

        [OperationContract]
        DateTime? GetLastRestartTime();

    }

    public interface IServiceCallback
    {
        [OperationContract]
        void NotifyNotificationBack(string Message);
    }
}