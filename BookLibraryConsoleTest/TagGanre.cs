using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibrary;

namespace BookLibraryConsoleTest
{
    class TagGanre : IBookISTag
    {
        public bool IsTag(Book b, string tag)
        {
            return b.Genre == tag;
        }
    }
}
