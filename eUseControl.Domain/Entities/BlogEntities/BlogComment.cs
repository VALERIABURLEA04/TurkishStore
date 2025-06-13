using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.BlogEntities
{
    [Table("BlogComments")]
    public class BlogComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BlogPostId { get; set; }

        [Required, StringLength(100)]
        public string AuthorName { get; set; }

        [Required]
        public string CommentText { get; set; }

        public DateTime PostedDate { get; set; }

        [ForeignKey("BlogPostId")]
        public virtual BlogPost BlogPost { get; set; }
    }
}