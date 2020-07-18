using SmartNotifier.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SmartNotifier.View.DB
{
    public class NotifierDB
    {
        private static NotifierDB instance = null;
        private static readonly object padlock = new object();

        NotifierDB()
        { }
        public static NotifierDB Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NotifierDB();
                    }
                    return instance;
                }
            }
        }

        public List<string> NotificationQueue = new List<string>();
        public void InitializeDB()
        {
            dbworker_Tick(null, null);
            DispatcherTimer dbworker = new DispatcherTimer();
            dbworker.Interval = TimeSpan.FromSeconds(20);
            dbworker.Tick += dbworker_Tick;
            dbworker.Start();
        }

        private void dbworker_Tick(object sender, EventArgs e)
        {
            //Check Last Restart Time
            if (NotifierDB.Instance.LastRestartTime == null)
            {
                NotifierDB.Instance.LastRestartTime = SmartNotifierHelper.Instance.ServiceInstance.GetLastRestartTime();
                if (NotifierDB.Instance.LastRestartTime != null && NotifierDB.Instance.LastRestartedTimeSpan.TotalHours > 0)
                {
                    NotificationQueue.Add("Console last restart time was " + NotifierDB.Instance.LastRestartTime + ". It is recommend to restart the machine for better performance.");
                }
            }

            if (NotifierDB.Instance.SystemInfo == null)
            {
                SystemInfo = SmartNotifierHelper.Instance.ServiceInstance.GetSystemInforamtion();
            }

            DriveDetails = SmartNotifierHelper.Instance.ServiceInstance.GetDriveInforamtion();

            if (DriveDetails != null)
            {
                string drivespaceNotification = string.Empty;
                if (DriveDetails.CTotalFreeSpace < 100 && DriveDetails.CTotalSize > DriveDetails.CTotalFreeSpace)
                {
                    drivespaceNotification = Environment.NewLine + " C: " + DriveDetails.CTotalFreeSpace + " GB" + Environment.NewLine;
                }
                if (DriveDetails.DTotalFreeSpace < 100 && DriveDetails.DTotalSize > DriveDetails.DTotalFreeSpace)
                {
                    drivespaceNotification = Environment.NewLine + " D: " + DriveDetails.CTotalFreeSpace + " GB" + Environment.NewLine;
                }
                if (drivespaceNotification.Length > 0)
                {
                    NotificationQueue.Add("Drive free space is less." + drivespaceNotification + "Please cleanup the space");
                }
            }
        }

        public DriveInformation DriveDetails { get; set; } = null;

        public SystemInforamtion SystemInfo { get; set; } = null;

        public DateTime? LastRestartTime { get; set; } = null;

        public TimeSpan LastRestartedTimeSpan
        {
            get
            {
                if (NotifierDB.Instance.LastRestartTime != null)
                {
                    return DateTime.Now - (DateTime)NotifierDB.Instance.LastRestartTime;
                }

                return TimeSpan.Zero;
            }
        }




    }
}
