namespace businessLogic.Dtos.BlogDtos
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Day { get; set; }
        public string MonthYear { get; set; }
        public string ImageUrl { get; set; }
        public string Categories { get; set; }
        public int CommentsCount { get; set; }
    }
}