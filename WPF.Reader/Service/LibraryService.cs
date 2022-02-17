using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPF.Reader.Model;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        public static string Author { get; private set; }
        public static string Name { get; private set; }
        public static int Price { get; private set; }
        public static string Content { get; private set; }
        public static List<Genre> Genres { get; private set; }

        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouer et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>() {
            new Book(),{ Author = "Albert Camus", Name = "L'etranger", Price = 500, Content = "Lorem ipsum", Genres = new List<Genre>() { SF, Thriller } },
            new Book(),
            new Book(),
        };

        // C'est aussi ici que vous ajouterez les requète réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
    }
}
