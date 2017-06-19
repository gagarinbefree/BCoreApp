using BCoreMvc.Models.ViewModels.Blog;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BCoreMvc.Models.Commands
{
    public interface ITopCommands
    {
        Task<TopViewModel> GetTopPostsAsync(ClaimsPrincipal user, int? pager = null);
    }
}
