using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace AppChecker
{
    public class AppCheckerService : ServiceBase
    {
        private System.ComponentModel.IContainer components = null;
        private List<ApplicationModel> _apps;
        //private Action<List<ApplicationModel>> SendMail = Mail.Send;
        private EventLog log;
        //private System.Timers.Timer timernew;
        private Timer timer;

        public AppCheckerService()
        {
            InitializeComponent();
        }
        private CheckerService service;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ServiceName = "Service";
            this.log = new EventLog("App Checker");
            this.log.Source = "AppChecker";

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            try
            {
                service = new CheckerService(log);
                timer = new Timer(service.ExecuteCompare, null, TimeSpan.Zero, TimeSpan.FromHours(1));
                log.WriteEntry("Se inicio Servicio Correctamente", EventLogEntryType.Information);                
            }
            catch (Exception ex)
            {
                log.WriteEntry(ex.Message, EventLogEntryType.Information);
            }
        }
    }
}
