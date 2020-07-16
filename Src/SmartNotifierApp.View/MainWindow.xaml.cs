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
            _vm = new NotificationHelper();
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

        private int _count = 0;
        private readonly NotificationHelper _vm;

        private string CreateMessage()
        {
            return $"{_count++} {"Sample test"}";
        }

        private void Button_ShowInformationClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowInformation(CreateMessage());
        }

        private void Button_ShowSuccessClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowSuccess(CreateMessage());
        }

        private void Button_ShowWarningClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowWarning(CreateMessage());
        }

        private void Button_ShowErrorClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowError(CreateMessage());
        }

        private void Button_ShowCustomizedMessageClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowCustomizedMessage(CreateMessage());
        }

        private void Button_ClearLastClick(object sender, RoutedEventArgs e)
        {
            _vm.ClearLast();
        }

        private void Button_ClearAllClick(object sender, RoutedEventArgs e)
        {
            _vm.ClearAll();
        }

        private void Button_ClearByTag(object sender, RoutedEventArgs e)
        {
            _vm.ClearByTag();
        }

        private void Button_ClearByMessage(object sender, RoutedEventArgs e)
        {
            _vm.ClearByMessage();
        }
    }
}
