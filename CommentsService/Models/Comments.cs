﻿using System.ComponentModel.DataAnnotations;

namespace CommentsService.Models
{
    public class Comments
    {
        [Key]
        public Guid CommentId { get; set; }

        public Guid PostId { get; set; }
        public string UserId { get; set; }
        public string Details { get; set; }
    }
}
