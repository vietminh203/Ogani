using System;

namespace Ogani.Data
{
    public class Page
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalView { set; get; }
    }
}