using Biblioteka.Commands;
using Biblioteka.Model;
using Biblioteka.SQLConnection;
using Biblioteka.View.SubView;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka.ViewModel
{
    class AuthorsViewModel : INotifyPropertyChanged
    {
        string[] data = new string[999];
        public static string[] infoTemp = new string[99];
        public AddAuthorCommand addAuthorCommand { get; set; }
        public DeleteAuthorCommand deleteAuthorCommand { get; set; }

        private ObservableCollection<AuthorModel> authorsList;
        public ObservableCollection<AuthorModel> AuthorsList
        {
            get { return authorsList; }
            set 
            { 
                authorsList = value;
                OnPropertyChanged("AuthorsList");
            }
        }

        private AuthorModel selectedRow;
        public AuthorModel SelectedRow
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
                    infoTemp[1] = SelectedRow.FirstNameAuthor;
                    infoTemp[2] = SelectedRow.LastNameAuthor;
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

        private string newFirstNameAuthor;
        public string NewFirstNameAuthor
        {
            get { return newFirstNameAuthor; }
            set
            {
                newFirstNameAuthor = value;
                OnPropertyChanged(nameof(NewFirstNameAuthor));
                if (NewFirstNameAuthor != null && NewLastNameAuthor != null)
                {
                    CanCreate = true;
                }
                else
                {
                    CanCreate = false;
                }
            }
        }
        private string newLastNameAuthor;
        public string NewLastNameAuthor
        {
            get { return newLastNameAuthor; }
            set
            {
                newLastNameAuthor = value;
                OnPropertyChanged(nameof(NewLastNameAuthor));
                if (NewFirstNameAuthor != null && NewLastNameAuthor != null)
                {
                    CanCreate = true;
                }
                else
                {
                    CanCreate = false;
                }
            }
        }


        public AuthorsViewModel()
        {
           
            authorsList = new ObservableCollection<AuthorModel>
            {
                //new AuthorModel { Id=1, FirstNameAuthor="Jan", LastNameAuthor="Kowalski"},
                //new AuthorModel { Id = 2, FirstNameAuthor = "Ambrozja", LastNameAuthor = "Raduńska" }
            };
            DataGrid_Loaded();
            addAuthorCommand = new AddAuthorCommand(this);
            deleteAuthorCommand = new DeleteAuthorCommand(this);
        }

        private void DataGrid_Loaded()
        {
            DBClass.openConnection();
            DBClass.sql = "select * from authors";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            AuthorsList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    AuthorsList.Add(new AuthorModel
                    {
                        Id = Int32.Parse(data[0]),
                        FirstNameAuthor = data[1],
                        LastNameAuthor = data[2]
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

        public void AddAuthor()
        {
            //TypeBooks_Loaded();
            // do osobnego okienka z wpisem nowego autora
            //AddAuthorSubView addAuthorSubView = new AddAuthorSubView();
            //addAuthorSubView.ShowDialog();
            //OnPropertyChanged("AuthorsList");
            DBClass.openConnection();
            DBClass.sql = "insert into authors( FirstName, LastName ) values " +
                "('" + newFirstNameAuthor + "', '" + newLastNameAuthor + "')";
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

        public void DeleteAuthor()
        {
            DBClass.openConnection();
            DBClass.sql = "delete from authors where authorid = '" + infoTemp[0] + "'";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            //DataGrid refresh;
            DataGrid_Loaded();
        }

        public string[] InfoAuthorGetter()
        {
            string[] selectedPatient = new string[9];
            selectedPatient[0] = infoTemp[0];
            selectedPatient[1] = infoTemp[1];
            return selectedPatient;
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
;