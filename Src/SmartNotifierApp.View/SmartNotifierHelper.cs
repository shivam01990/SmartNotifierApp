using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationExample
{
    public sealed class SmartNotifierHelper
    {
        private static SmartNotifierHelper instance = null;
        private static readonly object padlock = new object();
        private readonly NotificationServices _vm;
        private int _count = 0;

        SmartNotifierHelper()
        {
            _vm = new NotificationServices();
        }

        public static SmartNotifierHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SmartNotifierHelper();
                    }
                    return instance;
                }
            }
        }

     
        public void ShowInformation(string message)
        {
            _vm.ShowInformation(message);
        }

        public void ShowSuccess(string message)
        {
            _vm.ShowSuccess(message);
        }

        public void ShowWarningClick(string message)
        {
            _vm.ShowWarning(message);
        }

        public void ShowError(string message)
        {
            _vm.ShowError(message);
        }

        public void ShowCustomized(string message)
        {
            _vm.ShowCustomizedMessage(message);
        }

        public void ClearLast()
        {
            _vm.ClearLast();
        }

        public void ClearAll()
        {
            _vm.ClearAll();
        }

        public void ClearByTag()
        {
            _vm.ClearByTag();
        }

        public void ClearByMessage()
        {
            _vm.ClearByMessage();
        }
    }
}
