using C_Sharp_Final_Odev_library;
using System;

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

            Console.WriteLine("\nType 'help' to see the available commands.");

            while (true)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid command. Type 'help' for a list of commands.");
                    continue;
                }

                try
                {
                    switch (input.ToLower())
                    {
                        case "help":
                            ShowHelp();
                            break;
                        case "add":
                            AddItem(library);
                            break;
                        case "list":
                            ListItems(library);
                            break;
                        case "modify":
                            ModifyItem(library);
                            break;
                        case "delete":
                            DeleteItem(library);
                            break;
                        case "checkout":
                            CheckOutItem(library);
                            break;
                        case "return":
                            ReturnItem(library);
                            break;
                        case "exit":
                            Console.WriteLine("Exiting... Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine("  help      - Show this help menu");
            Console.WriteLine("  add       - Add a new item to the library");
            Console.WriteLine("  list      - List all items in the library");
            Console.WriteLine("  modify    - Modify an existing library item");
            Console.WriteLine("  delete    - Delete an item from the library");
            Console.WriteLine("  checkout  - Check out an item");
            Console.WriteLine("  return    - Return a checked-out item");
            Console.WriteLine("  exit      - Exit the program");
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
