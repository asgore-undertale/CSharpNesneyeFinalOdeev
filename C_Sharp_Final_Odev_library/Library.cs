using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Final_Odev_library
{
    public class Library
    {
        private List<LibraryItem> _items = new List<LibraryItem>();
        private readonly string _filePath;

        private readonly ILibraryItemFactory _itemFactory;
        private readonly ICsvHandler _csvHandler;

        public Library(string filePath, ILibraryItemFactory itemFactory, ICsvHandler csvHandler)
        {
            _filePath = filePath;
            _itemFactory = itemFactory;
            _csvHandler = csvHandler;
            LoadFromCsv();
        }


        public void AddItem(LibraryItem item)
        {
            _items.Add(item);
            SaveToCsv();
        }

        public void DeleteItem(Guid id)
        {
            var item = FindItemById(id);
            _items.Remove(item);
            SaveToCsv();
        }

        public void ModifyItem(Guid id, LibraryItem newItem)
        {
            DeleteItem(id);
            AddItem(newItem);
        }

        public string ListItems()
        {
            var details = _items.Select(item => item.DisplayDetails()).ToList();
            return string.Join("\n\n", details);
        }

        public LibraryItem FindItemById(Guid id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                throw new ArgumentException("Item not found.");
            return item;
        }

        public void LoadFromCsv()
        {
            _items = _csvHandler.LoadItems(_filePath, _itemFactory);
        }

        public void SaveToCsv()
        {
            _csvHandler.SaveItems(_filePath, _items);
        }
    }
}
