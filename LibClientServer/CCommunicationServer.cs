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
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;

namespace LibClientServer
{
    public class CCommunicationServer : ICommunication
    {
        public CCommunicationServer()
        {
            mpChannel = null;
        }
        //aPort 8068
        public override bool openConnection(int aPort)
        {
            try
            {
                BinaryServerFormatterSinkProvider serverFormatter =
                new BinaryServerFormatterSinkProvider();

                serverFormatter.TypeFilterLevel = TypeFilterLevel.Full;

                BinaryClientFormatterSinkProvider clientProv =
                    new BinaryClientFormatterSinkProvider();

                Hashtable props = new Hashtable();
                props["port"] = aPort;
                props["name"] = RemotingObjectName;

                // now create and register our custom HttpChannel 
                mpChannel = new TcpChannel(props, clientProv, serverFormatter);
                ChannelServices.RegisterChannel(mpChannel, false);
                WellKnownObjectMode mode = WellKnownObjectMode.Singleton;
                WellKnownServiceTypeEntry entry = new WellKnownServiceTypeEntry(
                    typeof(LibRemotingObj.ModelRemoteObject),
                    RemotingObjectName,
                    mode);
                ////
                //LifetimeServices.LeaseTime = TimeSpan.FromSeconds(10);
                //LifetimeServices.LeaseManagerPollTime = TimeSpan.FromSeconds(3);
                //LifetimeServices.RenewOnCallTime = TimeSpan.FromSeconds(2);
                //LifetimeServices.SponsorshipTimeout = TimeSpan.FromSeconds(1);
                ////
                RemotingConfiguration.RegisterWellKnownServiceType(entry);
                return true;
            }
            catch (Exception)
            {
                ChannelServices.UnregisterChannel(mpChannel);
                mpChannel = null;
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
