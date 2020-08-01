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
    [Serializable]  //MUST INCLUDE 
    public delegate void FuncInvoker(Object aObject);


    public interface IClientObserver
    {
        void Invoke(object obj);
    }

    /* Example use in Closure like Func
    delegate(object p)
               {
                   
                   // The Object P is here..to use.. 
                   
               });  

    */

    [Serializable]
    public class IRemoteObject:MarshalByRefObject
    {
        // This is to insure that when created as a Singleton, the first instance never dies,
        // regardless of the time between chat users.
        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
