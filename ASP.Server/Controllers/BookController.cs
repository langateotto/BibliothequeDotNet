﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP.Server.Controllers
{
    public class CreateBookModel
    {
        [Required]
        [Display(Name = "Nom")]
        public String Name { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre
        public String Author { get; set; }
        public String Content { get; set; }
        public Double Price { get; set; }
        public DateTime Created { get; set; }

        // Liste des genres séléctionné par l'utilisateur
        //public List<int> Genres { get; set; }
        //public ICollection<Genre> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init;  }
    }

    public class EditBookModel 
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public String Name { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre
        public String Author { get; set; }
        public String Content { get; set; }
        public Double Price { get; set; }
        public DateTime Created { get; set; }

        // Liste des genres séléctionné par l'utilisateur
        //public List<int> Genres { get; set; }
        //public ICollection<Genre> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init; }
    }

    public class BookController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            List<Book> ListBooks = libraryDbContext.Books.ToList();
            return View(ListBooks);
        }

        public ActionResult<CreateBookModel> Create(CreateBookModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                // Il faut intéroger la base pour récupérer l'ensemble des objets genre qui correspond aux id dans CreateBookModel.Genres
                 List<Genre> genres = null;
                // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
                libraryDbContext.Add(new Book() {
                    Name = book.Name, 
                    Author = book.Author, 
                    Content = book.Content,
                    Price = book.Price,
                    Created = book.Created
                });
                libraryDbContext.SaveChanges();
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookModel() { AllGenres = null } );
        }

        public ActionResult<EditBookModel> Edit(int id)
        {
            Book book = libraryDbContext.Books.First(x => x.Id == id);

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new EditBookModel() { Id=book.Id, Name=book.Name, Author=book.Author, Content=book.Content, Price=book.Price, Created=book.Created});
        }

        public ActionResult<EditBookModel> Update(EditBookModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                Book existingBook = libraryDbContext.Books.First(x => x.Id == book.Id);
                // Il faut intéroger la base pour récupérer l'ensemble des objets genre qui correspond aux id dans CreateBookModel.Genres
                List<Genre> genres = null;
                // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
                
                existingBook.Name = book.Name;
                existingBook.Author = book.Author;
                existingBook.Content = book.Content;
                existingBook.Price = book.Price;
                existingBook.Created = book.Created;

                libraryDbContext.Update<Book>(existingBook);
                libraryDbContext.SaveChanges();
                return RedirectToAction("List");
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new EditBookModel() { AllGenres = null });
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Book book = libraryDbContext.Books.First(g => g.Id == id);
                if (book != null)
                {

                    libraryDbContext.Books.Remove(book);
                    libraryDbContext.SaveChanges();
                    return RedirectToAction("List");
                }
                else
                {
                    ///Todo
                    return RedirectToAction("List");
                }
            }
            catch
            {
                ///Todo
                return RedirectToAction("List");
            }
        }
    }
}