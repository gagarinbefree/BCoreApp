using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BCoreMvc.Models.ViewModels.Blog;

namespace BCoreMvc.Models.Commands.Api
{
    public class FeedCommands : IFeedCommands
    {
        public Task<FeedViewModel> GetLastPostsAsync(ClaimsPrincipal user, int? page = default(int?))
        {
            throw new NotImplementedException();
        }

        public Task<FeedViewModel> SearchPostsByTagAsync(string tag, ClaimsPrincipal user, int? page = default(int?))
        {
            throw new NotImplementedException();
        }
    }
}
