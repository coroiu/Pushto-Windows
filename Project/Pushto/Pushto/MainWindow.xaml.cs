using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace Pushto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        static readonly Mutex mutex = new Mutex(true, "{9e3ab63f-10b6-49ca-86e3-61c860659d62}");
        public static MainWindow window;
        public MainWindow()
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                Environment.Exit(0);
            }
            IntPtr ptr = GetForegroundWindow();
            int u;
            GetWindowThreadProcessId(ptr, out u);
            Process process = Process.GetProcessById(u);
            if (process.ProcessName == "cmd")    //Is the uppermost window a cmd process?
            {
                AttachConsole(process.Id);
                Console.WriteLine();
                Logger.Verbose("Init", "Starting application in console developer mode");
            }
            else if (Debugger.IsAttached)
            {
                AllocConsole();
                Logger.Verbose("Init", "Starting application in developer mode");
            }
            else
            {
                Logger.Verbose("Init", "Starting application in commercial mode");
            }

            Logger.Verbose("Init", "Version " + VersionControl.UpdateManager.Version);
            InitializeComponent();
        }
    }
}
