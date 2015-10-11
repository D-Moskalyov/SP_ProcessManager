using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace ProcessManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public delegate void ScanExeDelegate(DirectoryInfo dI);
    public delegate void FinishScanDelegate();
    
    public partial class MainWindow : Window
    {
        public event EventHandler<EventArgsForList> addFileEvent;
        public FinishScanDelegate finishScanDelegate;

        public List<ExeFileInfo> exeFiles = new List<ExeFileInfo>();
        int disksCount = 0;
        public ScanExeDelegate scanExeDelegate;

        public MainWindow()
        {
            InitializeComponent();
            progressBar.IsIndeterminate = true;

            FillListBox();

            scanExeDelegate = ScanExeFull;
            //scanExeDelegate += SecondMet;
            finishScanDelegate = FinishScan;

            Fill();
        }
        void Fill()
        {
            string[] disks = Environment.GetLogicalDrives();
            List<DirectoryInfo> dIs = new List<DirectoryInfo>();
            disksCount = disks.Length;

            for (int i = 0; i < disks.Length; i++)
            {
                dIs.Add(new DirectoryInfo(disks[i]));
            }

            //Console.Read();

            foreach (DirectoryInfo dI in dIs)
            {
                scanExeDelegate.BeginInvoke(dI, CallbackMethod, scanExeDelegate);
                //ScanDir(dI);
                //break;
            }

            Console.WriteLine("finScan");
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                Process prToRun = new Process();
                prToRun.StartInfo.FileName = openFileDialog.FileName;
                prToRun.Start();
            }
        }

        private void copy_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                //ProcessModule prMod = (Process.GetProcessById(((ProcInfo)listBox.SelectedItem).ID)).MainModule;
                string fileName = Process.GetProcessById(((ProcInfo)listBox.SelectedItem).ID).MainModule.FileName;
                //poc.StartInfo.UseShellExecute = !poc.StartInfo.UseShellExecute;
                //string fileName = poc.MainModule.FileName;
                Console.WriteLine(fileName);
                Process newProcess = new Process();
                newProcess.StartInfo.FileName = fileName;
                newProcess.Start();
            }
        }

        private void kill_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                try
                {
                    (Process.GetProcessById(((ProcInfo)listBox.SelectedItem).ID)).Kill();
                }
                catch (InvalidOperationException exp)
                {
                    MessageBox.Show(exp.Message);
                }
                catch (System.ComponentModel.Win32Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    FillListBox();
                }
            }
        }

        private void exe_Click(object sender, RoutedEventArgs e)
        {
            ExeList existlist = new ExeList();
            existlist.Owner = this;
            //Thread.Sleep(1000);
            //exeFiles.Add("cdc");
            existlist.Show();
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process proc = Process.GetProcessById(((ProcInfo)listBox.SelectedItem).ID);

            ProcessInfo processInfo = new ProcessInfo(proc);
            processInfo.Owner = this;
            processInfo.Show();
        }

        public void ScanExeFull(DirectoryInfo dI)
        { 
            ScanDir(dI);
            Console.WriteLine(dI.FullName);
        }

        void CallbackMethod(IAsyncResult ar)
        {
            ScanExeDelegate del = (ScanExeDelegate)ar.AsyncState;

            if (del != null)
            {
                //int res = del.EndInvoke(ar);
                //Console.WriteLine(res.ToString());
            }

            if (--disksCount == 0)
            {
                Console.WriteLine("*******FINISH***********FINISH************FINISH***********");
                exeFiles.Sort(
                    (a, b) => {
                    if (a.FullName.CompareTo(b.FullName) > 0)
                        return 1;
                    else
                        return -1;
                    });
                Console.WriteLine("****SORTFINISH*********SORTFINISH********SORTFINISH******");

                progressBar.Dispatcher.Invoke(
                    new Action(() =>
                    {
                        progressBar.Visibility = System.Windows.Visibility.Hidden;
                        labal.Content = "***SCAN FINISHED***";
                    }
                ));
            }
        }

        void ScanDir(DirectoryInfo dI)
        {
            List<FileSystemInfo> fSI = new List<FileSystemInfo>();

            try
            {
                fSI.AddRange(dI.GetFiles("*.exe", SearchOption.TopDirectoryOnly));

                if (fSI.Count > 0)
                {
                    foreach (FileSystemInfo fsi2 in fSI)
                    {
                        exeFiles.Add(new ExeFileInfo(fsi2));

                        if (addFileEvent != null)
                        {
                            EventArgsForList eAfL = new EventArgsForList(exeFiles[exeFiles.Count -1]);
                            addFileEvent(this, eAfL);
                        }
                    }
                }

                List<DirectoryInfo> di = new List<DirectoryInfo>();
                di.AddRange(dI.GetDirectories());

                foreach (DirectoryInfo DI in di)
                {
                    ScanDir(DI);
                }
            }
            catch (SystemException exp)
            {
                //Console.WriteLine(exp.Message);
            }
        }

        void FillListBox()
        {
            List<ProcInfo> prInfoList = new List<ProcInfo>();

            listBox.Items.Clear();
            Process[] allProcess = Process.GetProcesses();
            foreach (Process pr in allProcess)
            {
                //Console.WriteLine(pr.StartInfo.FileName);
                ProcInfo prInfo = new ProcInfo(pr.ProcessName, pr.Id);
                prInfoList.Add(prInfo);

            }

            foreach (ProcInfo pr in prInfoList)
                listBox.Items.Add(pr);
        }

        public void FinishScan()
        {
            //.Visibility = System.Windows.Visibility.Hidden;
        }

    }
}
