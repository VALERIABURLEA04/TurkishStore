using businessLogic.Dtos.BlogDtos;
using businessLogic.Interfaces.Repositories;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.BlogEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace businessLogic.BLStruct
{
    public class BlogPostRepository : IBlogPostRepository, IDisposable
    {
        private readonly EUseControlDbContext _context;
        private bool _disposed;

        public BlogPostRepository()
        {
            _context = new EUseControlDbContext();
        }

        public async Task<int> CreateAsync(BlogPost post)
        {
            if (post == null)
                return 0;

            _context.BlogPosts.Add(post);
            await _context.SaveChangesAsync();

            return post.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.BlogPosts.FindAsync(id);

            if (entity == null)
                return false;

            _context.BlogPosts.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<BlogPostDto>> GetBlogPostsAsync()
        {
            var blogposts = await _context.BlogPosts
                .OrderByDescending(bp => bp.PublishDate)
                .ToListAsync();

            var result = blogposts
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

            if (result == null || !result.Any())
                return new List<BlogPostDto>();

            return result;
        }

        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(BlogPost post)
        {
            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}