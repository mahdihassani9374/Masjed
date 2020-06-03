using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.DTO.Post
{
    public class PostCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PrimaryPicture { get; set; }
    }
}
