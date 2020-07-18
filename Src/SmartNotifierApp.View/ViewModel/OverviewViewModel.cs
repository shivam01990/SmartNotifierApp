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

        private void timer_Tick(object sender, EventArgs e)
        {
            RestartInterval = NotifierDB.Instance.LastRestartedTimeSpan.ToString();
        }

        public string Name
        {
            get { return "Overview"; }
        }

        public string Icon
        {
            get { return "/Files/Overview.png"; }
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
            }
        }


    }
}
