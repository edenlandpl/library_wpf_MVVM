using Biblioteka.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Biblioteka.Commands
{
    class DeleteAuthorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public static string[] selectedAuthor = new string[9];
        private AuthorsViewModel getter;  

        public AuthorsViewModel vm;

        public DeleteAuthorCommand(AuthorsViewModel VM)
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
            getter = new AuthorsViewModel();        //Deklarujemy zmienną typu PatientsView
            selectedAuthor = getter.InfoAuthorGetter();
            //SelectedIDPatient = selectedPatient[1];
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć autora " + selectedAuthor[1] + " " + selectedAuthor[2], "Potwierdź usunięcie autora", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.OK:
                    vm.DeleteAuthor();
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }
    }
}
