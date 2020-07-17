using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.View
{
    public sealed class SmartNotifierHelper
    {
        private static SmartNotifierHelper instance = null;
        private static readonly object padlock = new object();
        private readonly NotificationServices _vm;

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

        public void ShowWarning(string message)
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

        private NotifyServiceWCF.Service1Client _serviceInstance;
        public NotifyServiceWCF.Service1Client ServiceInstance
        {
            get
            {
                return _serviceInstance;
            }
        }

        public void InitializeServiceInstance()
        {
            //InstanceContext instanceContext = new InstanceContext(this);
            //NotifyServiceWCF.Service1Client client = new NotifyServiceWCF.Service1Client(instanceContext);
            NotifyServiceWCF.Service1Client client = new NotifyServiceWCF.Service1Client();
            _serviceInstance = client;
        }

        public DateTime? LastRestartTime { get; set; } = null;

        public TimeSpan LastRestartedTimeSpan { get; set; }

    }
}
