//######################################################################
//# DESCRIPTION:
//#
//# AUTHOR:		Mohammad Saiful Alam (Jewel)
//# POSITION:	Senior General Manager
//# E-MAIL:		saiful_vonair@yahoo.com
//# CREATE DATE: 
//#
//# Copyright: Free to use
//######################################################################

using System;

namespace LibRemotingObj
{
    [Serializable]
    public class ModelRemoteObject : IRemoteObject
    {
        protected FuncInvoker mpNotifyProgress;
        // This is to insure that when created as a Singleton, the first instance never dies,
        // regardless of the time between chat users.
        protected FuncInvoker mHost;
        //
        protected FuncInvoker mClient;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void setHost(FuncInvoker host)
        {
            mHost = host;
        }
        
        public void setClient(FuncInvoker obj)
        {
            mClient = obj;
        }

        IClientObserver mIClientObserver;
        public void setClient(IClientObserver obj)
        {
            mIClientObserver = obj;
        }

        public void sendMessageToHost(String message)
        {
            if(mHost != null)
            {
                mHost.Invoke(message);
            }
        }

        public void sendMessageToClient(String message)
        {
            if (mIClientObserver != null)
            {
                mIClientObserver.Invoke(message);
            }
        }
    }
}
