using Hardcodet.Wpf.TaskbarNotification;
using SteamChatLogger.WinApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SteamChatLogger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Logger Logger { get; } = new Logger();

        private TaskbarIcon TrayIcon = null;

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += this.AddDomain_UnhandledException;

            this.Logger.EnabledChanged   += this.Logger_EnabledChanged;
            this.Logger.ConnectedChanged += this.Logger_ConnectedChanged;
            this.Logger.Committed        += this.Logger_Committed;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            this.TrayIcon = new TaskbarIcon();
            this.TrayIcon.TrayMouseDoubleClick += this.TrayIcon_TrayMouseDoubleClick;

            ContextMenu contextMenu = new ContextMenu();
            MenuItem exitMenuItem = new MenuItem();
            contextMenu.Items.Add(exitMenuItem);
            exitMenuItem.Header = "E_xit";
            exitMenuItem.Click += this.ExitMenuItem_Click;
            this.TrayIcon.ContextMenu = contextMenu;
            this.UpdateTrayIcon();

            this.Logger.Enable();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.Logger.Disable();

            base.OnExit(e);
        }

        private void AddDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString(), "Steam Chat Logger");
        }

        private void Logger_EnabledChanged(bool enabled)
        {
            this.UpdateTrayIcon();
        }

        private void Logger_ConnectedChanged(bool connected)
        {
            this.UpdateTrayIcon();
        }

        private Stopwatch Stopwatch = new Stopwatch();
        private void Logger_Committed()
        {
            this.Stopwatch.Reset();
            this.Stopwatch.Start();

            this.UpdateTrayIcon();
            Task.Delay(500).ContinueWith(_ => this.UpdateTrayIcon());
        }

        private void TrayIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            string directory = this.Logger.LogDirectories.FirstOrDefault();
            if(directory == null) { return; }

            Process.Start(directory);
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Shutdown();
        }
        
        private void UpdateTrayIcon()
        {
            if (this.TrayIcon == null) { return; }

            this.TrayIcon.Dispatcher.Invoke(() =>
            {
                Bitmap icon = null;
                string status = "";
                if (!this.Logger.Enabled)
                {
                    status = "Disabled";
                    icon = SteamChatLogger.Properties.Resources.database_error;
                }
                else if (!this.Logger.Connected)
                {
                    status = "Disconnected";
                    icon = SteamChatLogger.Properties.Resources.database_error;
                }
                else
                {
                    int chatCount = this.Logger.ChatCount;
                    status = "Active (" + chatCount.ToString() + " chat" + (chatCount == 1 ? "" : "s") + ")";
                    if (this.Stopwatch.IsRunning &&
                        this.Stopwatch.ElapsedMilliseconds < 500)
                    {
                        icon = SteamChatLogger.Properties.Resources.database_save;
                    }
                    else
                    {
                        icon = SteamChatLogger.Properties.Resources.database;
                    }
                }
                
                this.TrayIcon.ToolTipText = "Steam Chat Logger\n" + status;

                IntPtr hIcon = icon.GetHicon();
                this.TrayIcon.Icon = Icon.FromHandle(hIcon);
                User.DestroyIcon(hIcon);
            });
        }
    }
}
