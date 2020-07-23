using SmartNotifier.Common;
using SmartNotifier.View.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SmartNotifier.View.ViewModel
{
    public class OverviewViewModel : ViewModelBase
    {
        public OverviewViewModel()
        {
        }

        public string Name
        {
            get { return "Overview"; }
        }

        public string Icon
        {
            get { return "/Files/Overview.png"; }
        }

        public string MachineName
        {
            get
            {
                return Environment.MachineName.ToString();
            }
        }

        public string RAM
        {
            get
            {
                if (NotifierDB.Instance.SystemInfo != null && NotifierDB.Instance.SystemInfo.RAM != null)
                {
                    decimal memory = decimal.Parse(NotifierDB.Instance.SystemInfo.RAM);
                    memory = memory / (1024 * 1024 * 1024);
                    return memory.ToString("#.##") + " GB";
                }

                return string.Empty;
            }
        }

        public string SystemType
        {
            get
            {
                return NotifierDB.Instance.SystemInfo?.SystemType ?? string.Empty;
            }
        }

        public string SystemModel
        {
            get
            {
                return NotifierDB.Instance.SystemInfo?.SystemModel ?? string.Empty;
            }
        }

        public string CDriveInfo
        {
            get
            {
                if (NotifierDB.Instance.DriveDetails != null)
                {
                    return NotifierDB.Instance.DriveDetails.CTotalFreeSpace + " GB " + "free of " + NotifierDB.Instance.DriveDetails.CTotalSize + " GB";
                }

                return "NA";
            }
        }

        public string DDriveInfo
        {
            get
            {
                if (NotifierDB.Instance.DriveDetails != null)
                {
                    return NotifierDB.Instance.DriveDetails.DTotalFreeSpace + " GB " + "free of " + NotifierDB.Instance.DriveDetails.DTotalSize + " GB";
                }

                return "NA";
            }
        }

        public int CDriveProgress
        {
            get
            {
                if (NotifierDB.Instance.DriveDetails != null && NotifierDB.Instance.DriveDetails.CTotalSize > 0)
                {
                    return (int)((NotifierDB.Instance.DriveDetails.CTotalSize - NotifierDB.Instance.DriveDetails.CTotalFreeSpace) * 100 / NotifierDB.Instance.DriveDetails.CTotalSize);
                }
                return 0;
            }
        }

        public int DDriveProgress
        {
            get
            {
                if (NotifierDB.Instance.DriveDetails != null && NotifierDB.Instance.DriveDetails.DTotalSize > 0)
                {
                    return (int)((NotifierDB.Instance.DriveDetails.DTotalSize - NotifierDB.Instance.DriveDetails.DTotalFreeSpace) * 100 / NotifierDB.Instance.DriveDetails.DTotalSize);
                }
                return 0;
            }
        }


        private DispatcherTimer timer;
        public string LastRestartTime
        {
            get
            {
                if (timer == null)
                {
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromMilliseconds(1000);
                    timer.Tick += timer_Tick;
                    timer.Start();
                }
                return NotifierDB.Instance.LastRestartTime.ToString();
            }
        }

        public ConsoleInformation ConsoleInfo
        {
            get
            {
                return NotifierDB.Instance.ConsoleInfo ?? new ConsoleInformation();
            }
        }

        private string restartInterval;
        public string RestartInterval
        {
            get
            {
                return restartInterval;
            }
            set
            {
                restartInterval = value;
                RaisedPropertyChanged(nameof(RestartInterval));
                RaisedPropertyChanged(nameof(LastRestartTime));
                RaisedPropertyChanged(nameof(RAM));
                RaisedPropertyChanged(nameof(SystemType));
                RaisedPropertyChanged(nameof(SystemModel));
                RaisedPropertyChanged(nameof(CDriveInfo));
                RaisedPropertyChanged(nameof(DDriveInfo));
                RaisedPropertyChanged(nameof(CDriveProgress));
                RaisedPropertyChanged(nameof(DDriveProgress));
                RaisedPropertyChanged(nameof(ConsoleInfo));
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan span = NotifierDB.Instance.LastRestartedTimeSpan;
            RestartInterval = String.Format("{0} days, {1} hours, {2} minutes, {3} seconds", span.Days, span.Hours, span.Minutes, span.Seconds);
        }


    }
}
