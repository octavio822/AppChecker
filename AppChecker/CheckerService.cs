using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace AppChecker
{
    public class CheckerService
    {
        //List of applications coming from config.json
        private List<ApplicationModel> _apps;
        //Eventlog to Log get the errors 
        private EventLog _log;
        //Name of the service (installed in PowerShell)
        private readonly string _serviceName = ConfigurationManager.AppSettings["ServiceName"];

        //Getting the service log
        public CheckerService(EventLog log)
        {
            this._log = log;
        }

        /// <summary>
        /// Area where the Magic occurs
        /// </summary>
        /// <param name="sender"> Come from the callback</param>
        internal void ExecuteCompare(object sender)
        {
            //Verify if is weekend,
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday||DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                return;
            //Verify if 
            if (DateTime.Now.Hour != 6 && DateTime.Now.Hour != 16)
                return;
            try
            {
                _apps = GetAllApps();
                for (int i = 0; i < _apps.Count; i++)
                {
                    if (!IsProcessOpen(_apps[i].process))
                        _apps[i].State = State.Stopped;
                }
                Mail mail = new Mail();
                mail.Send(_apps);
            }
            catch (Exception ex)
            {
                _log.WriteEntry(ex.Message, EventLogEntryType.Warning);
            }
        }
        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }
        public List<ApplicationModel> GetAllApps()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\"+_serviceName+"\"");      
            var value = (string)key.GetValue("ImagePath");         
            return JsonConvert.DeserializeObject<List<ApplicationModel>>(File.ReadAllText(Path.Combine(value.Replace("AppChecker.exe",""), "config.json")));
        }



    }
}
