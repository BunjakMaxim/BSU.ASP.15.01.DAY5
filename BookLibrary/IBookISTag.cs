using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public interface IBookISTag
    {
        bool IsTag(Book b, string tag);
    }
}
