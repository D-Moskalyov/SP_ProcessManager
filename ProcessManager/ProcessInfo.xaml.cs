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
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;

namespace ProcessManager
{
    /// <summary>
    /// Логика взаимодействия для ProcessInfo.xaml
    /// </summary>
    public partial class ProcessInfo : Window
    {
        Process process;

        public ProcessInfo(Process proc)
        {
            process = proc;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BasePriority.Text = process.BasePriority.ToString();
            Id.Text = process.Id.ToString();
            MachineName.Text = process.MachineName.ToString();
            MainWindowTitle.Text = process.MainWindowTitle.ToString();
            NonpagedSystemMemorySize64.Text = process.NonpagedSystemMemorySize64.ToString();
            PagedMemorySize64.Text = process.PagedMemorySize64.ToString();
            PagedSystemMemorySize64.Text = process.PagedSystemMemorySize64.ToString();
            ProcessName.Text = process.ProcessName.ToString();
            ProcessThreadCollection processThreadCollection = process.Threads;
            foreach (ProcessThread pTC in processThreadCollection)
            {
                comboBox.Items.Add(pTC.Id.ToString());
            }
        }
    }
}
