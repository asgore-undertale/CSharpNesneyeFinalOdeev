using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace C_Sharp_Final_Odev_library
{
    public interface ICsvHandler
    {
        List<LibraryItem> LoadItems(string filePath, ILibraryItemFactory factory);
        void SaveItems(string filePath, IEnumerable<LibraryItem> items);
    }

    public class CsvHandler : ICsvHandler
    {
        public List<LibraryItem> LoadItems(string filePath, ILibraryItemFactory factory)
        {
            var items = new List<LibraryItem>();
            if (!File.Exists(filePath)) return items;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(',');
                if (parts.Length < 6) continue;

                var id = Guid.Parse(parts[0]);
                var itemType = parts[1];
                var title = parts[2];

                var authors = Authors.FromCsv(parts[3]);

                var additionalData = new[] { parts[4] };
                var isCheckedOut = bool.Parse(parts[5]);

                var item = factory.CreateLibraryItem(itemType, title, authors, additionalData, isCheckedOut);
                item.GetType().GetProperty("Id").SetValue(item, id);
                items.Add(item);
            }

            return items;
        }

        public void SaveItems(string filePath, IEnumerable<LibraryItem> items)
        {
            var lines = items.Select(item => item.ToCsv());
            File.WriteAllLines(filePath, lines);
        }
    }
}
