using SmartNotifier.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.View.ViewModel
{
    public class ProcessViewModel : ViewModelBase
    {
        public EventHandler ProcessesExecutionHandler;
        private ObservableCollection<ConsoleProcesses> _processList = new ObservableCollection<ConsoleProcesses>();

        public ProcessViewModel()
        {
            ProcessesExecutionHandler += OnProcessesReceived;
            ProcessList = new ObservableCollection<ConsoleProcesses>(ConsoleProcesses.GetConsoleProcesses());
        }

        public string Name
        {
            get { return "Console Process/Services"; }
        }

        public string Icon
        {
            get { return "/Files/Process.png"; }
        }

        public ObservableCollection<ConsoleProcesses> ProcessList
        {
            get
            {
                return _processList;
            }
            set
            {
                _processList = value;
                RaisedPropertyChanged(nameof(ProcessList));
            }
        }

        private void OnProcessesReceived(object sender, EventArgs e)
        {
            ProcessList = new ObservableCollection<ConsoleProcesses>(DB.NotifierDB.Instance.ConsoleProssesesList);
        }
    }
}
