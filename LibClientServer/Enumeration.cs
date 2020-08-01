//######################################################################
//# FILENAME: LibController
//#
//# DESCRIPTION:
//# 
//#
//# AUTHOR:		Mohammad Saiful Alam (Jewel)
//# POSITION:	Senior Software Engineer
//# E-MAIL:		saiful.alam@bjitgroup.com
//# CREATE DATE: 2009/07/20
//#
//# Copyright (c) 2008 BJIT Ltd.
//######################################################################
using System;
using System.Collections.Generic;
using System.Text;

namespace LibClientServer
{
    #region ACTION
        public enum ACTION
        {
            //Some thread related issue
            ACTION_THREAD_PAUSE,
            ACTION_THREAD_RESUME,
            ACTION_THREAD_CANCEL,
            //
            ACTION_BACKUP_DATA,
            ACTION_RESTORE_DATA,         
            //
            ACTION_CLICK_YES,
            ACTION_CLICK_NO,
            ACTION_NONE
        }
        #endregion 
    // 
}
