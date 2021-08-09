using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    /// <summary>
    /// Using a List of items to save once to the database withing a using scope. This is used mainly for one off save to the Database
    /// </summary>
    public class WriteToDbLog
    {
        public static void WriteLog(List<AppLog> entry, string applicationName, bool displayInUI)
        {
            AppLog appLog = new AppLog();

            using (var dbConnect = new ApplicationLogger.DatabaseLog())
            {
                var application = dbConnect.AppLists.Where(s => s.AppCode == applicationName).FirstOrDefault();
                if (applicationName == string.Empty)
                    application.AppID = 99; //unknown 

                //Write into the database what gets passed
                foreach (var res in entry)
                {
                    //Loop through each item and ssave into Database

                    appLog = dbConnect.AppLogs.Add(new AppLog
                    {
                        AdditionalUserInfo = res.AdditionalUserInfo,
                        AppID = application.AppID, //This will fetch the AppId from the AppList Table
                                                   //  AppLogID = entry.AppLogID, //PRIMARY KEY
                        LogDetail = res.LogDetail,
                        LogDetail_Additional = res.LogDetail_Additional,
                        LogTime = res.LogTime,
                        UserName = res.UserName,
                        Workstation = res.Workstation
                    });
                    dbConnect.SaveChanges();
                }
            }
        }


        public void worker_Process(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            worker.ReportProgress(0, String.Format("Processing Iteration 1."));
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                worker.ReportProgress((i + 1) * 10, String.Format("Processing Iteration {0}.", i + 2));
            }

            worker.ReportProgress(100, "Done Processing.");
        }
    }

    /// <summary>
    /// Using singleton to maintain a DBconnection object if application requires realtime saving records to Database.
    /// </summary>
    public class WriteToDbLogInstance : IDisposable
    {

        /// <summary>
        /// Create a single instance of the object that inherits IDisposable to dispose of the class including the Database Connection object
        /// </summary>
        public static readonly WriteToDbLogInstance WriteToDbInstance = new WriteToDbLogInstance();

        /// <summary>
        /// Create an static instance of the DB Entity object <see cref="DBEntityModel"/> to maintain one connection object throughout the lifecycle of the calling application.
        /// </summary>
        private static DatabaseLog dbConnect = new DatabaseLog();
        bool disposed = false;

        public void WriteLogInstance(AppLog entry, string applicationName, bool displayInUI)
        {
            AppLog appLog = new AppLog();

            if (disposed)
                dbConnect = new DatabaseLog();

            AppList application = new AppList();
            application = dbConnect.AppLists.Where(s => s.AppCode == applicationName).FirstOrDefault();
            if (application == null)
                 application = new AppList();

            if (applicationName == string.Empty || application.AppID == 0)
                application.AppID = 99; //unknown 

            //Write into the database what gets passed

            appLog = dbConnect.AppLogs.Add(new AppLog
            {
                AdditionalUserInfo = entry.AdditionalUserInfo,
                AppID = application.AppID, //This will fetch the AppId from the AppList Table
                                           //  AppLogID = entry.AppLogID, //PRIMARY KEY
                LogDetail = entry.LogDetail,
                LogDetail_Additional = entry.LogDetail_Additional,
                LogTime = entry.LogTime,
                UserName = entry.UserName,
                Workstation = entry.Workstation
            });
            dbConnect.SaveChanges();
        }
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                dbConnect.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }


    }
}
