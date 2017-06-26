using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BCore.Models.Commands;
using BCoreMvc.Models.ViewModels.Blog;
using BCoreDao;
using Microsoft.Extensions.Configuration;

namespace BCoreMvc.Models.Commands.Api
{
    public class UpdateCommands : Commands, IUpdateCommands
    {
        public UpdateCommands(IConfiguration configuration, IMapper mapper) 
            : base(configuration, mapper)
        {

        }

        public void AddPartToPost(UpdateViewModel model)
        {
            if (model.PreviewPost.Parts.Count() == 0)
                model.PreviewPost.CreatedOn = DateTime.Now;

            model.PreviewPost.Parts.Add(Mapper.Map<PartViewModel>(model.WhatsNew));

            model.WhatsNew.Clear();
        }

        public async Task<int> DeletePostAsync(Guid id, ClaimsPrincipal user)
        {
            var userId = GetUserId(user);
            if (userId == null)
                return -1;

            Post post = await Get<Post>($"Posts/{id}");
            if (post.UserId != userId)
                return -1;

            return await Delete($"Posts/{id}");
        }

        public async Task<UpdateViewModel> GetPostsByUserAsync(ClaimsPrincipal user, int? page = default(int?))
        {
            string userId = GetUserId(user);

            List<Post> posts = await Get<List<Post>>($"Posts?userid={userId}&page={page}");

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

            var model = Mapper.Map<UpdateViewModel>(posts);
            foreach (PostViewModel recent in model.RecentPosts)
            {
                recent.StatusLine = new PostStatusLineViewModel();
                recent.Editable(GetUserId(user));
                recent.IsPreview = false;
            }

            return model;
        }

        public async Task<Guid> SubmitPostAsync(UpdateViewModel model, ClaimsPrincipal user)
        {
            if (model.PreviewPost.Parts.Count() == 0)
                return Guid.Empty;

            var post = Mapper.Map<Post>(model);
            post.UserId = GetUserId(user);

            Post freshPost = await Post<Post>($"Posts/", post);
            Guid postId = freshPost.Id;
                             
            IEnumerable<PartViewModel> textParts = model
                .PreviewPost
                .Parts
                .Where(f => f.PartType == 0);

            if (textParts.Count() == 0)
                return postId;

            string text = textParts.Select(f => String.Format("{0} ", f.Value)).Aggregate((a, b) => a + b);
            if (String.IsNullOrWhiteSpace(text))
                return postId;

            List<string> tags = HashTag.GetHashTags(text);
            foreach (var tag in tags)
            {
                var existTag = await Get<Hash>($"Hashes/{tag}");
                Guid hashId;
                if (existTag == null)
                {
                    Hash freshHash = await Post<Hash>($"Posts/{postId}/Hashes", new Hash { Tag = tag });
                    hashId = freshHash.Id;
                }
                else
                    hashId = existTag.Id;

                PostHash postHash = new PostHash
                {
                    PostId = postId,
                    HashId = hashId
                };

                await Post<PostHash>("PostHashes", postHash);
            }

            return postId;
        }
    }
}