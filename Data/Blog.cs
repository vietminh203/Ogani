using System;

namespace Ogani.Data
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Content { set; get; }
        public string Image { set; get; }
        public DateTime CreateAt { get; set; }
        public AppUser AppUser { get; set; }
        public Guid UserId { get; set; }
    }
}