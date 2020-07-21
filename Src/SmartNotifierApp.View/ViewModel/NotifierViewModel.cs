using SmartNotifier.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.View.ViewModel
{
    public class NotifierViewModel : ViewModelBase
    {
        public EventHandler NotifyAllMessageExecutionHandler;
        private ObservableCollection<NotificationEntity> _notificationList = new ObservableCollection<NotificationEntity>();

        public NotifierViewModel()
        {
            NotifyAllMessageExecutionHandler += OnMessageReceived;
        }

        private void OnMessageReceived(object sender, EventArgs e)
        {
            NotificationList = new ObservableCollection<NotificationEntity>(DB.NotifierDB.Instance.OldNotificationQueue.OrderByDescending(x => x.NotifyOn).ToList());
        }

        public string Name
        {
            get { return "Notification"; }
        }

        public string Icon
        {
            get { return "/Files/Notification.png"; }
        }

        public ObservableCollection<NotificationEntity> NotificationList
        {
            get
            {
                return _notificationList;
            }
            set
            {
                _notificationList = value;
                RaisedPropertyChanged(nameof(NotificationList));
            }
        }
    }
}
