using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.Post
{
    public class PostFileCreateDto
    {
        public string Title { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public int PostId { get; set; }
        public long Length { get; set; }
    }
}
