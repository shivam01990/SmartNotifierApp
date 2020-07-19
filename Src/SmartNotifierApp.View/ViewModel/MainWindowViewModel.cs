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
        private ObservableCollection<ViewModelBase> _delarsMenu;

        public ObservableCollection<ViewModelBase> DelarsMenu
        {
            get { return this._delarsMenu; }
            set
            {
                _delarsMenu = value;
                RaisedPropertyChanged("DelarsMenu");
            }

        }

        public MainWindowViewModel()
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }



        ObservableCollection<ViewModelBase> temp_delarsMenu = new ObservableCollection<ViewModelBase>();

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            #region Dealer Menu
            temp_delarsMenu.Add(new OverviewViewModel());
            #endregion

        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DelarsMenu = temp_delarsMenu;
        }

    }
}
