using System;
using System.Collections.Generic;
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
            FileSearchEngine();
            IsFileFound = false;
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

        public bool IsFileFound;
        public void FileSearchEngine()
        {
            string fileOME = SearchDirectory + SearchName;
            if(File.Exists(fileOME))
            {
                Process.Start(fileOME);
                IsFileFound = true;
                FileFoundPath = fileOme;
            }

            if(!IsFileFound)
            {
                foreach (string file in Directory.GetFiles(SearchDirectory))
                {
                    if (Path.GetFileName(file).Equals(SearchName))
                    {
                        Process.Start(file);
                        FileFoundPath = file;
                        IsFileFound = true;
                        if (IsFileFound)
                            break;
                    }
                    if (IsFileFound)
                        break;
                }

            }

            if (IsFileFound != true)
            {
                foreach (string FoundDirectory in Directory.GetDirectories(SearchDirectory))
                {
                    foreach (string file in Directory.GetFiles(FoundDirectory))
                    {
                        if (Path.GetFileName(file).Equals(SearchName))
                        {
                            Process.Start(file);
                            FileFoundPath = file;
                            IsFileFound = true;
                        }
                        if (IsFileFound == true)
                            break;
                    }
                    if (IsFileFound)
                        break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}