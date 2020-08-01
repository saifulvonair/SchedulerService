//######################################################################
//# FILENAME: 
//#
//# DESCRIPTION:
//# 
//#
//# AUTHOR:		Mohammad Saiful Alam (Jewel)
//# POSITION:	
//# E-MAIL:		saiful.alam@bjitgroup.com
//# CREATE DATE: 
//#
//# Copyright (c) 
//######################################################################
using LibClientServer;
using LibException;
using LibRemotingObj;
using System;
using System.ServiceProcess;
using System.Timers;

namespace SchedulerService
{
    public partial class ServiceTaskScheduler : ServiceBase
    {
        Timer timer1;
        ModelRemoteObject mModelRemoteObject;

        public ServiceTaskScheduler()
        {
            InitializeComponent();
            CLogger.getInstance().initalize(AppDomain.CurrentDomain.BaseDirectory);
        }

        protected override void OnStart(string[] args)
        {            
            CLogger.getInstance().appendLog("OnStart " + DateTime.Now);
            startTimer();
            startServer();
        }

        public void startTimer()
        {
            timer1 = new Timer();
            timer1.Interval = 10000;
            timer1.Elapsed += new ElapsedEventHandler(timer1_Tick);
            timer1.Enabled = true;
            CLogger.getInstance().appendLog(" timer1.Enabled = true " + DateTime.Now);
        }

        public void startServer()
        {
            if (CManagerClientServer.getInstance().openServer(8085))
            {
                //MessageBox.Show("Server is Ready!! to transfer and reeive Object!");
                CLogger.getInstance().appendLog("Server is Ready!! to transfer and reeive Object! " + DateTime.Now);
                mModelRemoteObject = CManagerClientServer.getInstance().createRemotingObj();
                //
                mModelRemoteObject.setHost(delegate (object obj)
                {   
                    string data = (string)obj;
                    CLogger.getInstance().appendLog("Get Command-> :" + data);
                    if(data.Contains("Start"))
                    {
                        timer1.Enabled = true;
                        sendMessageToClient("Received From Client : " + data + "->" + DateTime.Now);
                    }
                    else if(data.Contains("Stop"))
                    {
                        timer1.Enabled = false;
                        sendMessageToClient("Received From Client : " + data + "->" + DateTime.Now);
                    }
                });
            }
            else
            {
                CLogger.getInstance().appendLog("Server is not Ready oops there might be some problem in port!!");
            }
        }

        public void sendMessageToClient(string message)
        {
            if (mModelRemoteObject != null)
            {
                mModelRemoteObject.sendMessageToClient(message);
            }
        }


        protected override void OnStop()
        {
            CLogger.getInstance().appendLog("OnStop " + DateTime.Now);
            CLogger.getInstance().uninitialize();
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            CLogger.getInstance().appendLog("timer1_Tick At: "+ DateTime.Now);
            try
            {
                if (mModelRemoteObject != null)
                {
                    mModelRemoteObject.sendMessageToClient("timer1_Tick At: " + DateTime.Now);
                }
            }
            catch(Exception ex)
            {
                CLogger.getInstance().appendLog(ex.Message+ "at: " + DateTime.Now);
            }

        }
    }
}
