using eUseControl.Domain.Repositories;
using eUSeControl.BusinessLogic.Dtos.BlogDtos;
using eUSeControl.BusinessLogic.Interfaces;
using eUSeControl.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace businessLogic.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        private static BlogPostService _instance;
        private static readonly object _lock = new object();

        public BlogPostService()
        {
            _blogPostRepository = new BlogPostRepository();
        }

        public async Task<List<BlogPostDto>> GetBlogPostsAsync()
        {
            var blogPosts = await _blogPostRepository.GetBlogPostsAsync();

            var result = blogPosts
                .Select(bp => new BlogPostDto
                {
                    Id = bp.Id,
                    Title = bp.Title,
                    Content = bp.Content.Length <= 100 ? bp.Content : bp.Content.Substring(0, 100) + "...",
                    Author = bp.Author,
                    Day = bp.PublishDate.Day.ToString(),
                    MonthYear = bp.PublishDate.ToString("MMM yyyy"),
                    ImageUrl = bp.ImageUrl,
                    Categories = bp.Categories,
                    CommentsCount = bp.CommentsCount
                })
                .ToList();

            return result;
        }

        public static BlogPostService GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new BlogPostService();
                }
            }

            return _instance;
        }
    }
}