using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.View.ViewModel
{
  public class OverviewViewModel : ViewModelBase
    {
        public  string Name
        {
            get { return "Overview"; }
        }

        public  string Icon
        {
            get { return "/Files/Overview.png"; }
        }
    }
}
