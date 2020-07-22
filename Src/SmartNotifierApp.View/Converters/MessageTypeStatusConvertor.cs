using SmartNotifier.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartNotifier.View.Converters
{
    public class MessageTypeStatusConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(Brush))
            {
                if (value is ProcessStatus)
                {
                    string str = "Red";
                    ProcessStatus processStatus = (ProcessStatus)value;
                    if (processStatus == ProcessStatus.Running)
                    {
                        str = "DarkGreen";
                    }

                    return str;
                }
            }

            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
