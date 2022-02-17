using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public ICommand ReadCommand { get; init; } = new RelayCommand(x => { Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(x); /* A vous de définir la commande */  });
    
        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public Book CurrentBook { get; init; }
        public ICommand ReadCommand { get; set; }


        public DetailsBook(Book book)
        {
            CurrentBook = book;

            ReadCommand = new RelayCommand(book => { Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(book); });

        }
        //public ObservableCollection<Book> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;



    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
      public InDesignDetailsBook() : base(new Book() /*{ Title = "Test Book" }*/) { }
    }
}
