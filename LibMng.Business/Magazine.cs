using LibMng_Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMng.Business
{
    public class Magazine : LibraryItem
    {
        public int IssueNumber { get; private set; }
        public DateTime PublicationDate { get; private set; }

        public Magazine(string title, Authors authors, DateTime publicationDate) // , int issueNumber
            : base(title, authors, "Magazine")
        {
            // IssueNumber = issueNumber;
            PublicationDate = publicationDate;
        }

        public override string DisplayDetails()
        {
            return $"[{ItemType}] ID: {Id}\nTitle: {Title}\nAuthors: {Authors.ToString()}\nPublication Date: {PublicationDate.ToShortDateString()}\nChecked Out: {IsCheckedOut}"; // \nIssue Number: {IssueNumber}
        }

        public override string ToCsv()
        {
            return $"{Id},{ItemType},{Title},{Authors.ToCsv()},{PublicationDate.Year}-{PublicationDate.Month}-{PublicationDate.Day},{IsCheckedOut}"; // ,{IssueNumber}
        }
    }
}
