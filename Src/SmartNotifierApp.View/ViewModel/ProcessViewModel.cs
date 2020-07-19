using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.View.ViewModel
{
    public class ProcessViewModel : ViewModelBase
    {
        public string Name
        {
            get { return "Console Process/Services"; }
        }

        public string Icon
        {
            get { return "/Files/Process.png"; }
        }
    }
}
