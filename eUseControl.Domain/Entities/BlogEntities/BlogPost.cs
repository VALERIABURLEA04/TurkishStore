using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.BlogEntities
{
    [Table("BlogPosts")]
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required, StringLength(100)]
        public string Author { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [NotMapped]
        public int Day => PublishDate.Day;

        [NotMapped]
        public string MonthYear => PublishDate.ToString("MMM yyyy");

        [StringLength(200)]
        public string Categories { get; set; } // for tags

        public int CommentsCount { get; set; }

        public virtual ICollection<BlogComment> Comments { get; set; }
            = new HashSet<BlogComment>();
    }
}