using Biblioteka.Commands;
using Biblioteka.Model;
using Biblioteka.SQLConnection;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;

namespace Biblioteka.ViewModel
{
    class TypesBooksViewModel : INotifyPropertyChanged
    {
        string[] data = new string[999];
        public static string[] infoTemp = new string[99];

        public AddTypeBookCommand addTypeBookCommand { get; set; }
        public DeleteTypeBookCommand deleteTypeBookCommand { get; set; }

        private ObservableCollection<TypeBooksModel> typesBooksList;
        public ObservableCollection<TypeBooksModel> TypesBooksList
        {
            get { return typesBooksList; }
            set
            {
                typesBooksList = value;
                OnPropertyChanged("TypesBooksList");
            }
        }

        private TypeBooksModel selectedRow;
        public TypeBooksModel SelectedRow
        {
            get { return selectedRow; }
            set
            {
                IsSelected = true;
                selectedRow = value;
                OnPropertyChanged("SelectedRow");
                if (selectedRow != null)
                {
                    infoTemp[0] = SelectedRow.Id.ToString();
                    infoTemp[1] = SelectedRow.TypeBooks;
                }
                else
                {
                    IsSelected = false;
                }
            }
        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private bool canCreate = false;
        public bool CanCreate
        {
            get { return canCreate; }
            set
            {
                canCreate = value;
                OnPropertyChanged(nameof(CanCreate));
            }
        }

        private string newTypeBook;
        public string NewTypeBook
        {
            get { return newTypeBook; }
            set
            {
                newTypeBook = value;
                OnPropertyChanged(nameof(NewTypeBook));
                if (NewTypeBook != null)
                {
                    CanCreate = true;
                }
                else
                {
                    CanCreate = false;
                }
            }
        }

        public void AddTypeBook()
        {
            DBClass.openConnection();
            DBClass.sql = "insert into typeBook( nameTypeBook ) values " +
                "('" + newTypeBook + "')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;

            DBClass.closeConnection();
            DataGrid_Loaded();
        }

        public string[] InfoTypeBookGetter()
        {
            string[] selectedTypeBook = new string[9];
            selectedTypeBook[0] = infoTemp[0];
            selectedTypeBook[1] = infoTemp[1];
            return selectedTypeBook;
        }

        public void DeleteTypeBook()
        {
            DBClass.openConnection();
            DBClass.sql = "delete from typeBook where typeBookId = '" + infoTemp[0] + "'";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            //DataGrid refresh;
            DataGrid_Loaded();
        }

        public TypesBooksViewModel()
        {
            addTypeBookCommand = new AddTypeBookCommand(this);
            deleteTypeBookCommand = new DeleteTypeBookCommand(this);
            typesBooksList = new ObservableCollection<TypeBooksModel>();
            DataGrid_Loaded();
        }

        private void DataGrid_Loaded()
        {
            DBClass.openConnection();
            DBClass.sql = "select * from typebook";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            TypesBooksList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    TypesBooksList.Add(new TypeBooksModel
                    {
                        Id = Int32.Parse(data[0]),
                        TypeBooks = data[1]
                    });
                }
            }
            DBClass.closeConnection();

            try
            {

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void INotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
