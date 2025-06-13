using businessLogic.Dtos.BlogDtos;
using eUseControl.Domain.Entities.BlogEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace businessLogic.Interfaces.Repositories
{
    public interface IBlogPostRepository
    {
        Task<List<BlogPostDto>> GetBlogPostsAsync();

        Task<BlogPost> GetBlogPostByIdAsync(int id);

        Task<int> CreateAsync(BlogPost item);

        Task<bool> UpdateAsync(BlogPost item);

        Task<bool> DeleteAsync(int id);
    }
}