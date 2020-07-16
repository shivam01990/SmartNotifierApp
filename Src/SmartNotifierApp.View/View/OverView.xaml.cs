﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartNotifier.View.View
{
    /// <summary>
    /// Interaction logic for OverView.xaml
    /// </summary>
    public partial class OverView : UserControl
    {
        public OverView()
        {
            InitializeComponent();
        }
        private void Button_ShowInformationClick(object sender, RoutedEventArgs e)
        {
            SmartNotifierHelper.Instance.ShowInformation("Test message");
        }
    }
}
