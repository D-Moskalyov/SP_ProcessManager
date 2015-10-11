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

namespace ProcessManager
{
    /// <summary>
    /// Логика взаимодействия для ExeList.xaml
    /// </summary>
    public partial class ExeList : Window
    {
        //public int num = 0;
        //public static int count = 0;

        public ExeList()
        {
            InitializeComponent();
            //this.num = ++count;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Owner).addFileEvent += ExeList_addFileEvent;

            for (int i = 0; i < ((MainWindow)Owner).exeFiles.Count; i++)
            {
                listBox.Items.Add(((MainWindow)Owner).exeFiles[i]);
            }
        }

        public void ExeList_addFileEvent(object sender, EventArgsForList e)//?static?
        {
            Console.WriteLine(e.exeFileInfo.ShortName);
            listBox.Items.Add(e.exeFileInfo);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBox.Text = ((ExeFileInfo)listBox.SelectedItem).FullName;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((MainWindow)Owner).addFileEvent -= ExeList_addFileEvent;
        }

    }
}
