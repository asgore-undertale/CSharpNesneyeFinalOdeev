using LibMng_Business;
using System;
using System.Linq;

namespace LibMng_Client_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Library System CLI!");
            Console.WriteLine("With an awesome data persistence feature!!! \\(^o^)/ ");

            string filePath = "library.csv";
            LibraryItemFactory factory = new LibraryItemFactory();
            Dictionary<string, ILibraryItemCreationStrategy> strategies = factory.GetStrategies();
            FileRepository csvHandler = new FileRepository();
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
                            AddItem(library, strategies);
                            break;
                        case "list":
                            ListItems(library);
                            break;
                        case "modify":
                            ModifyItem(library, strategies);
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

        static LibraryItem? CreateItemFromUserInputs(Dictionary<string, ILibraryItemCreationStrategy> strategies)
        {

            Console.Write($"Enter item type ({string.Join("/", strategies.Keys.ToList())}): ");
            string itemType = Console.ReadLine();

            if (!strategies.ContainsKey(itemType))
            {
                Console.WriteLine("Invalid item type.");
                return null;
            }

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

            string[] additionalData = new string[2];
            if (itemType.Equals("Book", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter page count: ");
                additionalData[0] = Console.ReadLine();
            }
            else if (itemType.Equals("DVD", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter duration (in minutes): ");
                additionalData[0] = Console.ReadLine();
            }
            else if (itemType.Equals("Magazine", StringComparison.OrdinalIgnoreCase))
            {
                /*
                Console.Write("Enter issue number: ");
                additionalData[0] = Console.ReadLine();
                */
                Console.Write("Enter publication date (yyyy-MM-dd): ");
                additionalData[0] = Console.ReadLine(); // 1
            }

            var strategy = strategies[itemType];
            return strategy.CreateLibraryItem(title, authors, additionalData, false);
        }

        static void AddItem(Library library, Dictionary<string, ILibraryItemCreationStrategy> strategies)
        {
            var item = CreateItemFromUserInputs(strategies);
            if (item != null)
            {
                library.AddItem(item);
                Console.Write("Item has been added successfully!!\n");
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

        static void ModifyItem(Library library, Dictionary<string, ILibraryItemCreationStrategy> strategies)
        {
            Console.Write("Enter the ID of the item to modify: ");
            Guid id = Guid.Parse(Console.ReadLine());


            var item = CreateItemFromUserInputs(strategies);
            if (item != null)
            {
                library.ModifyItem(id, item);
                Console.Write("Item has been added successfully!!\n");
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
            library.SaveItems();
            Console.WriteLine("Item checked out successfully.");
        }

        static void ReturnItem(Library library)
        {
            Console.Write("Enter the ID of the item to return: ");
            Guid id = Guid.Parse(Console.ReadLine());
            var item = library.FindItemById(id);
            item.Return();
            library.SaveItems();
            Console.WriteLine("Item returned successfully.");
        }
    }
}