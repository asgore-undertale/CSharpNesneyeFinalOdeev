using C_Sharp_Final_Odev_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Final_Odev
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Library System CLI!");
            Console.WriteLine("With an awesome data persistence feature!!! \\(^o^)/ ");

            string filePath = "library.csv";
            LibraryItemFactory factory = new LibraryItemFactory();
            ICsvHandler csvHandler = new CsvHandler();
            Library library = new Library(filePath, factory, csvHandler);

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. List Items");
                Console.WriteLine("3. Modify Item");
                Console.WriteLine("4. Delete Item");
                Console.WriteLine("5. Check Out Item");
                Console.WriteLine("6. Return Item");
                Console.WriteLine("7. Exit");

                Console.Write("Select an option (1-7): ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddItem(library);
                            break;
                        case "2":
                            ListItems(library);
                            break;
                        case "3":
                            ModifyItem(library);
                            break;
                        case "4":
                            DeleteItem(library);
                            break;
                        case "5":
                            CheckOutItem(library);
                            break;
                        case "6":
                            ReturnItem(library);
                            break;
                        case "7":
                            Console.WriteLine("Exiting... Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void AddItem(Library library)
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter number of authors: ");
            int numAuthors = int.Parse(Console.ReadLine());

            Authors authors = new Authors();
            for (int i = 0; i < numAuthors; i++)
            {
                Console.Write($"Enter author {i + 1}'s first name: ");
                string firstName = Console.ReadLine();

                Console.Write($"Enter author {i + 1}'s last name: ");
                string lastName = Console.ReadLine();

                authors.AuthorsList.Add(new Author(firstName, lastName));
            }

            Console.Write("Enter item type (Book/DVD): ");
            string itemType = Console.ReadLine();

            if (itemType.Equals("Book", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter page count: ");
                int pageCount = int.Parse(Console.ReadLine());

                var book = new Book(title, authors, pageCount);
                library.AddItem(book);
                Console.WriteLine("Book added successfully.");
            }
            else if (itemType.Equals("DVD", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter duration (in minutes): ");
                double duration = double.Parse(Console.ReadLine());

                var dvd = new DVD(title, authors, duration);
                library.AddItem(dvd);
                Console.WriteLine("DVD added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid item type.");
            }
        }

        static void ListItems(Library library)
        {
            string items = library.ListItems();
            if (string.IsNullOrWhiteSpace(items))
            {
                Console.WriteLine("No items in the library.");
            }
            else
            {
                Console.WriteLine("\nLibrary Items:");
                Console.WriteLine(items);
            }
        }

        static void ModifyItem(Library library)
        {
            Console.Write("Enter the ID of the item to modify: ");
            Guid id = Guid.Parse(Console.ReadLine());

            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine();

            Console.Write("Enter number of authors: ");
            int numAuthors = int.Parse(Console.ReadLine());

            Authors authors = new Authors();
            for (int i = 0; i < numAuthors; i++)
            {
                Console.Write($"Enter author {i + 1}'s first name: ");
                string firstName = Console.ReadLine();

                Console.Write($"Enter author {i + 1}'s last name: ");
                string lastName = Console.ReadLine();

                authors.AuthorsList.Add(new Author(firstName, lastName));
            }

            Console.Write("Enter item type (Book/DVD): ");
            string itemType = Console.ReadLine();

            if (itemType.Equals("Book", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter new page count: ");
                int newPageCount = int.Parse(Console.ReadLine());

                var newBook = new Book(newTitle, authors, newPageCount);
                library.ModifyItem(id, newBook);
                Console.WriteLine("Book modified successfully.");
            }
            else if (itemType.Equals("DVD", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter new duration (in minutes): ");
                double newDuration = double.Parse(Console.ReadLine());

                var newDvd = new DVD(newTitle, authors, newDuration);
                library.ModifyItem(id, newDvd);
                Console.WriteLine("DVD modified successfully.");
            }
            else
            {
                Console.WriteLine("Invalid item type.");
            }
        }

        static void DeleteItem(Library library)
        {
            Console.Write("Enter the ID of the item to delete: ");
            Guid id = Guid.Parse(Console.ReadLine());
            library.DeleteItem(id);
            Console.WriteLine("Item deleted successfully.");
        }

        static void CheckOutItem(Library library)
        {
            Console.Write("Enter the ID of the item to check out: ");
            Guid id = Guid.Parse(Console.ReadLine());
            var item = library.FindItemById(id);
            item.CheckOut();
            library.SaveToCsv();
            Console.WriteLine("Item checked out successfully.");
        }

        static void ReturnItem(Library library)
        {
            Console.Write("Enter the ID of the item to return: ");
            Guid id = Guid.Parse(Console.ReadLine());
            var item = library.FindItemById(id);
            item.Return();
            library.SaveToCsv();
            Console.WriteLine("Item returned successfully.");
        }
    }
}
