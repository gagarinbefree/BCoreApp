using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BCoreMvc.Models.ViewModels.Blog;
using BCoreDao;
using Microsoft.Extensions.Configuration;

namespace BCoreMvc.Models.Commands.Api
{
    public class TopCommands : Commands, ITopCommands
    {
        public TopCommands(IConfiguration configuration, IMapper mapper) 
            : base(configuration, mapper)
        {

        }

        public async Task<TopViewModel> GetTopPostsAsync(ClaimsPrincipal user, int? page = null)
        {
            List<Post> posts = await Get<List<Post>>($"Posts", page);

            TopViewModel model = await _createViewModel(posts.OrderByDescending(f => f.Comments.Count()).ThenByDescending(f => f.CreatedOn).ToList(), user, page);

            model.Pager = new PagerViewModel(posts.Count(), page == null ? 1 : (int)page);

            return model;
        }

        private async Task<TopViewModel> _createViewModel(ICollection<Post> posts, ClaimsPrincipal user, int? page = null)
        {
            foreach (Post post in posts)
            {
                List<Part> parts = await Get<List<Part>>($"Posts/{post.Id}/Parts");

                List<Part> imageParts = parts
                    .OrderBy(f => f.CreatedOn)
                    .Where(f => f.PartType == 1)
                    .Take(1)
                    .ToList();

                List<Part> txtParts = parts
                    .OrderBy(f => f.CreatedOn)
                    .Where(f => f.PartType == 0)
                    .Take(imageParts.Count != 0 ? 1 : 2)
                    .ToList();

                post.Parts = txtParts.Concat(imageParts).ToList();

                List<Hash> hashes = await Get<List<Hash>>($"Posts/{post.Id}/Hashes");
                post.Hashes = hashes;
            }

            var model = Mapper.Map<TopViewModel>(posts);
            int ii = page != null ? (int)page * PagerViewModel.ItemsOnPage - 1 : 1;
            foreach (PostViewModel recent in model.RecentPosts)
            {
                recent.StatusLine = new PostStatusLineViewModel();
                recent.Editable(GetUserId(user));
                recent.IsPreview = false;
                recent.TopNumber = ii;

                ii++;
            }
            
            return model;
        }
    }
}
