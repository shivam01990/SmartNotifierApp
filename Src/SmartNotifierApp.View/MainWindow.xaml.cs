using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace ConfigurationExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon nIcon = new NotifyIcon();
        public MainWindow()
        {
            InitializeComponent();
            nIcon.Visible = true;
            WindowState = System.Windows.WindowState.Minimized;
            nIcon.Icon = new Icon(@"CT.ico");
            nIcon.Text = "Smart Notifier App";
            nIcon.Click += NIcon_Click;
            //nIcon.ShowBalloonTip(3000, "", "Check for updates", ToolTipIcon.Info);
        }

        private void NIcon_Click(object sender, System.EventArgs e)
        {
            WindowState = WindowState.Normal;
            Show();
            Activate();
        }

       
        private void Button_ShowInformationClick(object sender, RoutedEventArgs e)
        {
            SmartNotifierHelper.Instance.ShowInformation("Test message");
        }
       
    }
}
