//######################################################################
//# FILENAME: CLogger
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
using System.IO;
using LibException;

namespace LibException
{
    public class CLogger
    {
        #region private
        CLogger()
        {
            mpLogger = null;
            //mbDisplay = true;
            mbDisplay = true;// For Release to QA thid should be false to protect loger generation 
            mStrLoggerName = "\\Logger.txt";
            mStrReportName = "\\Report.txt";
            //initalize("D:");
        }

        public static CLogger mpLogger;
        //
        BinaryWriter mpFileWriter;
        FileStream mpFs;
        String mStrLoggerName;
        //
        StreamWriter mpReportFileWriter;
        String mStrReportName;
        //
        //FileStream mpFsSelected;
        StreamWriter mpSelectedFileWriter;
        //
        bool mbDisplay;
        #endregion
        //
        #region public 
        public static CLogger getInstance()
        {
            if (mpLogger == null)
                mpLogger = new CLogger();
            return mpLogger;
        }
        //
        public void initalize(String aFullPath)
        {
            if (!mbDisplay) return;
            try
            {
                Directory.CreateDirectory(aFullPath);

                mpFs = File.Open(aFullPath + mStrLoggerName, FileMode.Create);
                mpFileWriter = new BinaryWriter(mpFs);
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
        }
        //
        public void initializeReport(String aFullPath, bool aFileModeFlag)
        {
            try
            {
                mpReportFileWriter = new StreamWriter(aFullPath + mStrReportName, aFileModeFlag, Encoding.Default);
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
        }
        //
        public void initializeSelectedFile(String aFullPath,bool aFileModeFlag)
        {
            try
            {
                //mpFsSelected = File.Open(aFullPath, FileMode.CreateNew);
                mpSelectedFileWriter = new StreamWriter(aFullPath, aFileModeFlag,Encoding.Default);
            }
            catch (Exception ex)
            {
                CExceptionHandler.getInstance().filterException(ex);
            }
        }
        //This method must be called 
        public void uninitialize()
        {
            if (!mbDisplay) return;
            if (mpFs != null)
            {
                mpFs.Close();
                mpFs = null;
            }
            if (mpFileWriter != null)
            {
                mpFileWriter.Close();
                mpFileWriter = null;
            }
        }
        //
        public void uninitializeReport()
        {
            if (mpReportFileWriter != null)
            {
                mpReportFileWriter.Close();
                mpReportFileWriter = null;

            }
        }
        //
        public void uninitializeSelectedFile()
        {
            if (mpSelectedFileWriter != null)
            {
                mpSelectedFileWriter.Close();
                mpSelectedFileWriter = null;

            }
        }
        //
        public void appendLog(String lStrVal)
        {
            if (!mbDisplay) return;
            if(mpFileWriter != null)
            {
                try
                {
                    mpFileWriter.Write(Environment.NewLine);
                    mpFileWriter.Write(lStrVal);
                }
                catch (Exception ex)
                {
                    CExceptionHandler.getInstance().filterException(ex);
                }
            }
        }
        //
        public void appendReport(String lStrVal)
        {
            if (mpReportFileWriter != null)
            {
                try
                {
                    mpReportFileWriter.Write(lStrVal);
                    mpReportFileWriter.Write(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    CExceptionHandler.getInstance().filterException(ex);
                }
            }
        }
        //
        public void appendSelectedFileName(String lStrVal)
        {
            if (mpSelectedFileWriter != null)
            {
                try
                {
                    mpSelectedFileWriter.Write(lStrVal);
                    mpSelectedFileWriter.Write(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    CExceptionHandler.getInstance().filterException(ex);
                }
            }
        }
        #endregion
    }
}
