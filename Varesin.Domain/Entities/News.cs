using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrimaryPicture { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public NewsType Type { get; set; }
        public virtual ICollection<NewsFile> Files { get; set; }
    }
}
