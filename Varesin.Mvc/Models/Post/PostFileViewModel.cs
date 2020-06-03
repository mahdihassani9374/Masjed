using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.Post
{
    public class PostFileViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public FileType Type { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public int CountDownload { get; set; }
    }
}
