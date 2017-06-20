using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BCore.Models.Commands;
using BCoreMvc.Models.ViewModels.Blog;

namespace BCoreMvc.Models.Commands.Api
{
    public class UpdateCommands : IUpdateCommands
    {
        public void AddPartToPost(UpdateViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePostAsync(Guid id, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateViewModel> GetPostsByUserAsync(ClaimsPrincipal user, int? page = default(int?))
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SubmitPostAsync(UpdateViewModel model, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}