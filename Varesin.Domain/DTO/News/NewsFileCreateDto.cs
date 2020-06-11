using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.News
{
    public class NewsFileCreateDto
    {
        public string Title { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public int NewsId { get; set; }
        public long Length { get; set; }
    }
}
