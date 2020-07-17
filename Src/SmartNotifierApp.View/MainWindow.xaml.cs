using SmartNotifier.View.ViewModel;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;

namespace SmartNotifier.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class MainWindow : Window//, NotifyServiceWCF.IService1Callback
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x10000;

        NotifyIcon nIcon = new NotifyIcon();
        private ContextMenu m_menu;
        private bool isExitNotByIconTray = true;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            AddNotifyTray();
            ListBoxMenu.SelectedIndex = 0;
            SmartNotifierHelper.Instance.InitializeServiceInstance();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            BackgroundWorker checkSystemTimer = new BackgroundWorker();
            checkSystemTimer.DoWork += new DoWorkEventHandler(CheckLastRestart);
            checkSystemTimer.RunWorkerAsync();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (SmartNotifierHelper.Instance.LastRestartTime == null)
            {
                SmartNotifierHelper.Instance.LastRestartTime = SmartNotifierHelper.Instance.ServiceInstance.GetLastRestartTime();
            }
            else
            {
                SmartNotifierHelper.Instance.LastRestartTime = ((DateTime)SmartNotifierHelper.Instance.LastRestartTime).AddSeconds(1);
            }
        }

        private void CheckLastRestart(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (SmartNotifierHelper.Instance.LastRestartTime != null)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        SmartNotifierHelper.Instance.ShowWarning("Last restart time" + SmartNotifierHelper.Instance.LastRestartTime);
                    });

                    System.Threading.Thread.Sleep(5 * 60 * 1000);  // Wait five minutes
                }
            }
        }

        private void AddNotifyTray()
        {
            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0, new MenuItem("Exit", new System.EventHandler(Exit_Click)));
            nIcon.Visible = true;
            nIcon.Icon = new Icon(@"CT.ico");
            nIcon.Text = "Smart Notifier App";
            nIcon.DoubleClick += NIcon_Click;
            nIcon.ContextMenu = m_menu;
            WindowState = System.Windows.WindowState.Minimized;
            Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            isExitNotByIconTray = false;
            this.Close();
        }

        private void NIcon_Click(object sender, System.EventArgs e)
        {
            WindowState = WindowState.Normal;
            Show();
            Activate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper((Window)sender).Handle;
            var value = GetWindowLong(hwnd, GWL_STYLE);
            SetWindowLong(hwnd, GWL_STYLE, (int)(value & ~WS_MAXIMIZEBOX));
        }


        // Minimize to system tray when application is closed.
        protected override void OnClosing(CancelEventArgs e)
        {
            WindowState = WindowState.Minimized;
            // setting cancel to true will cancel the close request
            // so the application is not closed
            e.Cancel = isExitNotByIconTray;
            if (isExitNotByIconTray == false)
            {
                this.Hide();
                base.OnClosing(e);
            }
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MiniMize_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void ListBoxMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ListBoxMenu.SelectedIndex != -1)
            {
                pnlcontent.Content = ListBoxMenu.SelectedItem;
            }
        }

        public void Drag_Window(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void NotifyNotificationBack(string Message)
        {
            Dispatcher.BeginInvoke(new ThreadStart(() =>
          this.Dispatcher.Invoke(() =>
          {
              SmartNotifierHelper.Instance.ShowInformation(Message);
          })
       ));



        }
    }
}
