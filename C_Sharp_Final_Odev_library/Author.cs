using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace C_Sharp_Final_Odev_library
{
    public class Author
    {
        public Guid Id { get; private set; }

        private string _firstName; // Backing field for FirstName
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new ArgumentException("Last name must only contain letters.");
                _firstName = value; // Store the valid value
            }
        }

        private string _lastName; // Backing field for LastName
        public string LastName
        {
            get => _lastName;
            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new ArgumentException("Last name must only contain letters.");
                _lastName = value; // Store the valid value
            }
        }

        public Author(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{_firstName} {_lastName}";
        }

        public string ToCsv()
        {
            return $"{_firstName}_{_lastName}";
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

        public override string ToString()
        {
            return string.Join("-", AuthorsList.Select(a => a.ToString()));
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
