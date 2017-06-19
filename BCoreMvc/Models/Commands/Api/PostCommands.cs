using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BCoreDao;
using BCoreMvc.Models.ViewModels.Blog;

namespace BCoreMvc.Models.Commands.Api
{
    public class PostCommands : IPostCommands
    {
        public Task<int> DeleteCommentAsync(Guid id, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Task<Hash> GetHashById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PostViewModel> GetPostById(Guid id, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SubmitCommentsAsync(PostViewModel model, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}