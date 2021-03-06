﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrimaryPicture { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<PostFile> Files { get; set; }
    }
}
