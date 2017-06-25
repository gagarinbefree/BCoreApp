using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BCoreMvc.Models.ViewModels.Blog;
using Microsoft.Extensions.Configuration;
using BCoreDao;

namespace BCoreMvc.Models.Commands.Api
{
    public class FeedCommands : Commands, IFeedCommands
    {
        public FeedCommands(IConfiguration configuration, IMapper mapper)
            : base(configuration, mapper)
        {

        }

        public async Task<FeedViewModel> GetLastPostsAsync(ClaimsPrincipal user, int? page = null)
        {
            List<Post> posts = await Get<List<Post>>($"Posts", page);

            return null; //????????
        }

        public Task<FeedViewModel> SearchPostsByTagAsync(string tag, ClaimsPrincipal user, int? page = default(int?))
        {
            throw new NotImplementedException();
        }

    }
}
