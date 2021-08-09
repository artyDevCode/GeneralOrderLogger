using ApplicationLogger;
using System;
using System.Collections.Generic;
using AL = ApplicationLogger.WriteToDbLog;

namespace TestConsoleApp
{
    public class Program
    {
      //  private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {

            //Write to the log file
            Console.WriteLine("User is - " + System.Security.Principal.WindowsIdentity.GetCurrent().Name + 
                  "   Computer name - " + System.Environment.MachineName);
          
            //Text file insert. Declare the log4net. NOTE: The client application does not need log4net installed
            Logger.ConfigureFileAppender(ApplicationLogger.Logger.getLoggerPath()); // This only has to be called once. The looger will insert information to this declared logger. This is for the file log

            //This Replaces the Log.Info() etc. This was the application does not need to have log$net installed.
            Logger.CallLog("START: Main. ");

            //*************************************************************************


            //Database insert. This inserts the logging details to the database instead or including the text log file
            //Sends a list of objects to the database generally as a one time call. If further calls are required, use the sample below
            var records = new List<AppLog>();

            records.Add(
            new AppLog
            {
                AdditionalUserInfo = "test1 additional user info",
                // AppID = 2,
                LogDetail = "TestConsoleApp1",
                LogDetail_Additional = "This is a test additionalinfo TestConsoleApp1",
                LogTime = DateTime.Now,
                UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Workstation = System.Environment.MachineName
            });
            records.Add(new AppLog
            {
                AdditionalUserInfo = "test2 additional user info",
                // AppID = 2,
                LogDetail = "TestConsoleApp2",
                LogDetail_Additional = "This is a test additionalinfo TestConsoleApp2",
                LogTime = DateTime.Now,
                UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Workstation = System.Environment.MachineName
            });
            records.Add(new AppLog
            {
                AdditionalUserInfo = "test3 additional user info",
                // AppID = 2,
                LogDetail = "TestConsoleApp3",
                LogDetail_Additional = "This is a test additionalinfo TestConsoleApp3",
                LogTime = DateTime.Now,
                UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Workstation = System.Environment.MachineName
            });
            records.Add(new AppLog
            {
                AdditionalUserInfo = "test4 additional user info",
                // AppID = 2,
                LogDetail = "TestConsoleApp4",
                LogDetail_Additional = "This is a test additionalinfo TestConsoleApp4",
                LogTime = DateTime.Now,
                UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Workstation = System.Environment.MachineName
            });


            AL.WriteLog(records, "ExternalLinkClearRevisions", false);


            //**********************************************************************
            //Singleton pattern write live records to db. This creates a single instance of the class as well as a single instance of the Database DBContect object

            WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
            {
                AdditionalUserInfo = "test1 additional user info",
                // AppID = 2,
                LogDetail = "TestConsoleApp1",
                LogDetail_Additional = "This is a test additionalinfo TestConsoleApp1",
                LogTime = DateTime.Now,
                UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Workstation = System.Environment.MachineName
            }, "ExternalLinkClearRevisions", false);
            WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
            {
                AdditionalUserInfo = "test2 additional user info",
                // AppID = 2,
                LogDetail = "TestConsoleApp2",
                LogDetail_Additional = "This is a test additionalinfo TestConsoleApp2",
                LogTime = DateTime.Now,
                UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Workstation = System.Environment.MachineName
            }, "ExternalLinkClearRevisions", false);
            WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
            {
                AdditionalUserInfo = "test3 additional user info",
                // AppID = 2,
                LogDetail = "TestConsoleApp3",
                LogDetail_Additional = "This is a test additionalinfo TestConsoleApp3",
                LogTime = DateTime.Now,
                UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Workstation = System.Environment.MachineName
            }, "ExternalLinkClearRevisions", false);
            WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
            {
                AdditionalUserInfo = "test4 additional user info",
                // AppID = 2,
                LogDetail = "TestConsoleApp4",
                LogDetail_Additional = "This is a test additionalinfo TestConsoleApp4",
                LogTime = DateTime.Now,
                UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Workstation = System.Environment.MachineName
            }, "ExternalLinkClearRevisions", false);

            WriteToDbLogInstance.WriteToDbInstance.Dispose();
        }
    }
}
