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
        private static int depthCounter=0;
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
            IsFileFound=ProcessDirectory(SearchDirectory, SearchName);
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


        public static bool ProcessDirectory(string targetDirectory, string foilName)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            foreach (string fileName in fileEntries)
                if (Path.GetFileName(fileName).Equals(foilName))
                {
                    Process.Start(fileName);
                    return true;
                }

            if(++depthCounter==10)
            {
                return false;
            }

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                if (ProcessDirectory(subdirectory, foilName)) return true;

            return false;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}