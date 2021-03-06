﻿using SmartNotifier.View.DB;
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
using System.Linq;
using SmartNotifier.Common;

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
            NotifierDB.Instance.InitializeDB((MainWindowViewModel)DataContext);

            // Approch can be to be modify by eventHandler
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += timer_Tick;
            timer.Start();
            //NotifierDB.Instance.NotifyMessageExecutionHandler += OnRecevingNotification;
        }

        //private void OnRecevingNotification(object sender, EventArgs e)
        //{
        //    this.Dispatcher.Invoke(() =>
        //    {
        //        SmartNotifierHelper.Instance.ShowWarning(sender.ToString());
        //    });
        //}

        private void timer_Tick(object sender, EventArgs e)
        {
            if (NotifierDB.Instance.NewNotificationQueue.Count > 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    NotificationEntity message = NotifierDB.Instance.NewNotificationQueue.Dequeue();
                    switch (message.NotificationMessageType)
                    {
                        case MessageType.Error:
                            SmartNotifierHelper.Instance.ShowError(message.NotificationTrimMessage);
                            break;
                        case MessageType.Warninig:
                            SmartNotifierHelper.Instance.ShowWarning(message.NotificationTrimMessage);
                            break;
                        case MessageType.Information:
                            SmartNotifierHelper.Instance.ShowInformation(message.NotificationTrimMessage);
                            break;
                        default:
                            SmartNotifierHelper.Instance.ShowWarning(message.NotificationTrimMessage);
                            break;
                    }

                    SmartNotifierHelper.Instance.AddtoLogFile("Notification:" + message.NotificationMessage);
                    nIcon.Text = "Smart Notifier App(" + NotifierDB.Instance.OldNotificationQueue.Count + ")";
                    nIcon.Icon = GetIcon(NotifierDB.Instance.OldNotificationQueue.Count.ToString());
                });
            }
        }

        private void AddNotifyTray()
        {
            m_menu = new ContextMenu();
            m_menu.MenuItems.Add(0, new MenuItem("Exit", new System.EventHandler(Exit_Click)));
            nIcon.Visible = true;
            nIcon.Icon = GetIcon(NotifierDB.Instance.OldNotificationQueue.Count.ToString());
            nIcon.Text = "Smart Notifier App";
            nIcon.DoubleClick += NIcon_Click;
            nIcon.ContextMenu = m_menu;
            WindowState = System.Windows.WindowState.Minimized;
            Hide();
        }

        public static Icon GetIcon(string text)
        {
            Icon icon = new Icon(@"CT.ico");
            //Create bitmap, kind of canvas
            Bitmap bitmap = new Bitmap(icon.Width + 20, icon.Height);

            System.Drawing.Font drawFont = new System.Drawing.Font("Calibri", 100, System.Drawing.FontStyle.Regular);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);

            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            graphics.DrawIcon(icon, 0, 0);
            graphics.DrawString(text, drawFont, drawBrush, -20, -20);

            //To Save icon to disk
            //bitmap.Save("icon.ico", System.Drawing.Imaging.ImageFormat.Icon);

            Icon createdIcon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());

            drawFont.Dispose();
            drawBrush.Dispose();
            graphics.Dispose();
            bitmap.Dispose();

            return createdIcon;
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
