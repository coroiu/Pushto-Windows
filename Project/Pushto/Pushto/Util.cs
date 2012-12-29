using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Threading;

namespace Pushto
{
    class Util
    {
        public static void RunLater(Action action, string name, TimeSpan time)
        {
            new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(time);
                Logger.Debug("RunLater", "Running later: " + name + ".");
                action();
            })).Start();
            Logger.Debug("RunLater", "Scheduled: " + name + ".");
        }
    }
}
