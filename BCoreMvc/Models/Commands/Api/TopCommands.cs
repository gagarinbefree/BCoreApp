using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BCoreMvc.Models.ViewModels.Blog;

namespace BCoreMvc.Models.Commands.Api
{
    public class TopCommands : ITopCommands
    {
        public Task<TopViewModel> GetTopPostsAsync(ClaimsPrincipal user, int? pager = default(int?))
        {
            throw new NotImplementedException();
        }
    }
}
