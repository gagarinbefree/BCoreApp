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
        Task<PostViewModel> GetPostById(Guid id,ClaimsPrincipal user);
        Task<Hash> GetHashById(Guid postId, Guid hashId);
        Task<int> DeleteCommentAsync(Guid postId, Guid commentId, ClaimsPrincipal user);
        Task<Comment> SubmitCommentsAsync(PostViewModel model, ClaimsPrincipal user);
    }
}
