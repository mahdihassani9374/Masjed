using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.DTO.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrimaryPicture { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
