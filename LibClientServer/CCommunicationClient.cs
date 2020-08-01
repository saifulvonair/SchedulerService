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
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Runtime.Remoting.Channels.Tcp;

namespace LibClientServer
{
    class CCommunicationClient : ICommunication
    {
        public CCommunicationClient()
        {
            mpChannel = null;
        }
        public override bool openConnection(int aPort)
        {
            try
            {
                //Configure remoting.
                BinaryServerFormatterSinkProvider serverFormatter =
                    new BinaryServerFormatterSinkProvider();

                serverFormatter.TypeFilterLevel = TypeFilterLevel.Full;

                BinaryClientFormatterSinkProvider clientProv =
                    new BinaryClientFormatterSinkProvider();

                Hashtable props = new Hashtable();
                props["name"] = RemotingObjectName;
                props["port"] = 0;

                mpChannel = new TcpChannel(props, clientProv, serverFormatter);

                ChannelServices.RegisterChannel(mpChannel, false);
                return true;
            }
            catch (Exception)
            {
                if (mpChannel != null)
                {
                    ChannelServices.UnregisterChannel(mpChannel);
                    mpChannel = null;
                }
            }
            return false;
        }
        //
        public override void closeConnection(int aPort)
        {
            if (mpChannel != null)
            {
                ChannelServices.UnregisterChannel(mpChannel);
                mpChannel = null;
            }
        }
    }
}
