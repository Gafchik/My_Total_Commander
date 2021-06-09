using My_Total_Commander.ViewModel;
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

namespace My_Total_Commander
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ModelView();
            listbox.MouseDoubleClick += Listbox_MouseDoubleClick;
            current_Path.TextChanged += Current_Path_TextChanged;


        }

        private void Current_Path_TextChanged(object sender, TextChangedEventArgs e) => (DataContext as ModelView).InitializeComponen(current_Path.Text);



        private void Listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e) => (DataContext as ModelView).Select.Execute(sender);
      
    }
}
