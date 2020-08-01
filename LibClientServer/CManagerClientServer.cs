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
using LibRemotingObj;


namespace LibClientServer
{
    public class CManagerClientServer
    {
        ICommunication mpServer;
        ICommunication mpClient;
        int miPort;
        
        static public CManagerClientServer mpCManagerClientServer;
       
        CManagerClientServer()
        {
            mpServer = new CCommunicationServer();
            mpClient = new CCommunicationClient();
            //SHOULD BE SAME
            mpServer.RemotingObjectName = "LibRemotingObj";
            mpClient.RemotingObjectName = "LibRemotingObj";
            miPort = 8085;

        }
        //
        public static CManagerClientServer getInstance()
        {
            if (mpCManagerClientServer == null)
                mpCManagerClientServer = new CManagerClientServer();
            return mpCManagerClientServer;
        }
        //
        public bool openServer(int aPort)
        {
            if (mpServer != null)
            {
              return mpServer.openConnection(aPort);
            }
            return false;
        }
        //
        public void closeServer()
        {
            if (mpServer != null)
            {
                mpServer.closeConnection(0);
            }
        }
        //
        public bool connectClient()
        {
            if (mpClient != null)
            {
                return mpClient.openConnection(0);
            }
            return false;
        }
        //
        public void disconnectClient()
        {
            if (mpClient != null)
            {
                mpClient.closeConnection(0);
            }
        }
        //
        public ModelRemoteObject createRemotingObj()
        {
            string fullServerAddress = "";// string.Format(
            //       "tcp://{0}:{1}/LibClientServer", "localhost", miPort.ToString());
            //tcp://localhost:8085/SayHello
            fullServerAddress =     "tcp://localhost:" + 
                                    miPort.ToString() +
                                    "/LibRemotingObj";

            // Create a proxy from remote object.            
            LibRemotingObj.ModelRemoteObject Instance =
            (LibRemotingObj.ModelRemoteObject)Activator.GetObject(
            typeof(LibRemotingObj.ModelRemoteObject),
            fullServerAddress);
            return Instance;
        }
        //
        public bool connectClientToServer()
        {
            try
            {
                disconnectClient();
                if (connectClient())
                {
                    //createRemotingObj();                                    
                }
                return true;
            }
            catch(Exception)
            {

            }
            return false;
        }
    }
}
