﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmartNotifier.View.Converters
{
    public class ImageToSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(ImageSource))
            {
                if (value is string)
                {
                    string str = (string)value;
                    BitmapImage image = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory+str, UriKind.RelativeOrAbsolute));
                    return image;
                }
                else if (value is Uri)
                {
                    Uri uri = (Uri)value;
                    return new BitmapImage(uri);
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
