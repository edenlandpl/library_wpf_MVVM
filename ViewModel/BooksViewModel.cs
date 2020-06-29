using Biblioteka.Commands;
using Biblioteka.Model;
using Biblioteka.SQLConnection;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;

namespace Biblioteka.ViewModel
{
    class BooksViewModel
    {
        string[] data = new string[999];
        public static string[] infoTemp = new string[99];
        public static string[] selectedTemp = new string[99];

        public UpdateBookCommand updateBookCommand { get; set; }
        public AddBookCommand addBookCommand { get; set; }
        public DeleteBookCommand deleteBookCommand { get; set; }


        private ObservableCollection<BookModel> booksList;
        public ObservableCollection<BookModel> BooksList
        {
            get { return booksList; }
            set
            {
                booksList = value;
                OnPropertyChanged("BooksList");
            }
        }
        

        public ObservableCollection<string> authorsList;
        public ObservableCollection<string> AuthorsList
        {
            get { return authorsList; }
            set
            {
                authorsList = value;
                OnPropertyChanged("AuthorsList");
            }
        }

        internal void DeleteBook()
        {
            DBClass.openConnection();
            DBClass.sql = "delete from books where bookid = '" + selectedTemp[0] + "'";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            //DataGrid refresh;
            DataGrid_Loaded();
        }

        public ObservableCollection<string> titleList;
        public ObservableCollection<string> TitleList
        {
            get { return titleList; }
            set
            {
                titleList = value;
                OnPropertyChanged("TitleList");
            }
        }


        Dictionary<string, string> typeBookDictionaryList = new Dictionary<string, string>();
        Dictionary<string, string> authorsDictionaryList = new Dictionary<string, string>();

        public ObservableCollection<string> typeBookList;
        public ObservableCollection<string> TypeBookList
        {
            get { return typeBookList; }
            set
            {
                typeBookList = value;
                OnPropertyChanged("TypeBookList");
            }
        }


        private BookModel selectedRow;
        public BookModel SelectedRow
        {
            get { return selectedRow; }
            set
            {
                isSelected = true;
                selectedRow = value;
                OnPropertyChanged("SelectedRow");
                if (selectedRow != null)
                {
                    selectedTemp[0] =  SelectedRow.Id.ToString();
                    selectedTemp[1] = SelectedRow.titleBook;
                    selectedTemp[2] = SelectedRow.typeBook;
                    selectedTemp[3] = SelectedRow.publisher;
                    selectedTemp[4] = SelectedRow.firstNameAuthor;
                    selectedTemp[5] = SelectedRow.lastNameAuthor;
                    selectedTemp[6] = SelectedRow.releaseDate;
                }
                else
                {
                    isSelected = false;
                }
            }
        }

