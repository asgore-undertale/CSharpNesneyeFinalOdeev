using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Final_Odev_library
{
    public class Book : LibraryItem
    {
        private int _pageCount;

        public Book(string title, Authors authors, int pageCount)
            : base(title, authors, "Book")
        {
            if (pageCount <= 0)
                throw new ArgumentException("Page count must be greater than zero.");
            _pageCount = pageCount;
        }

        public override string DisplayDetails() {
            return $"[Book] ID: {Id}\nTitle: {Title}\nAuthors: {Authors.ToString()}\nPages: {_pageCount}\nChecked Out: {IsCheckedOut}";
        }

        public override string ToCsv()
        {
            return $"{Id},{ItemType},{Title},{Authors.ToCsv()},{_pageCount},{IsCheckedOut}";
        }
    }
}
