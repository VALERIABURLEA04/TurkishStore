using eUseControl.Domain.Entities.BlogEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eUseControl.Domain.Repositories
{
    public interface IBlogPostRepository
    {
        Task<List<BlogPost>> GetBlogPostsAsync();

        Task<BlogPost> GetBlogPostByIdAsync(int id);

        Task<int> CreateAsync(BlogPost item);

        Task<bool> UpdateAsync(BlogPost item);

        Task<bool> DeleteAsync(int id);
    }
}