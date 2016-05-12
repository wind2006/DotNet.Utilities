using System;

namespace YanZhiwei.BookShop.Domain.Entities
{
    public class Book
    {
        /*
        在MVC应用程序中，一切都是围绕Domain Model（领域模型）来的。
        */
        public string Author { get; set; }
        public int ID { get; set; }
        public string Isbn { get; set; }
        public decimal Price { get; set; }
        public DateTime Published { get; set; }
        public string Summary { get; set; }
        public byte[] Thumbnail { get; set; }
        public string Title { get; set; }
    }
}