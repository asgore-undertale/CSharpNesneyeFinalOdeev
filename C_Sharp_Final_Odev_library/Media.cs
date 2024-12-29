using System;
using System.Collections.Generic;

namespace C_Sharp_Final_Odev_library
{
    public abstract class Media : LibraryItem
    {
        public MediaType MediaType { get; set; }

        protected Media(string title, Authors authors, string itemType, MediaType mediaType)
            : base(title, authors, itemType)
        {
            MediaType = mediaType;
        }
    }
}
