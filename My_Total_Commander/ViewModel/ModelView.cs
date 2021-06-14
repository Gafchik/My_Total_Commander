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
using My_Total_Commander.View.ViewModel;
using System.Collections.Specialized;
using System.Windows;
using My_Total_Commander.View;

namespace My_Total_Commander.ViewModel
{
    public class ModelView : INotifyPropertyChanged
    {
        
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
        public ObservableCollection<string> Items { get; set; }
        public ObservableCollection<string> Disks { get; set; }
        public StringCollection Buffer { get; set; }
        
        public ModelView()
        {
            Buffer = new StringCollection();
            Items = new ObservableCollection<string>();
            Disks = new ObservableCollection<string>();
            Environment.GetLogicalDrives().ToList().ForEach(i => Disks.Add(i));
        }
        public void InitializeComponen(string path)
        {
            try
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
            catch (Exception)
            { return; }
           

        }

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

        private RelayCommand select;
        public RelayCommand Select
        {
            get
            {
                return select ?? (select = new RelayCommand(act =>
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
                }));
            }
        }


        // команда возврата по меню
        private RelayCommand back;
        public RelayCommand Back
        {
            get
            {
                return back ?? (back = new RelayCommand(act =>
                {
                    if (Current_Puth == null)
                        return;
                    try
                    {
                        foreach (var item in Disks.ToList())
                        {
                            if (item == Current_Puth)
                                return;
                        }
                        int start = Current_Puth.LastIndexOf('\\')+1;
                        int coutn = Current_Puth.Length - start;
                        Current_Puth = Current_Puth.Remove(start, coutn);
                        InitializeComponen(Current_Puth);
                    }
                    catch (Exception)
                    { return; }


                }));
            }
        }

        private RelayCommand dell;
        public RelayCommand Dell
        {
            get
            {
                return dell ?? (dell = new RelayCommand(act =>
                {
                    if (Selected_Item != null)
                    {
                        if (MessageBox.Show("Удалить ?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                            return;
                        if (Selected_Item[0] == '[')
                        {
                            string path = Selected_Item;
                            path = path.Remove(path.IndexOf('['), 1);
                            path = path.Remove(path.IndexOf(']'), 1);                          
                            Directory.Delete(Current_Puth + '\\' + path);
                            InitializeComponen(Current_Puth);
                        }
                        else
                        {
                            File.Delete(Current_Puth + '\\' + Selected_Item);
                            InitializeComponen(Current_Puth);
                        }
                            
                    }
                }));
            }
        }

        private RelayCommand copy;
        public RelayCommand Copy
        {
            get
            {
                return copy ?? (copy = new RelayCommand(act =>
                 {
                     if (Selected_Item != null)
                     {
                         Buffer.Add(Current_Puth + '\\' + Selected_Item);
                         Clipboard.SetFileDropList(Buffer);
                         MessageBox.Show("Скопировано в буфер обмена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                     }
                 
                 }));
            }
        }
       
        private RelayCommand paste;
        public RelayCommand Paste
        {
            get
            {
                return paste ?? (paste = new RelayCommand(act =>
                 {
                     if (Current_Puth != null)
                     {
                         foreach (var item in Disks.ToList())
                         {
                             if (item == Current_Puth)
                                 return;
                         }
                         Buffer.Clear();
                       
                      

                     }
                 }));
            }
        }

        private RelayCommand new_folder;
        public RelayCommand New_Folder
        {
            get
            {
                return new_folder ?? (new_folder = new RelayCommand(act =>
                {
                    if (Current_Puth != null)
                    {
                        foreach (var item in Disks.ToList())
                        {
                            if (item == Current_Puth)
                                return;
                        }
                        var win = new windiw_name(Current_Puth);
                        win.ShowDialog();
                        InitializeComponen(Current_Puth);

                    }
                }));
            }
        }
        
        private RelayCommand rename;
        public RelayCommand ReName
        {
            get
            {
                return rename ?? (rename = new RelayCommand(act =>
                {
                    if (Selected_Item != null)
                    {
                        
                        var win = new windiw_name(Current_Puth,Selected_Item);
                        win.ShowDialog();
                        InitializeComponen(Current_Puth);

                    }
                }));
            }
        }

       
    }
}
