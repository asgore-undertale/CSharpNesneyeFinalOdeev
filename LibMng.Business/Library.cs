using System;
using System.Collections.Generic;
using System.Linq;

namespace LibMng_Business
{
    public class Library
    {
        private List<LibraryItem> _items = new List<LibraryItem>();
        private readonly string _filePath;

        private readonly ILibraryItemFactory _itemFactory;
        private readonly FileRepository _fileRepository;

        public Library(
            string filePath,
            ILibraryItemFactory itemFactory,
            FileRepository fileRepository)
        {
            _filePath = filePath;
            _itemFactory = itemFactory;
            _fileRepository = fileRepository;
            LoadItems();
        }

        public void AddItem(LibraryItem item)
        {
            _items.Add(item);
            SaveItems();
        }

        public void DeleteItem(Guid id)
        {
            var item = FindItemById(id);
            _items.Remove(item);
            SaveItems();
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

        public void LoadItems()
        {
            _items = _fileRepository.LoadItems(_filePath, _itemFactory);
        }

        public void SaveItems()
        {
            _fileRepository.SaveItems(_filePath, _items);
        }
    }
}
