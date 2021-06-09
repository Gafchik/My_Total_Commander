using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Controls;

namespace My_Total_Commander.ViewModel
{
    public class ModelView : INotifyPropertyChanged
    {

        public ModelView()
        {
            Items = new ObservableCollection<string>();
            Disks = new ObservableCollection<string>();
            Environment.GetLogicalDrives().ToList().ForEach(i => Disks.Add(i));
        }
        public void InitializeComponen(string path)
        {
            if (Items.Count != 0)
                Items.Clear();

            foreach (var item in Directory.EnumerateDirectories(path).ToList())
            {
                DirectoryInfo info = new DirectoryInfo(item);
                Items.Add("[" + info.Name + "]");
            }
            foreach (var item in Directory.GetFiles(path).ToList())
            {
                FileInfo info = new FileInfo(item);
                Items.Add(info.Name);
            }

        }

       


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
        public ObservableCollection<string> Items { get; set; }
        public ObservableCollection<string> Disks { get; set; }

        private string selected_item;
        public string Selected_Item
        {
            get { return selected_item; }
            set { selected_item = value; OnPropertyChanged("Selected_Item"); }
        }
        private string selected_disk;
        public string Selected_Disk
        {
            get { return selected_disk; }
            set
            {
                selected_disk = value; OnPropertyChanged("Selected_Disk");
                Current_Puth = selected_disk;
            }
        }
        private string current_path;
        public string Current_Puth
        {
            get { return current_path; }
            set
            {
                current_path = value;
                InitializeComponen(Current_Puth); OnPropertyChanged("Current_Puth");
            }
        }

        internal void Select()
        {
            string path = Selected_Item;
            if (Selected_Item != null)
            {
                if (Selected_Item[0] == '[')
                {
                    path = path.Remove(path.IndexOf('['), 1);
                    path = path.Remove(path.IndexOf(']'), 1);
                    if (Current_Puth[Current_Puth.Length - 1] != '\\')
                        Current_Puth += '\\';

                    Current_Puth += path;
                    InitializeComponen(Current_Puth);
                }
                else
                {
                    if (File.Exists(Current_Puth + '\\' + Selected_Item))
                        Process.Start(Current_Puth + '\\' + Selected_Item);                    
                }
            }
        }
        internal void Back()
        {
            try
            {
                int start = Current_Puth.LastIndexOf('\\');
                int coutn = Current_Puth.Length - start;
                Current_Puth = Current_Puth.Remove(start, coutn);
                InitializeComponen(Current_Puth);
            }
            catch (Exception)
            { return; }
        }

    }
}
