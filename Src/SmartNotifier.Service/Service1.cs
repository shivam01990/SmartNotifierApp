using SmartNotifier.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public void MonitorNotifications()
        {
            //for (int i = 0; i < 2; i++)
            //{
            //    Thread.Sleep(3000);
            //    OperationContext.Current.GetCallbackChannel<IServiceCallback>().NotifyNotificationBack("Test Message" + i);
            //}
        }

        public DateTime? GetLastRestartTime()
        {
            DateTime? dtBootTime = null;
            try
            {
                // define a select query
                SelectQuery query = new SelectQuery("SELECT LastBootUpTime FROM Win32_OperatingSystem WHERE Primary = 'true'");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject mo in searcher.Get())
                {
                    dtBootTime = ManagementDateTimeConverter.ToDateTime(mo.Properties["LastBootUpTime"].Value.ToString());
                }
            }
            catch
            { }

            return dtBootTime;
        }

        public SystemInforamtion GetSystemInforamtion()
        {
            SystemInforamtion infoObj = new SystemInforamtion();

            try
            {
                // define a select query
                SelectQuery query = new SelectQuery("SELECT TotalPhysicalMemory, Model, SystemType FROM Win32_ComputerSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject mo in searcher.Get())
                {
                    infoObj.RAM = mo.Properties["TotalPhysicalMemory"].Value.ToString();
                    infoObj.SystemModel = mo.Properties["Model"].Value.ToString();
                    infoObj.SystemType = mo.Properties["SystemType"].Value.ToString();
                }
            }
            catch
            { }

            return infoObj;
        }

        public DriveInformation GetDriveInforamtion()
        {
            DriveInformation driveinfo = new DriveInformation();
            DriveInfo cdrive = new DriveInfo("C");
            if (DriveInfo.GetDrives().Any(x => x.Name == @"C:\"))
            {
                driveinfo.CTotalFreeSpace = cdrive.TotalFreeSpace / (1024 * 1024 * 1024);
                driveinfo.CTotalSize = cdrive.TotalSize / (1024 * 1024 * 1024);
            }

            DriveInfo ddrive = new DriveInfo("D");
            if (DriveInfo.GetDrives().Any(x => x.Name == @"D:\"))
            {
                driveinfo.DTotalFreeSpace = ddrive.TotalFreeSpace / (1024 * 1024 * 1024);
                driveinfo.DTotalSize = ddrive.TotalSize / (1024 * 1024 * 1024);
            }
            return driveinfo;
        }

        public List<ConsoleProcesses> GetConsoleProcesses()
        {
            //get a list of all running processes on current system
            Process[] systemprocesses = Process.GetProcesses();
            List<ConsoleProcesses> consoleprocess = ConsoleProcesses.GetConsoleProcesses();
            foreach (var process in consoleprocess)
            {
                process.Status = ConsoleProcesses.GetProcessStatus(process.ActualProcessName, systemprocesses);
            }

            return consoleprocess;
        }
    }
}
