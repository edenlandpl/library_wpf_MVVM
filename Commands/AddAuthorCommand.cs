using Biblioteka.ViewModel;
using System;
using System.Windows.Input;

namespace Biblioteka.Commands
{
    class AddAuthorCommand : ICommand
    {
        public AuthorsViewModel vm { get; set; }

        public AddAuthorCommand(AuthorsViewModel VM)
        {
            vm = VM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.AddAuthor();
        }
    }
}
