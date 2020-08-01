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
using System;
using System.Collections.Generic;
using System.Text;

namespace LibException
{

    //
    #region Exception
    public enum EXCEPTION
    {
        None = 0x0,
        IOException = 0x2,
        ArgumentNullException = 0x4,
        DirectoryNotFoundException = 0x8,
        NotSupportedException = 0x10,
        UnauthorizedAccessException = 0x20,
        ArgumentException = 0x40,
        PathTooLongException = 0x80,
        SecurityException = 0x100,
        AccessViolationException = 0x200,
        FileNotFoundException = 0x400,
        FileLoadException = 0x800,
        EndOfStreamException = 0x1000,
        InvalidPathException = 0x2000,
        UnknownException = 0x4000
    }
    #endregion
    //
    public class CExceptionHandler
    {
        #region private
        CExceptionHandler()
        {
            mpCExceptionHandler = null;
        }

        static CExceptionHandler mpCExceptionHandler;
        EXCEPTION meException;
        #endregion
        //
        public static CExceptionHandler getInstance()
        {
            if (mpCExceptionHandler == null)
                mpCExceptionHandler = new CExceptionHandler();
            return mpCExceptionHandler;
        }
        //
        public void clearException()
        {
            meException = EXCEPTION.None;
        }
        //
        public void filterException(Exception ex)
        {
            if (ex is System.IO.IOException)
            {
                if (ex is System.IO.DirectoryNotFoundException)
                {
                    meException = meException | EXCEPTION.DirectoryNotFoundException;
                }
                else if (ex is System.IO.PathTooLongException)
                {
                    meException = meException | EXCEPTION.PathTooLongException;
                }
                else if (ex is System.IO.FileNotFoundException)
                {
                    meException = meException | EXCEPTION.FileNotFoundException;
                }
                else if (ex is System.IO.FileLoadException)
                {
                    meException = meException | EXCEPTION.FileLoadException;
                }
                else if (ex is System.IO.EndOfStreamException)
                {
                    meException = meException | EXCEPTION.EndOfStreamException;
                }
                else if (ex.Data.Contains(EXCEPTION.InvalidPathException))
                {
                    meException = meException | EXCEPTION.InvalidPathException;
                }
                else //if (ex is UnknownException)
                {
                    meException = meException | EXCEPTION.UnknownException;
                }
            }
            else if (ex is ArgumentNullException)
            {
                meException = meException | EXCEPTION.ArgumentNullException;
            }
            else if (ex is System.IO.DirectoryNotFoundException)
            {
                meException = meException | EXCEPTION.DirectoryNotFoundException;
            }
            else if (ex is NotSupportedException)
            {
                meException = meException | EXCEPTION.NotSupportedException;
            }
            else if (ex is UnauthorizedAccessException)
            {
                meException = meException | EXCEPTION.UnauthorizedAccessException;
            }
            else if (ex is ArgumentException)
            {
                meException = meException | EXCEPTION.ArgumentException;
            }
            else if (ex is System.IO.PathTooLongException)
            {
                meException = meException | EXCEPTION.PathTooLongException;
            }
            else if (ex is System.Security.SecurityException)
            {
                meException = meException | EXCEPTION.SecurityException;
            }
            else if (ex is AccessViolationException)
            {
                meException = meException | EXCEPTION.AccessViolationException;
            }
            else if (ex is System.IO.FileNotFoundException)
            {
                meException = meException | EXCEPTION.FileNotFoundException;
            }
            else if (ex is System.IO.FileLoadException)
            {
                meException = meException | EXCEPTION.FileLoadException;
            }
            else if (ex is System.IO.EndOfStreamException)
            {
                meException = meException | EXCEPTION.EndOfStreamException;
            }
            else //if (ex is UnknownException)
            {
                meException = meException | EXCEPTION.UnknownException;
            }
            // 
            CLogger.getInstance().appendLog(ex.Message);
        }
        //
        public bool getException(EXCEPTION ex)
        {
            int liRet = (int)(meException & ex);
            if (liRet > 0)
                return true;
            return false;
        }
    }
}
