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
    //NotifierDB Pub Sub Model
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

        public Queue<NotificationEntity> NewNotificationQueue = new Queue<NotificationEntity>();
        public Queue<NotificationEntity> OldNotificationQueue = new Queue<NotificationEntity>();
        #region Events
        public EventHandler NotifyMessageExecutionHandler;
        #endregion

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

        public void InitializeDB()
        {
            dbworker_Tick(null, null);
            DispatcherTimer dbworker = new DispatcherTimer();
            dbworker.Interval = TimeSpan.FromSeconds(20);
            dbworker.Tick += dbworker_Tick;
            dbworker.Start();
        }

        private void AddMessageToNotificationQueue(NotificationEntity notificationParam, bool isValidateRepeateNotification)
        {
            while (OldNotificationQueue.Where(x => (x.NotifyOn) < DateTime.Now.AddHours(-AppSettings.ValidateRepeateNotificationTime)).Count() > 0)
            {
                OldNotificationQueue.Dequeue();
            }

            if (isValidateRepeateNotification)
            {
                if (OldNotificationQueue.Where(x => x.NotificationTypeOf == notificationParam.NotificationTypeOf).Count() == 0)
                {
                    NewNotificationQueue.Enqueue(notificationParam);
                    OldNotificationQueue.Enqueue(notificationParam);
                }
            }
            else
            {
                NewNotificationQueue.Enqueue(notificationParam);
                OldNotificationQueue.Enqueue(notificationParam);
            }
        }

        private void dbworker_Tick(object sender, EventArgs e)
        {
            //Check Last Restart Time
            ValidateLastRestart();
            ValidateDriveDetails();
        }

        private void ValidateDriveDetails()
        {
            DriveDetails = SmartNotifierHelper.Instance.ServiceInstance.GetDriveInforamtion();

            if (DriveDetails != null)
            {
                string drivespaceNotification = string.Empty;
                if (DriveDetails.CTotalFreeSpace < AppSettings.ThresholdSpaceForNotificationAlert && DriveDetails.CTotalSize > DriveDetails.CTotalFreeSpace)
                {
                    drivespaceNotification = Environment.NewLine + " C: " + DriveDetails.CTotalFreeSpace + " GB" + Environment.NewLine;
                }
                if (DriveDetails.DTotalFreeSpace < AppSettings.ThresholdSpaceForNotificationAlert && DriveDetails.DTotalSize > DriveDetails.DTotalFreeSpace)
                {
                    drivespaceNotification = Environment.NewLine + " D: " + DriveDetails.DTotalFreeSpace + " GB" + Environment.NewLine;
                }
                if (drivespaceNotification.Length > 0)
                {
                    drivespaceNotification = "Drive free space is less." + drivespaceNotification + "Please cleanup the space";
                    AddMessageToNotificationQueue(
                        new NotificationEntity()
                        {
                            NotificationMessage = drivespaceNotification,
                            NotificationMessageType = MessageType.Warninig,
                            NotificationTypeOf = NotificationType.DiskSpaceNotification,
                            NotifyOn = DateTime.Now
                        }, true);
                }
            }
        }

        private void ValidateLastRestart()
        {
            if (NotifierDB.Instance.LastRestartTime == null)
            {
                NotifierDB.Instance.LastRestartTime = SmartNotifierHelper.Instance.ServiceInstance.GetLastRestartTime();                
            }

            if (NotifierDB.Instance.LastRestartTime != null && NotifierDB.Instance.LastRestartedTimeSpan.TotalHours > AppSettings.ValidateRestartTime)
            {
                string restartmessage = "Console last restart time was " + NotifierDB.Instance.LastRestartTime + ". " + Environment.NewLine +
                    "It is recommend to restart the machine for better performance.";
                AddMessageToNotificationQueue(
                    new NotificationEntity()
                    {
                        NotificationMessage = restartmessage,
                        NotificationMessageType = MessageType.Warninig,
                        NotificationTypeOf = NotificationType.RestartNotification,
                        NotifyOn = DateTime.Now
                    }, true);
            }

            if (NotifierDB.Instance.SystemInfo == null)
            {
                SystemInfo = SmartNotifierHelper.Instance.ServiceInstance.GetSystemInforamtion();
            }

        }



    }
}
