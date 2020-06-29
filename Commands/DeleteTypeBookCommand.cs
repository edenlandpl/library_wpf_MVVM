using Biblioteka.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Biblioteka.Commands
{
    class DeleteTypeBookCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public static string[] selectedTypeBook = new string[9];
        private TypesBooksViewModel getter;        

        public TypesBooksViewModel vm;

        public DeleteTypeBookCommand(TypesBooksViewModel VM)
        {
            vm = VM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            // wyciągnięcie danych z tabeli - patients
            getter = new TypesBooksViewModel();        //Deklarujemy zmienną typu PatientsView
            selectedTypeBook = getter.InfoTypeBookGetter();
            //SelectedIDPatient = selectedPatient[1];
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć typ " + selectedTypeBook[1], "Potwierdź usunięcie", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.OK:
                    vm.DeleteTypeBook();
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }
    }
}
