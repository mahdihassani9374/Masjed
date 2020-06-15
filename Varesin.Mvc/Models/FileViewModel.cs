using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models
{
    public class FileViewModel
    {
        public string Title { get; set; }
        public FileType Type { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string RelativePath { get; set; }
    }
}
