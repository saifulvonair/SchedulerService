//######################################################################
//# FILENAME: ICommand
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
using Microsoft.Win32;
using LibCoreModel;
using System.Windows.Forms;
using LibSubjectObserver;
using LibException;
using System.Collections;

namespace LibClientServer
{
    [Serializable]   
    //
    public class ICommand : MarshalByRefObject
    {
        ArrayList mpListBeforeAfter;
        //        
        public IProgress mpProgress;

        public ICommand()
        {
            mpListBeforeAfter = null;
        }      
        public virtual int executeCmd() { return -1; }    
        //
        public void attachBeforeAfter(IBeforeAfter aBeforeAfter)
        {
            if (mpListBeforeAfter == null)
            {
                mpListBeforeAfter = new ArrayList();
            }
            mpListBeforeAfter.Add(aBeforeAfter);
        }
        //
        protected void afterOperation()
        {
            if (mpListBeforeAfter == null) return;
            foreach (IBeforeAfter lpItem in mpListBeforeAfter)
            {
                lpItem.afterOperation(this);
            }
        }
        //
        protected void beforeOperation()
        {
            if (mpListBeforeAfter == null) return;
            foreach (IBeforeAfter lpItem in mpListBeforeAfter)
            {
                lpItem.beforeOperation(this);
            }
        }       

    }
    //
    public class CommandShredFile : ICommand
    {
        public override int executeCmd()
        {
            try
            {
                CManagerShredder.getInstance().setNotifyProgress(mpProgress);
                beforeOperation();
                CManagerShredder.getInstance().shred();
                afterOperation();
            }
            catch (Exception ex)
            {
                CManagerShredder.getInstance().ShredderContent.initialize();
                CLogger.getInstance().appendLog(ex.Message);
            }
            return -1;
        }
    }
    //
    public class CommandCmdLine : ICommand
    {
        public String[] mpArgs = {};
        public override int executeCmd()
        {
            try
            {
                return CManagerCommandLine.getInstance().processCmdLine(mpArgs);
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
            return -1;
        }
    }
    //
    public class CommandGetList : ICommand
    {
        IRemoteList mpRemoteList;
        public IRemoteList RemoteList
        {
            get { return mpRemoteList; }
            set { mpRemoteList = value; }
        }
        public override int executeCmd()
        {
            try
            {
                if (RemoteList != null)
                    RemoteList.updateList(CManagerShredder.getInstance());
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
            return -1;
        }
    }

    public class CommandCloseApplication : ICommand
    {
       
        public override int executeCmd()
        {
            try
            {
                CManagerShredder.getInstance().ShredderContent.initialize();
                afterOperation(); 
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
            return -1;
        }
    }
}
