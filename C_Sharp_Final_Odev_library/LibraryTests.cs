using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace C_Sharp_Final_Odev_library
{
    public class LibraryTests
    {
        private const string TestFilePath = "test_library.csv";

        private void CleanUp()
        {
            if (File.Exists(TestFilePath))
                File.Delete(TestFilePath);
        }

        [Fact]
        public void AddAndListItems_SavesAndLoadsCorrectly()
        {
            // Arrange
            CleanUp();
            LibraryItemFactory factory = new LibraryItemFactory();
            ICsvHandler csvHandler = new CsvHandler();
            Library library = new Library(TestFilePath, factory, csvHandler);
            var book = new Book("C# Programming", new Authors( new List<Author> { new Author("John", "Doe") }), 500);
            var dvd = new DVD("Inception", new Authors(new List<Author> { new Author("Christopher", "Nolan") }), 148);


            // Act
            library.AddItem(book);
            library.AddItem(dvd);
            var list = library.ListItems();

            // Assert
            Assert.Contains("C# Programming", list);
            Assert.Contains("Inception", list);

            // Cleanup
            CleanUp();
        }

        [Fact]
        public void DeleteItem_RemovesItemFromLibrary()
        {
            // Arrange
            CleanUp();
            LibraryItemFactory factory = new LibraryItemFactory();
            ICsvHandler csvHandler = new CsvHandler();
            Library library = new Library(TestFilePath, factory, csvHandler);
            var book = new Book("C# Programming", new Authors(new List<Author> { new Author("John", "Doe") }), 500);
            library.AddItem(book);

            // Act
            library.DeleteItem(book.Id);
            var list = library.ListItems();

            // Assert
            Assert.DoesNotContain("C# Programming", list);

            // Cleanup
            CleanUp();
        }

        [Fact]
        public void ModifyItem_UpdatesItemCorrectly()
        {
            // Arrange
            CleanUp();
            LibraryItemFactory factory = new LibraryItemFactory();
            ICsvHandler csvHandler = new CsvHandler();
            Library library = new Library(TestFilePath, factory, csvHandler);
            var book = new Book("C# Programming", new Authors(new List<Author> { new Author("John", "Doe") }), 500);
            library.AddItem(book);

            // Act
            var newBook = new Book("C# Programming", new Authors(new List<Author> { new Author("Jane", "Smith") }), 600);
            library.ModifyItem(newBook.Id, newBook);
            var list = library.ListItems();

            // Assert
            Assert.Contains("Jane Smith", list);
            Assert.Contains("Pages: 600", list);

            // Cleanup
            CleanUp();
        }
    }
}
