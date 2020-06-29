using Biblioteka.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.AppDbContext
{
    class AppDbContext : DbContext
    {
        //public AppDbContext()
        //{

        //}

        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<TypeBooksModel> TypeBooks { get; set; }
        public DbSet<BookModel> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Apki\\C#\\Biblioteka\\Biblioteka\\Biblioteka.mdf;Integrated Security=True;Connect Timeout=30");


            base.OnConfiguring(optionsBuilder);
        }
    }
}
