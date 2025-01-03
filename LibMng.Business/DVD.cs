﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMng_Business
{
    public class DVD : LibraryItem
    {
        private double _duration;

        public DVD(string title, Authors authors, double duration)
            : base(title, authors, "DVD")
        {
            if (duration <= 0)
                throw new ArgumentException("Duration must be greater than zero.");
            _duration = duration;
        }

        public override string DisplayDetails()
        {
            return $"[{ItemType}] ID: {Id}\nTitle: {Title}\nAuthor: {Authors.ToString()}\nDuration: {_duration} minutes\nChecked Out: {IsCheckedOut}";
        }

        public override string ToCsv()
        {
           return $"{Id},{ItemType},{Title},{Authors.ToCsv()},{_duration},{IsCheckedOut}";
        } 
    }
}
