using BCoreMvc.Models.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BCoreMvc.Models.Commands
{
    public interface IFeedCommands
    {
        Task<FeedViewModel> GetLastPostsAsync(ClaimsPrincipal user, int? page = null);
        Task<FeedViewModel> SearchPostsByTagAsync(string tag, ClaimsPrincipal user, int? page = null);
    }
}
