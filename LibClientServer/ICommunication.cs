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
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Channels.Tcp;

namespace LibClientServer
{
    public abstract  class ICommunication
    {
        //
        protected TcpChannel mpChannel;
        protected String mStrRemotingObjectName;
        //
        public ICommunication()
        {
            RemotingObjectName = "LibClientServer";
        }
        //
        public String RemotingObjectName
        {
            get { return mStrRemotingObjectName; }
            set { mStrRemotingObjectName = value; }
        }
        //
        public abstract bool openConnection(int aPort);
        //
        public abstract void closeConnection(int aPort);
        //
    }
}
