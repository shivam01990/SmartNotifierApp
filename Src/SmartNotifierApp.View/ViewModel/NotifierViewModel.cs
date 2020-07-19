using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.View.ViewModel
{
    public class NotifierViewModel : ViewModelBase
    {
        public string Name
        {
            get { return "Notification"; }
        }

        public string Icon
        {
            get { return "/Files/Notification.png"; }
        }
    }
}
