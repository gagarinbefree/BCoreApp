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

            FeedViewModel model = await _createViewModel(posts, user);

            model.Pager = new PagerViewModel(posts.Count(), (int)page);

            return model;
        }

        public async Task<FeedViewModel> SearchPostsByTagAsync(string tag, ClaimsPrincipal user, int? page = default(int?))
        {
            Hash hase = await Get<Hash>($"Hashes/{tag}");
            if (hase == null)
                return new FeedViewModel();

            List<PostHash> tagPostHashes = await Get<List<PostHash>>($"PostHashes/{hase.Id}");
            if (tagPostHashes.Count() == 0)
                return new FeedViewModel();

            List<Post> posts = new List<Post>();
            foreach(PostHash postHash in tagPostHashes)
            {
                Post post = await Get<Post>($"Posts/{postHash.PostId}");
                posts.Add(post);
            }

            return await _createViewModel(posts, user);
        }

        private async Task<FeedViewModel> _createViewModel(ICollection<Post> posts, ClaimsPrincipal user)
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

            var model = Mapper.Map<FeedViewModel>(posts);
            foreach (PostViewModel recent in model.RecentPosts)
            {
                recent.StatusLine = new PostStatusLineViewModel();
                recent.Editable(GetUserId(user));
                recent.IsPreview = false;
            }

            return model;
        }

    }
}
