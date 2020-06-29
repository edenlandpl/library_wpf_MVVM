using Biblioteka.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteka.View.SubView
{
    /// <summary>
    /// Logika interakcji dla klasy AddAuthorSubView.xaml
    /// </summary>
    public partial class AddAuthorSubView
    {
        public AddAuthorSubView()
        {
            InitializeComponent();
            this.DataContext = new AuthorsViewModel();
        }
    }
}
