using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace BookLibrary
{
    public class BookListService : IEnumerable<Book>
    {
        private Book[] arrayBooks;
        private readonly string path;
        public int CountBook { get; private set; }
        public BookListService(string path)
        {
            this.path = path;
            ReadBooksList();
        }

        public void AddBook(Book book)
        {
            if (FindBookInArray(book) != -1)
                throw new Exception("Повторное добавление книги!");

            if(arrayBooks.Length + 1 == CountBook)
            {
                Book[] array = new Book[CountBook * 2];
                Array.Copy(arrayBooks, array, CountBook);
                arrayBooks = array;
            }

            arrayBooks[CountBook++] = book;
        }

        public void RemoveBook(Book book)
        {
            int j = FindBookInArray(book);

            if(j < 0)
                throw new Exception("Книга не  найдена");

            for(int i = j; i < CountBook - 1; i++)
                arrayBooks[i] = arrayBooks[i + 1];
            CountBook--;
        }

        public Book[] FindByTag(IBookISTag bookTag, string tag)
        {
            Book[] booksIsTag = new Book[10];
            int ind = 0;
            for(int i = 0; i < CountBook; i++)
                if(bookTag.IsTag(arrayBooks[i], tag))
                {
                    if(ind >= booksIsTag.Length)
                    {
                        Book[] newBooksList = new Book[booksIsTag.Length * 2];
                        Array.Copy(booksIsTag, newBooksList, ind);
                        booksIsTag = newBooksList;
                    }
                    booksIsTag[ind++] = arrayBooks[i];
                }

            Book[] n = new Book[ind];
            Array.Copy(booksIsTag, n, ind);
            return n;
        }

        public void	SortBooksByTag(IComparer<Book> c)
        {
            if (c == null)
                throw new ArgumentNullException();

            int end = CountBook - 1;
            for(int i = 0; i < CountBook; i++)
            {
                for(int j = 0; j < end; j++)
                    if(c.Compare(arrayBooks[j], arrayBooks[j+1]) > 0)
                    {
                        Book b = arrayBooks[j];
                        arrayBooks[j] = arrayBooks[j + 1];
                        arrayBooks[j + 1] = b;
                    }
                end--;
            }
        }

        private void ReadBooksList()
        {
            CountBook = 0;
            arrayBooks = new Book[10];

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string autor = reader.ReadString();
                    string title = reader.ReadString();
                    int year = reader.ReadInt32();
                    string genre = reader.ReadString();

                    AddBook(new Book() { Author = autor, Title = title, Year = year, Genre = genre});
                }

                reader.Close();
            }
        }

        public void WriteBooksList()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                foreach (Book b in this)
                {
                    writer.Write(b.Author);
                    writer.Write(b.Title);
                    writer.Write(b.Year);
                    writer.Write(b.Genre);
                }
            }
        }

        public IEnumerator<Book> GetEnumerator()
        {
            for (int i = 0; i < CountBook; i++)
                yield return arrayBooks[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int FindBookInArray(Book b)
        {
            for (int i = 0; i < CountBook; i++)
                if (arrayBooks[i].Equals(b))
                    return i;

            return -1;
        }
    }
}
