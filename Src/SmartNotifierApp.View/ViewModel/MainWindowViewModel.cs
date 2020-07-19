using SmartNotifier.View.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SmartNotifier.View.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private ObservableCollection<ViewModelBase> _mainMenu;
        private ObservableCollection<ViewModelBase> temp_mainMenu = new ObservableCollection<ViewModelBase>();

        public ObservableCollection<ViewModelBase> MainMenu
        {
            get { return this._mainMenu; }
            set
            {
                _mainMenu = value;
                RaisedPropertyChanged(nameof(MainMenu));
            }
        }

        public MainWindowViewModel()
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            #region Dealer Menu
            temp_mainMenu.Add(new OverviewViewModel());
            temp_mainMenu.Add(new ProcessViewModel());
            temp_mainMenu.Add(new NotifierViewModel());
            #endregion
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MainMenu = temp_mainMenu;
        }

    }
}
