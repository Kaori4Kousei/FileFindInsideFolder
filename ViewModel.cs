using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileFindingTool
{
    class ViewModel : INotifyPropertyChanged
    {
        private string _searchName;
        private static int depthCounter=0;
        private List<string> _fileFoundPathList;
        private string _selectedItem;

        public string SearchName
        {
            get => _searchName;
            set
            {
                if (value == _searchName)
                {
                    return;
                }

                _searchName = value;
                OnPropertyChanged();
            }
        }

        private string _searchDirectory;
        private string _fileFoundPath;

        public string SearchDirectory
        {
            get => _searchDirectory;
            set
            {
                if (value == _searchDirectory)
                {
                    return;
                }

                _searchDirectory = value;
                OnPropertyChanged();
            }
        }

        public ICommand FindFile => new RelayCommand(() => {
            ProcessDirectory(SearchDirectory, SearchName);
        }
    );

        public string FileFoundPath
        {
            get => _fileFoundPath;
            set
            {
                if (value == _fileFoundPath)
                {
                    return;
                }

                _fileFoundPath = value;
                OnPropertyChanged();
            }
        }

        private static bool IsFileFound;

        public static ObservableCollection<PathList> FileFoundPathList { get; } = new ObservableCollection<PathList>
        {

        };

        public static void ProcessDirectory(string targetDirectory, string foilName)
        {
            foreach (string file in Directory.GetFiles(targetDirectory, foilName,SearchOption.AllDirectories))
            {
                if (Path.GetFileName(file).Equals(foilName))
                {
                    FileFoundPathList.Add(new PathList() { FileFoundPath = file });
                }
            }
            // Process the list of files found in the directory.
            //string[] fileEntries = Directory.GetFiles(targetDirectory);

            //foreach (string fileName in fileEntries)
            //    if (Path.GetFileName(fileName).Equals(foilName))
            //    {
            //        Process.Start(fileName);
            //        return true;
            //    }

            //string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            //foreach (string subdirectory in subdirectoryEntries)
            //    if (ProcessDirectory(subdirectory, foilName)) return true;

            //return false;

            //to avoid recursion
        }

        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value == _selectedItem)
                {
                    return;
                }

                _selectedItem = value;
                OnPropertyChanged();
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}