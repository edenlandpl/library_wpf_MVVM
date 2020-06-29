using Biblioteka.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Biblioteka.Commands
{
    class UpdateBookCommand  : ICommand
    {
        public BooksViewModel vm { get; set; }

    public UpdateBookCommand(BooksViewModel VM)
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
        vm.UpdateBook();
    }
}
}
