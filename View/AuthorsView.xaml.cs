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

namespace Biblioteka.View
{
    /// <summary>
    /// Logika interakcji dla klasy AuthorsView.xaml
    /// </summary>
    public partial class AuthorsView : UserControl
    {
        public AuthorsView()
        {
            InitializeComponent();
            this.DataContext = new AuthorsViewModel();
        }
    }
}
