using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Pushto
{
    class Logger
    {
        public enum Level { Verbose, Debug, Warning, Error, Fatal }
        private Logger() { }

        public static string filename = "logs/" + DateTime.Now.ToString("yyyyMMddHHmm") + ".log";
        public static void Log(string sender, string message, Level level)
        {
            string output = " ";
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm";
            output += "[" + time.ToString(format) + "]";

            switch (level)
            {
                case Level.Verbose:
                    output += "[*]";
                    break;
                case Level.Debug:
                    output += "[?]";
                    break;
                case Level.Warning:
                    output += "[!]";
                    break;
                case Level.Error:
                    output += "[E]";
                    break;
                case Level.Fatal:
                    output += "[†]";
                    break;
            }
            output += "[" + sender + "] " + message;
            Console.WriteLine(output);

            if (!Debugger.IsAttached)
            {
                // Create a writer and open the file:
                StreamWriter log;
                if (!File.Exists(filename))
                {
                    Directory.CreateDirectory("logs");
                    log = new StreamWriter(filename);
                }
                else
                {
                    log = File.AppendText(filename);
                }
                // Write to the file:
                log.WriteLine(output);
                // Close the stream:
                log.Close();
            }
        }

        public static void Verbose(string sender, string message)
        {
            Log(sender, message, Level.Verbose);
        }

        public static void Debug(string sender, string message)
        {
            Log(sender, message, Level.Debug);
        }

        public static void Warning(string sender, string message)
        {
            Log(sender, message, Level.Warning);
        }

        public static void Error(string sender, string message)
        {
            Log(sender, message, Level.Error);
        }

        public static void Fatal(string sender, string message)
        {
            Log(sender, message, Level.Fatal);
        }
    }
}
