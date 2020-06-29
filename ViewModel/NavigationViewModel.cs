using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Prolog.Command;

namespace Biblioteka.ViewModel
{
    class NavigationViewModel : INotifyPropertyChanged
    {
        public ICommand AuthorsCommand { get; set; }
        public ICommand TypesBooksCommand { get; set; }
        public ICommand BooksCommand { get; set; }
        private object selectedView;
        public object SelectedView
        {
            get { return selectedView; }
            set { selectedView = value; OnPropertyChanged("SelectedView"); }
        }
        public NavigationViewModel()
        {
            AuthorsCommand = new BaseCommand(OpenAuthors);
            TypesBooksCommand = new BaseCommand(OpenTypesBooks);
            BooksCommand = new BaseCommand(OpenBooks);
        }

        private void OpenBooks(object obj)
        {
            SelectedView = new BooksViewModel();
        }

        private void OpenTypesBooks(object obj)
        {
            SelectedView = new TypesBooksViewModel();
        }

        private void OpenAuthors(object obj)
        {
            SelectedView = new AuthorsViewModel();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));               
            }
        }
    }
}
