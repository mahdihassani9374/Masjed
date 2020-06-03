using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.Entities
{
    public class SlideShow
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public string Link { get; set; }
    }
}
