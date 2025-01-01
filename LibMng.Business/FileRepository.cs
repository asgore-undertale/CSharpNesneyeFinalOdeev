using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibMng_Business
{
    public interface IRepository
    {
    }

    public interface IReadableRepository : IRepository
    {
        List<LibraryItem> LoadItems(string source, ILibraryItemFactory factory);
    }

    public interface IWritableRepository : IRepository
    {
        void SaveItems(string destination, IEnumerable<LibraryItem> items);
    }

    public class FileRepository : IReadableRepository, IWritableRepository
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
