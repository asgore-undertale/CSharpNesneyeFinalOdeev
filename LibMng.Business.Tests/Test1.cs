using System;
using System.Collections.Generic;
using Xunit;
using LibMng_Business;
using Moq;

namespace LibMng_Business_Tests
{
    public class LibraryTests : IDisposable
    {
        private readonly Mock<FileRepository> _mockFileRepository;
        private readonly Mock<ILibraryItemFactory> _mockFactory;
        private readonly Library _library;

        public LibraryTests()
        {
            _mockFileRepository = new Mock<FileRepository>();
            _mockFactory = new Mock<ILibraryItemFactory>();

            _library = new Library("mockPath", _mockFactory.Object, _mockFileRepository.Object);
        }

        public void Dispose()
        {
            _mockFileRepository?.VerifyAll();
        }

        [Fact]
        public void AddAndListItems_SavesAndLoadsCorrectly()
        {
            var book = new Book("C# Programming", new Authors(new List<Author> { new Author("John", "Doe") }), 510);
            var dvd = new DVD("Inception", new Authors(new List<Author> { new Author("Christopher", "Nolan") }), 148);

            _mockFileRepository.Setup(r => r.LoadItems(It.IsAny<string>(), _mockFactory.Object))
                .Returns(new List<LibraryItem>());
            _mockFileRepository.Setup(r => r.SaveItems(It.IsAny<string>(), It.IsAny<List<LibraryItem>>()));

            _library.AddItem(book);
            _library.AddItem(dvd);
            var list = _library.ListItems();

            Assert.Contains("C# Programming", list);
            Assert.Contains("Inception", list);

            _mockFileRepository.Verify(r => r.SaveItems(It.IsAny<string>(), It.IsAny<List<LibraryItem>>()), Times.Exactly(2));
        }

        [Fact]
        public void DeleteItem_RemovesItemFromLibrary()
        {
            var book = new Book("C# Programming", new Authors(new List<Author> { new Author("John", "Doe") }), 510);
            _mockFileRepository.Setup(r => r.LoadItems(It.IsAny<string>(), _mockFactory.Object))
                .Returns(new List<LibraryItem> { book });
            _mockFileRepository.Setup(r => r.SaveItems(It.IsAny<string>(), It.IsAny<List<LibraryItem>>()));

            _library.DeleteItem(book.Id);
            var list = _library.ListItems();

            Assert.DoesNotContain("C# Programming", list);

            _mockFileRepository.Verify(r => r.SaveItems(It.IsAny<string>(), It.IsAny<List<LibraryItem>>()), Times.Once);
        }

        [Fact]
        public void ModifyItem_UpdatesItemCorrectly()
        {
            var book = new Book("C# Programming", new Authors(new List<Author> { new Author("John", "Doe") }), 500);
            var newBook = new Book("C# Programming", new Authors(new List<Author> { new Author("Jane", "Smith") }), 610);

            _mockFileRepository.Setup(r => r.LoadItems(It.IsAny<string>(), _mockFactory.Object))
                .Returns(new List<LibraryItem> { book });
            _mockFileRepository.Setup(r => r.SaveItems(It.IsAny<string>(), It.IsAny<List<LibraryItem>>()));

            _library.ModifyItem(book.Id, newBook);
            var list = _library.ListItems();

            Assert.Contains("Jane Smith", list);
            Assert.Contains("Pages: 600", list);

            _mockFileRepository.Verify(r => r.SaveItems(It.IsAny<string>(), It.IsAny<List<LibraryItem>>()), Times.Exactly(2));
        }
    }
}
