using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DemoWindowsService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is staretd at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stoped at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        }

        private void OnElapsedTime(Object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);    
            }
            string filePath = path + "\\ServiceLogs_" + DateTime.Now.ToShortDateString().Replace('/','_')+".txt";
            if (!File.Exists(filePath))
            {
                using(StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using(StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        //code will call service every 5 seconds and create a folder if none exists and write our message.
    }
}