        private bool isSelected = true;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));

            }
        }

        private bool canCreate = true;
        public bool CanCreate
        {
            get { return canCreate; }
            set
            {
                canCreate = value;
                OnPropertyChanged(nameof(CanCreate));
            }
        }

        public BooksViewModel()
        {
            updateBookCommand = new UpdateBookCommand(this);
            addBookCommand = new AddBookCommand(this);
            deleteBookCommand = new DeleteBookCommand(this);
            booksList = new ObservableCollection<BookModel> { };
            typeBookList = new ObservableCollection<string> { };
            authorsList = new ObservableCollection<string>{ };
            titleList = new ObservableCollection<string> { };
            readAuthors();
            readTitles();
            readTypes();
            DataGrid_Loaded();
        }

        private void readAuthors()
        {
            DBClass.openConnection();
            DBClass.sql = "select authorId, lastName from authors";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            AuthorsList.Clear();
            authorsDictionaryList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                        authorsList.Add(data[1]);
                    }
                    authorsDictionaryList.Add(data[1], data[0]);
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
            DataGrid_Loaded();
        }

        private void readTypes()
        {
            DBClass.openConnection();
            DBClass.sql = "select typeBookId, nameTypeBook from typeBook";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            typeBookDictionaryList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                        
                    }
                    TypeBookList.Add(data[1]);
                    typeBookDictionaryList.Add(data[1], data[0]);
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
            DataGrid_Loaded();
        }

        private void readTitles()
        {
            DBClass.openConnection();
            DBClass.sql = "select titileBook from books";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            TitleList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                        titleList.Add(data[j]);
                    }

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
            DataGrid_Loaded();
        }


        public void UpdateBook()
        {
            if (SelectedRow == null)
            {
                MessageBox.Show("Zaznacz jakąś książkę", "Brak zaznaczenia"); 
                return;
            }
            DBClass.openConnection();
            DBClass.sql = "update books Set titileBook = '" + SelectedRow.titleBook + "', publisher = '" + SelectedRow.publisher + "', releaseDate = '" + SelectedRow.releaseDate + "' where books.bookId = '" + SelectedRow.Id.ToString() + "' ";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            DBClass.closeConnection();
            DataGrid_Loaded();
        }


        public void AddBook()
        {
            DBClass.openConnection();
            DBClass.sql = "insert into books( titileBook, publisher, typeBookId, authorId, releaseDate ) values " +
                "('" + NewTitleBook + "', '" + NewPublisher + "', '" + typeBookDictionaryList[(NewTypeBook)] + "', '" + authorsDictionaryList[(NewAuthorBook)] + "', '" + NewReleaseDate + "')";
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

        #region Właściwości pól

        private string newTitleBook;
        public string NewTitleBook
        {
            get { return newTitleBook; }
            set
            {
                newTitleBook = value;
                OnPropertyChanged(nameof(NewTitleBook));
                if (NewTypeBook != null && NewTitleBook != null && NewPublisher != null && NewFirstNameAuthor != null && NewLastNameAuthor != null && NewReleaseDate != null )
                {
                    CanCreate = true;
                }
                else
                {
                    CanCreate = false;
                }
            }
        }

        private string newAuthorBook;
        public string NewAuthorBook
        {
            get { return newAuthorBook; }
            set
            {
                newAuthorBook = value;
                OnPropertyChanged(nameof(NewAuthorBook));                
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
                if (NewTypeBook != null && NewTitleBook != null && NewPublisher != null && NewFirstNameAuthor != null && NewLastNameAuthor != null && NewReleaseDate != null)
                {
                    CanCreate = true;
                }
                else
                {
                    CanCreate = false;
                }
            }
        }

        private string newPublisher;
        public string NewPublisher
        {
            get { return newPublisher; }
            set
            {
                newPublisher = value;
                OnPropertyChanged(nameof(NewPublisher));
                if (NewTypeBook != null && NewTitleBook != null && NewPublisher != null && NewFirstNameAuthor != null && NewLastNameAuthor != null && NewReleaseDate != null)
                {
                    CanCreate = true;
                }
                else
                {
                    CanCreate = false;
                }
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
                if (NewTypeBook != null && NewTitleBook != null && NewPublisher != null && NewFirstNameAuthor != null && NewLastNameAuthor != null && NewReleaseDate != null)
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
                if (NewTypeBook != null && NewTitleBook != null && NewPublisher != null && NewFirstNameAuthor != null && NewLastNameAuthor != null && NewReleaseDate != null)
                {
                    CanCreate = true;
                }
                else
                {
                    CanCreate = false;
                }
            }
        }

        private string newReleaseDate;
        public string NewReleaseDate
        {
            get { return newReleaseDate; }
            set
            {
                newReleaseDate = value;
                OnPropertyChanged(nameof(NewReleaseDate));
                if (NewTypeBook != null && NewTitleBook != null && NewPublisher != null && NewFirstNameAuthor != null && NewLastNameAuthor != null && NewReleaseDate != null)
                {
                    CanCreate = true;
                }
                else
                {
                    CanCreate = false;
                }
            }
        }

        private string selectedTypeBook;
        public string SelectedTypeBook
        {
            get { return selectedTypeBook; }
            set
            {
                selectedTypeBook = value;
                OnPropertyChanged(nameof(SelectedTypeBook));

                DataGrid_Loaded();
            }
        }
        
        private string selectedAuthorList = null;
        public string SelectedAuthorList
        {
            get { return selectedAuthorList; }
            set
            {
                selectedAuthorList = value;
                OnPropertyChanged(nameof(SelectedAuthorList));
                DataGrid_Loaded();
            }
        }
        private string selectedTitleList = null;
        public string SelectedTitleList
        {
            get { return selectedTitleList; }
            set
            {
                selectedTitleList = value;
                OnPropertyChanged(nameof(SelectedTitleList));
                DataGrid_Loaded();
            }
        }

        //'" + selectedTitleList + "'
        #endregion
        private void DataGrid_Loaded()
        {
            string typeTemp = "*";
            if (SelectedTypeBook != null)
            {
                typeTemp = typeBookDictionaryList[SelectedTypeBook];
            }
            
            
            DBClass.openConnection();
            if (SelectedAuthorList == null && SelectedTypeBook == null)
            {
                DBClass.sql = "select distinct bookId, titileBook, nameTypeBook, publisher, authors.firstName, authors.lastName, releaseDate " +
                "from authors, typeBook, books " +
                "where  " +
                "(  authors.firstName in (select distinct  authors.firstName from authors where authorId = books.authorId) " +
                "and authors.lastName in (select distinct  authors.lastName from authors where authorId = books.authorId) " +
                "and typeBook.nameTypeBook in (select distinct  nameTypeBook from typeBook where typeBookId = books.typeBookId))";
            }
            else if (SelectedAuthorList == null && SelectedTypeBook != null)
            {
                    DBClass.sql = "select distinct bookId, titileBook , nameTypeBook, publisher, authors.firstName, authors.lastName,  releaseDate " +
                "FROM books " +
                "inner join authors on authors.authorId = books.authorId " +
                "inner join typeBook on typeBook.typeBookId = '" + typeTemp + "' " +
                "where books.authorId = books.authorId " +
                "and books.typeBookID = '" + typeTemp + "'";

            }
            else if (SelectedAuthorList != null && SelectedTypeBook == null)
                {
                    DBClass.sql = "select distinct bookId, titileBook , nameTypeBook, publisher, authors.firstName, authors.lastName,  releaseDate " +
                "from authors, typeBook, books " +
                "where  " +
                "(  authors.firstName in (select distinct  authors.firstName from authors where authorId = books.authorId) " +
                "and authors.lastName in (select distinct  authors.lastName from authors where authors.lastName = '" + selectedAuthorList + "') " +
                "and typeBook.nameTypeBook in (select distinct  nameTypeBook from typeBook where typeBookId = books.typeBookId))";

            }
            else
            {
             DBClass.sql = "SELECT DISTINCT bookId, titileBook , nameTypeBook, publisher, authors.firstName, authors.lastName,  releaseDate " +
                    "FROM books " +
                    "inner join authors on authors.lastName = '" + selectedAuthorList + "' " +
                    "inner join typeBook on typeBook.typeBookId = '" + typeTemp + "'  " +
                    "where books.authorId = (select distinct  authors.authorId from authors where authors.lastName = '" + selectedAuthorList + "') " +
                    "and books.typeBookID = '" + typeTemp + "' ";
            }

            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            BooksList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    BooksList.Add(new BookModel
                    {
                        Id = Int32.Parse(data[0]),
                        titleBook = data[1],
                        typeBook = data[2],
                        publisher = data[3],
                        firstNameAuthor = data[4],
                        lastNameAuthor = data[5],
                        releaseDate = data[6]
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
