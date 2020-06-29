using Biblioteka.ViewModel;
using Biblioteka.ViewModel.SubViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteka.Commands
{
    class SaveAuthorCommand
    {
        public AddAuthorSubViewModel vm { get; set; }

        public SaveAuthorCommand(AddAuthorSubViewModel VM)
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
            Console.WriteLine("Command");
            vm.SaveAuthor();
        }
    }
}
