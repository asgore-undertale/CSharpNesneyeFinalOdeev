using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Final_Odev_library
{
    public interface ILibraryItemFactory
    {
        LibraryItem CreateLibraryItem(string itemType, string title, Authors authors, string[] additionalData, bool isCheckedOut);
    }

    public class LibraryItemFactory : ILibraryItemFactory
    {
        public LibraryItem CreateLibraryItem(string itemType, string title, Authors authors, string[] additionalData, bool isCheckedOut) // params azaltmak icin iyi bir fikir bulamadim, Dictionary kullanmak istedim ama uygun degildi
        {
            if (itemType == "Book")
            {
                int pageCount = int.Parse(additionalData[0]);
                var book = new Book(title, authors, pageCount);
                if (isCheckedOut) book.CheckOut();
                return book;
            }
            else if (itemType == "DVD")
            {
                double duration = double.Parse(additionalData[0]);
                var dvd = new DVD(title, authors, duration);
                if (isCheckedOut) dvd.CheckOut();
                return dvd;
            }
            else
            {
                throw new ArgumentException("Unknown item type.");
            }
        }
    }

    public abstract class LibraryItem
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string Title { get; protected set; }
        public Authors Authors { get; protected set; }
        public string ItemType { get; protected set; }
        public bool IsCheckedOut { get; protected set; }

        public LibraryItem(string title, Authors authors, string itemType)
        {
            Title = title;
            Authors = authors;
            ItemType = itemType;
            IsCheckedOut = false;
        }

        public abstract string DisplayDetails();
        public virtual void CheckOut() {
            if (IsCheckedOut)
                throw new InvalidOperationException("Item is already checked out.");
            IsCheckedOut = true;
        }
        public virtual void Return() {
            if (!IsCheckedOut)
                throw new InvalidOperationException("Item is not checked out.");
            IsCheckedOut = false;
        }
        public virtual string ToCsv()
        {
            return $"{Id},{ItemType},{Title},{Authors.ToCsv()},{IsCheckedOut}";
        }
    }

}
