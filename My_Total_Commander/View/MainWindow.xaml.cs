using My_Total_Commander.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            UpData.Click += UpData_Click;


        }

        private void UpData_Click(object sender, RoutedEventArgs e) => (DataContext as ModelView).InitializeComponen((DataContext as ModelView).Current_Puth);
     
        private void Listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e) => (DataContext as ModelView).Select.Execute(sender);
      
    }
}
