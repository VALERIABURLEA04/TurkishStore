using eUSeControl.BusinessLogic.Dtos.BlogDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eUSeControl.BusinessLogic.Interfaces
{
    public interface IBlogPostService
    {
        Task<List<BlogPostDto>> GetBlogPostsAsync();
    }
}