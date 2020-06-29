using Biblioteka.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Biblioteka.ViewModel.SubViewModel
{
    class AddAuthorSubViewModel : INotifyPropertyChanged
    {

        public SaveAuthorCommand saveAuthorCommand { get; set; }
        public AddAuthorSubViewModel()
        {

            saveAuthorCommand = new SaveAuthorCommand(this);
        }
        public void SaveAuthor()
        {
            Console.WriteLine("Zapis");
        }


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
