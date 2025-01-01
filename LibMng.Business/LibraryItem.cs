using LibMng.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMng_Business
{
    public interface ILibraryItemFactory
    {
        LibraryItem CreateLibraryItem(string itemType, string title, Authors authors, string[] additionalData, bool isCheckedOut);
    }

    // Strategy Interface
    public interface ILibraryItemCreationStrategy
    {
        LibraryItem CreateLibraryItem(string title, Authors authors, string[] additionalData, bool isCheckedOut);
    }

    // Concrete Strategy for Book
    public class BookCreationStrategy : ILibraryItemCreationStrategy
    {
        public LibraryItem CreateLibraryItem(string title, Authors authors, string[] additionalData, bool isCheckedOut)
        {
            int pageCount = int.Parse(additionalData[0]);
            var book = new Book(title, authors, pageCount);
            if (isCheckedOut) book.CheckOut();
            return book;
        }
    }

    // Concrete Strategy for DVD
    public class DVDCreationStrategy : ILibraryItemCreationStrategy
    {
        public LibraryItem CreateLibraryItem(string title, Authors authors, string[] additionalData, bool isCheckedOut)
        {
            double duration = double.Parse(additionalData[0]);
            var dvd = new DVD(title, authors, duration);
            if (isCheckedOut) dvd.CheckOut();
            return dvd;
        }
    }

    // Concrete Strategy for Magazine
    public class MagazineCreationStrategy : ILibraryItemCreationStrategy
    {
        public LibraryItem? CreateLibraryItem(string title, Authors authors, string[] additionalData, bool isCheckedOut)
        {
            //int issueNumber = int.Parse(additionalData[0]);

            DateTime publicationDate;
            // 1
            bool isValidDate = DateTime.TryParseExact(additionalData[0], "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture,
                                           System.Globalization.DateTimeStyles.None,
                                           out publicationDate);

            if (isValidDate)
            {
                var magazine = new Magazine(title, authors, publicationDate); //issueNumber, 
                if (isCheckedOut) magazine.CheckOut();
                return magazine;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                return null;
            }

        }
    }

    // Context - LibraryItemFactory now uses the strategy pattern
    public class LibraryItemFactory : ILibraryItemFactory
    {
        private readonly Dictionary<string, ILibraryItemCreationStrategy> _creationStrategies;

        public LibraryItemFactory()
        {
            _creationStrategies = new Dictionary<string, ILibraryItemCreationStrategy>
            {
                { "Book", new BookCreationStrategy() },
                { "DVD", new DVDCreationStrategy() },
                { "Magazine", new MagazineCreationStrategy() }
            };
        }

        public List<string> GetTypes()
        {
            return _creationStrategies.Keys.ToList<string>();
        }

        public Dictionary<string, ILibraryItemCreationStrategy> GetStrategies()
        {
            return _creationStrategies;
        }

        public LibraryItem CreateLibraryItem(string itemType, string title, Authors authors, string[] additionalData, bool isCheckedOut)
        {
            if (!_creationStrategies.ContainsKey(itemType))
            {
                throw new ArgumentException("Unknown item type.");
            }

            var strategy = _creationStrategies[itemType];
            return strategy.CreateLibraryItem(title, authors, additionalData, isCheckedOut);
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
        public virtual void CheckOut()
        {
            if (IsCheckedOut)
                throw new InvalidOperationException("Item is already checked out.");
            IsCheckedOut = true;
        }
        public virtual void Return()
        {
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
