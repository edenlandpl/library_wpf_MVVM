using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Biblioteka.Model
{
    class BookModel
    {
        public int Id { get; set; }
        public string publisher { get; set; }
        public string releaseDate { get; set; }
        public int IdAuthor { get; set; }
        public string firstNameAuthor { get; set; }
        public string lastNameAuthor { get; set; }
        public int IdTypeBook { get; set; }
        public string typeBook { get; set; }
        public string titleBook { get; set; }



    }
}
