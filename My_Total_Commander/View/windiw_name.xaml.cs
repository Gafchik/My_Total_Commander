using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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

namespace My_Total_Commander.View
{
    /// <summary>
    /// Логика взаимодействия для New_Folder.xaml
    /// </summary>
    public partial class windiw_name : Window
    {
        private string path;
        private string item;
        public windiw_name(string value)
        {
            path = value;
            InitializeComponent();
            btn.Click += newFolder;
        }
        public windiw_name(string Curr_puth,string S_item )
        {
            path = Curr_puth;
            item = S_item;
            InitializeComponent();
            btn.Click += rename;
        }
        private void newFolder(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Name.Text == null)
                return;                  
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(Name.Text);
            Close();
        }
        private void rename(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Name.Text == null)
                return;
           // DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (item[0] == '[')
            {
                string temp = item;
                temp = temp.Remove(temp.IndexOf('['), 1);
                temp = temp.Remove(temp.IndexOf(']'), 1);
                Directory.Move(path  + temp, path + Name.Text);
                Close();
            }
            else
            {
                FileInfo info = new FileInfo(path + item);
                File.Move(path +'\\' + item, path + '\\' + Name.Text+info.Extension);
                Close();
            }
        }


    }
}
