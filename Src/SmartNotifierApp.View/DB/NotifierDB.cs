using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            BackgroundWorker dbworker = new BackgroundWorker();
            dbworker.DoWork += new DoWorkEventHandler(ProcessDB);
            dbworker.RunWorkerAsync();
        }

        private void ProcessDB(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                //Check Last Restart Time
                if (NotifierDB.Instance.LastRestartTime == null)
                {
                    NotifierDB.Instance.LastRestartTime = SmartNotifierHelper.Instance.ServiceInstance.GetLastRestartTime();
                    if (NotifierDB.Instance.LastRestartTime != null && NotifierDB.Instance.LastRestartedTimeSpan.TotalHours > 0)
                    {
                        NotificationQueue.Add("Console last restart time was " + NotifierDB.Instance.LastRestartTime + ". It is recommend to restart the machine.");
                    }
                }

                System.Threading.Thread.Sleep(5 * 60 * 1000);  // Wait five minutes
            }
        }

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
