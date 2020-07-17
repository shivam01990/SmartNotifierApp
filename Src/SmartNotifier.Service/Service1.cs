using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace SmartNotifier.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class Service1 : IService1
    {
        public DateTime? GetLastRestartTime()
        {
            // define a select query

            SelectQuery query = new SelectQuery("SELECT LastBootUpTime FROM Win32_OperatingSystem WHERE Primary = 'true'");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            DateTime? dtBootTime= null;
            foreach (ManagementObject mo in searcher.Get())
            {

                dtBootTime = ManagementDateTimeConverter.ToDateTime(mo.Properties["LastBootUpTime"].Value.ToString());
            }

            return dtBootTime;
        }

        public void MonitorNotifications()
        {
            //for (int i = 0; i < 2; i++)
            //{
            //    Thread.Sleep(3000);
            //    OperationContext.Current.GetCallbackChannel<IServiceCallback>().NotifyNotificationBack("Test Message" + i);
            //}
        }
    }
}
