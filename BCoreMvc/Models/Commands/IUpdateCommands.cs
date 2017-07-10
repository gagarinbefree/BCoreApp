using BCoreMvc.Models.Commands;
using BCoreMvc.Models.ViewModels.Blog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BCoreMvc.Models.Commands
{
    public interface IUpdateCommands
    {
        Task<UpdateViewModel> GetPostsByUserAsync(ClaimsPrincipal user, int page = 1);
        void AddPartToPost(UpdateViewModel model);
        Task<Guid> SubmitPostAsync(UpdateViewModel model, ClaimsPrincipal user);
        Task<int> DeletePostAsync(Guid id, ClaimsPrincipal user);
    }
}
