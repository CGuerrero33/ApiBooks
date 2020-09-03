using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiBooks.Models.Book
{
    public class Book
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int PageCount { get; set; }

        public string Excerpt { get; set; }

        public DateTime PublishDate { get; set; }


        public Book()
        {

        }

        public Book(int iD, string title, string description, int pageCount, string excerpt, DateTime publishDate)
        {
            ID = iD;
            Title = title;
            Description = description;
            PageCount = pageCount;
            Excerpt = excerpt;
            PublishDate = publishDate;
        }
    }
}