using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pushto.Network;
using System.Net;
using System.ComponentModel;
using System.Diagnostics;

namespace Pushto.VersionControl
{
    public class UpdateManager
    {
        /* Version naming convention
         * 
         * 'version' is the major version. These versions are NOT compatible with each other.
         * This means that this number only changes should a new dependency arise.
         * 
         * 'revision' is, as it says, the revision version. These versions are subversions to 
         * the major versions and mark interchangable and compatible releases.
         * What this also means is that all updates are done to and only to the main executable file.
         * The only exception is the first revision within a major version that also carries all
         * dependencies for the mentioned major version.
         * Upgrades/Downgrades can be made from and to any subversion within a major version.
         * 
         * Revision helpers:
         *  d  - Developer version (alpha/beta)
         *  rc - Release Candidate
         *  r  - Commercial distribution (release)
         *  
         *  Release Candidates automatically updates to Releases.
         */
        static private readonly string version = "0.1";
        static private readonly string revision = "d1";
        public UpdateManager()
        {
            CheckUpdate();
        }

        private void CheckUpdate()
        {
            /*Logger.Verbose("UpdateManager", "Checking for update...");
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("version", Version);
            APIHandler.Instance.GET<Dictionary<string, object>>("/clients/windows/version", parameters, (Dictionary<string, object> data) =>
            {
                try
                {
                    if ((bool)data["has_update"])
                    {
                        string url = (string)data["url"];
                        Logger.Verbose("UpdateManager", "Update found at: " + url + ". Starting download...");

                        WebClient webClient = new WebClient();
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler((object obj, AsyncCompletedEventArgs args) =>
                        {
                            Logger.Warning("UpdateManager", "Download complete. Starting updater and shutting this process down...");
                            Process.Start("updater.exe");
                            Environment.Exit(0);
                        });
                        webClient.DownloadFileAsync(new Uri(url), "updater.exe");
                    }
                    else
                    {
                        Logger.Verbose("UpdateManager", "No update needed.");
                    }
                }
                catch (Exception e)
                {
                    Logger.Verbose("UpdateManager", "Crashed: " + e.ToString());
                }
                Util.RunLater(CheckUpdate, "Update check retry", TimeSpan.FromMinutes(15));
            });*/
        }

        static public string Version
        {
            get
            {
                return version + "-" + revision;
            }
        }
    }
}
