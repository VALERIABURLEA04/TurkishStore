using eUseControl.Domain.Entities.BlogEntities;
using eUseControl.Domain.Repositories;
using eUSeControl.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace eUSeControl.DataAccess.Repositories
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

        public async Task<List<BlogPost>> GetBlogPostsAsync()
        {
            var blogposts = await _context.BlogPosts
                .OrderByDescending(bp => bp.PublishDate)
                .ToListAsync();

            if (blogposts == null || !blogposts.Any())
                return new List<BlogPost>();

            return blogposts;
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