using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        private object _books;

        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            Genre SF, Classic, Romance, Thriller;
            bookDbContext.Genre.AddRange(
                SF = new Genre() { Name = "SF" },
                Classic = new Genre() { Name = "Classic" },
                Romance = new Genre() { Name = "Romance" },
                Thriller = new Genre() { Name = "Thriller" }
            );

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            bookDbContext.Books.AddRange(
                new Book() { Author = "Albert Camus", Name = "L'etranger", Price = 500, Content = "Lorem ipsum", Genres = new List<Genre>() { SF, Thriller } }, 
                new Book() { Author = "Marc Levy", Name = "Mes amis, mes amours", Price = 500, Content = "Lorem ipsum", Genres = new List<Genre>() { Romance, SF } },
                new Book() { Author = "Eric Kastner", Name = "Le 35 mai", Price = 500, Content = "Lorem ipsum", Genres = new List<Genre>() { SF, Classic } },
                new Book() { Author = "Brian Mealer", Name = "Le garcon qui dompta le vent", Price = 500, Content = "Lorem ipsum", Genres = new List<Genre>() { Romance, Thriller } }
            );
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }

        public ListBook<Book> Book
    {
        get { return Book; }
    }
       /* public int this[Book book]
        {
            get { return _books.IndexOf(book); }
        }*/
    }
}