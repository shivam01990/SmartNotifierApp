using SmartNotifier.View.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartNotifier.View.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private int _restartTimeNotificationInterval = AppSettings.ValidateRestartTime;
        private int _repeateNotificationTime = AppSettings.ValidateRepeateNotificationTime;
        private int _dBRefreshInterval = AppSettings.DBRefreshInterval;
        private int _thresholdSpaceForNotificationAlert = AppSettings.ThresholdSpaceForNotificationAlert;

        public SettingsViewModel()
        {
        }

        public string Name
        {
            get { return "Settings"; }
        }

        public string Icon
        {
            get { return "/Files/Settings.png"; }
        }

        public int RestartTimeNotificationInterval
        {
            get
            {
                return _restartTimeNotificationInterval;
            }
            set
            {
                _restartTimeNotificationInterval = value;
                RaisedPropertyChanged(nameof(RestartTimeNotificationInterval));
            }
        }

        public int RepeateNotificationTime
        {
            get
            {
                return _repeateNotificationTime;
            }
            set
            {
                _repeateNotificationTime = value;
                RaisedPropertyChanged(nameof(RepeateNotificationTime));
            }
        }

        public int ThresholdSpaceForNotificationAlert
        {
            get
            {
                return _thresholdSpaceForNotificationAlert;
            }
            set
            {
                _thresholdSpaceForNotificationAlert = value;
                RaisedPropertyChanged(nameof(ThresholdSpaceForNotificationAlert));
            }
        }

        public int DBRefreshInterval
        {
            get
            {
                return _dBRefreshInterval;
            }
            set
            {
                _dBRefreshInterval = value;
                RaisedPropertyChanged(nameof(DBRefreshInterval));
            }
        }

        private ICommand _saveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get
            {
                if (_saveSettingsCommand == null)
                {
                    _saveSettingsCommand = new RelayCommand(new Action<object>(SaveCommand));
                }
                return _saveSettingsCommand;
            }
            set
            {
                _saveSettingsCommand = value;
                RaisedPropertyChanged(nameof(SaveSettingsCommand));

            }
        }

        public void SaveCommand(object Parameter)
        {
            AppSettings.ValidateRestartTime = RestartTimeNotificationInterval;
            AppSettings.ValidateRepeateNotificationTime = RepeateNotificationTime;
            AppSettings.DBRefreshInterval = DBRefreshInterval;
            AppSettings.ThresholdSpaceForNotificationAlert = ThresholdSpaceForNotificationAlert;
        }

    }
}
