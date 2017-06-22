using BCoreDao;
using BCoreMvc.Models.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BCoreMvc.Models.Commands
{
    public interface IPostCommands
    {
        Task<List<Post>> GetAll();
        Task<PostViewModel> GetPostById(Guid id,ClaimsPrincipal user);        
        Task<Hash> GetHashById(Guid id);
        Task<int> DeleteCommentAsync(Guid id, ClaimsPrincipal user);
        Task<Guid> SubmitCommentsAsync(PostViewModel model, ClaimsPrincipal user);
    }
}
