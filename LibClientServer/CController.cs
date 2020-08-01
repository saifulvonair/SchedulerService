//######################################################################
//# FILENAME: CController
//#
//# DESCRIPTION:
//# 
//#
//# AUTHOR:		Mohammad Saiful Alam (Jewel)
//# POSITION:	Senior Software Engineer
//# E-MAIL:		saiful.alam@bjitgroup.com
//# CREATE DATE: 2009/08/03
//#
//# Copyright (c) 2008 BJIT Ltd.
//######################################################################
using System;
using System.Collections.Generic;
using System.Text;
using LibCoreModel;
using System.Threading;
using LibException;
using System.Windows.Forms;

namespace LibClientServer
{
    [Serializable]
    //public delegate void updateDelgate(CProgressEventArgument aObj);   
    public class CController : MarshalByRefObject
    {
        //public event updateDelgate ProgressStatus;        
        //public CProgressEventArgument mpProgess;
        static public CController Instance = null;
        public String mStrLoggerPath = "";
        public enum EVENT_TYPE
        {
            Asynchronous = 0,
            Synchronous
        };
        //
        #region private    
        Thread mpThread;
        ACTION meAction;
        EVENT_TYPE meEvent;
        ICommand mpCommand;
        public enum STATUS
        {
            Busy = 0,
            Free
        };
        STATUS meStatus;

        public CController()
        {            
            mpThread = null;
            mpCommand = null;
            Status = STATUS.Free;
            mStrLoggerPath = Environment.CurrentDirectory;
        }
        #endregion
        //
        #region property
        public EVENT_TYPE EventType
        {
            get { return meEvent; }
            set { meEvent = value; }
        }
        //
        public STATUS Status
        {
            get { return meStatus; }
            set { meStatus = value; }
        }
        //
        public ACTION Action
        {
            get { return meAction; }
            set { meAction = value; }
        }
        //
        public ICommand Command
        {
            get { return mpCommand; }
            set { mpCommand = value; }
        }

        #endregion
        //
        #region public method      
        //
        public void inittalize()
        {
            Status = STATUS.Free;
            EventType = EVENT_TYPE.Synchronous;
        }
        //
        public int handleAction(EVENT_TYPE aType)
        {
            switch (aType)
            {
                case EVENT_TYPE.Asynchronous:
                    return executeAsynchronousCommand();
                case EVENT_TYPE.Synchronous:
                    return handleAction();
            }
            return -1;
        }
        //
        public int handleAction()
        {
            CLogger.getInstance().initalize(mStrLoggerPath);           
            if (mpCommand != null)
            {
                try
                {
                    return mpCommand.executeCmd();
                }
                catch (Exception ex)
                {
                    CExceptionHandler.getInstance().filterException(ex);
                }                
            }
            CLogger.getInstance().uninitialize();
            return -1;
        }
        //
        public int handleAction(ACTION aAction)
        {
            switch (aAction)
            {
                case ACTION.ACTION_THREAD_PAUSE:
                    onPause();
                    break;
                case ACTION.ACTION_THREAD_CANCEL:
                    onCancel();
                    break;
                case ACTION.ACTION_THREAD_RESUME:
                    onResume();
                    break;
            }
            return -1;
        }
        //
        public void setCommand(ICommand aCmd)
        {
            mpCommand = aCmd;
        }
        #endregion
        //
        #region private
        //
        int executeAsynchronousCommand()
        {
            try
            {
                if (Status == STATUS.Busy)
                {
                    return -1;//Already Running another process..
                }
                ThreadStart lpTs = new ThreadStart(runThread);
                mpThread = new Thread(lpTs);
                mpThread.Start();
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
            return 0;
        }
        //Asynchronous thread function...
        void runThread()
        {
            try
            {
                handleAction();
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
        }
        //
        void onPause()
        {
            try
            {
                if (mpThread != null)
                {
                    if (mpThread.ThreadState == ThreadState.Running ||
                        mpThread.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        mpThread.Suspend();
                    }
                }
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
        }
        //
        void onCancel()
        {
            try
            {
                if (mpThread != null)
                {
                    if ((mpThread.ThreadState & ThreadState.AbortRequested) == ThreadState.AbortRequested)
                        return;//Already cancel one time
                    if (mpThread.ThreadState == ThreadState.Suspended)
                    {
                        mpThread.Resume();
                    }
                    if (mpThread.ThreadState == ThreadState.SuspendRequested)
                    {
                        mpThread.Resume();
                    }
                    mpThread.Abort();
                }
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
        }
        //
        void onResume()
        {
            try
            {
                if (mpThread != null)
                {
                    if (mpThread.ThreadState == ThreadState.Suspended)
                    {
                        mpThread.Resume();
                    }
                }
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
        }
        #endregion method
        //No command should be crated without controller
        public ICommand createCmd(String aCmdType)
        {
            ICommand lpCmd = null;
            switch (aCmdType)
            {
                case "CommandShredFile":
                    lpCmd = new CommandShredFile();
                    break;
                case "CommandCmdLine":
                    lpCmd = new CommandCmdLine();
                    break;
                case "CommandGetList":
                    lpCmd = new CommandGetList();
                    break;
                case "CommandCloseApplication":
                    lpCmd = new CommandCloseApplication();
                    break;              
                default:
                    break;
            }
            mpCommand = lpCmd;
            return lpCmd;            
        }
    }
}
