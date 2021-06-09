using System;
using System.Collections.Generic;
using System.IO;
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

namespace My_Total_Commander.View
{
    /// <summary>
    /// Логика взаимодействия для New_Folder.xaml
    /// </summary>
    public partial class New_Folder : Window
    {
        private string path;
        public New_Folder(string value)
        {
            path = value;
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Name.Text == null)
                return;
            DirectoryInfo dir = new DirectoryInfo(path);
            dir.CreateSubdirectory(Name.Text);
            Close();
        }


    }
}
