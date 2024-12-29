using System;
using System.Collections.Generic;
using System.Linq;

namespace C_Sharp_Final_Odev_library
{
    public class Author
    {
        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Author(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public string ToCsv()
        {
            return $"{FirstName}_{LastName}";
        }

        public static Author FromCsv(string csvLine)
        {
            var parts = csvLine.Split('_');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid CSV format for Author");

            var author = new Author(parts[0], parts[1]);
            return author;
        }
    }
    public class Authors
    {
        public List<Author> AuthorsList { get; set; } = new List<Author>();

        public Authors(List<Author> AuthorsList)
        {
            this.AuthorsList = AuthorsList;
        }

        public Authors()
        {
            this.AuthorsList = AuthorsList;
        }

        public string ToCsv()
        {
            return string.Join("-", AuthorsList.Select(a => a.ToCsv()));
        }

        public static Authors FromCsv(string csvLine)
        {
            var authers = new Authors();

            foreach (var a in csvLine.Split(new char[] { '-' }).ToList())
            {
                authers.AuthorsList.Add(Author.FromCsv(a.Trim()));
            }

            return authers;
        }
    }
}
