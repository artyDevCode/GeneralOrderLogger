using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public interface IViewApplicationLogger
    {
        //ie:
        //WriteToDbLog cl1 = new WriteToDbLog();
        //BackgroundWorker worker = new BackgroundWorker();
        //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        //worker.WorkerReportsProgress = true;
        //worker.DoWork += cl1.worker_Process;
        //worker.ProgressChanged += worker_ProgressChanged;
        //worker.RunWorkerAsync();
        void backgroundWorkingProcess();



        //ie:
        //TestProgressBar.Value = e.ProgressPercentage;
        //ProgressTextBlock.Text = (string) e.UserState;
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e);



        //ie:
        //MessageBox.Show("Revisions Removed!");
        //TestProgressBar.Value = 0;
        //ProgressTextBlock.Text = "";
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);
       }
}
