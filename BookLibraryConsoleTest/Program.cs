using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using BookLibrary;

namespace BookLibraryConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"BooksList.dat";

            BookListService books = new BookListService(path);
            ShowBooksList(books, "Спосок книг");

            /*
             * bl.AddBook(new Book() { Author = "Лев Толстой", Title = "Война и мир", Genre = "Роман", Year = 1869 });
             * bl.AddBook(new Book() { Author = "Алан Александр Милн",Title ="Вини-Пух", Genre = "Детский рассказ", Year = 1926});
             * bl.AddBook(new Book() { Author = "Мари Шелли", Title="Франкенштейн", Genre = "Научная фантастика", Year = 1818}); 
             * bl.AddBook(new Book() { Author = "Михаил Булгаков", Title ="Мастер и Маргарита", Genre="Роман", Year = 1966});
             * bl.AddBook(new Book() { Author = "Федор Достоевский", Title = "Преступление и наказание" , Genre = "Роман" , Year = 1866});
             * bl.AddBook(new Book() { Author = "Николай Гоголь", Title = "Мёртвые души", Genre = "Сатира", Year = 1842 });
             * bl.AddBook(new Book() { Author = "Александр Пушкин", Title = "Евгений Онегин", Genre = "Роман", Year = 1825 });
             */

            books.SortBooksByTag( new ComparableBook());
            ShowBooksList(books, "Отсортированный список");

            books.AddBook(new Book() { Author = "Николай Гоголь", Title = "Вечера на хуторе близ Диканьки", Year = 1832, Genre = "Проза"});
            ShowBooksList(books, "Список книг с добавленной книгой");

            books.RemoveBook(new Book() { Author = "Николай Гоголь", Title = "Вечера на хуторе близ Диканьки", Year = 1832, Genre = "Проза" });
            ShowBooksList(books, "Список книг с удаленной книгой");

            Console.WriteLine("Спосок книг выбранны по тегу \"Жанр = Роман\"");
            foreach(Book b in books.FindByTag(new TagGanre(), "Роман"))
                Console.WriteLine(b);
        }

        private static void ShowBooksList(BookListService books, string massege)
        {
            Console.WriteLine(massege);
            foreach (Book b in books)
                Console.WriteLine(b);

            Console.WriteLine();
        }
    }
}
