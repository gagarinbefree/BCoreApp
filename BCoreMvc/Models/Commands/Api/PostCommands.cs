using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BCoreDao;
using BCoreMvc.Models.ViewModels.Blog;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace BCoreMvc.Models.Commands.Api
{
    public class PostCommands : Commands, IPostCommands
    {
        public PostCommands(IConfiguration configuration, IMapper mapper) 
            : base(configuration, mapper)
        {

        }
        
        public async Task<PostViewModel> GetPostById(Guid id, ClaimsPrincipal user)
        {
            Post post = await Get<Post>($"Posts/{id}");

            List<Part> parts = await Get<List<Part>>(String.Format($"Posts/{id}/Parts"));
            post.Parts = parts.OrderBy(f => f.CreatedOn).ToList();

            List<Comment> comments = await Get<List<Comment>>($"Posts/{id}/Comments");
            post.Comments = comments.OrderByDescending(f => f.CreatedOn).ToList();

            var model = Mapper.Map<PostViewModel>(post);                        
            model.StatusLine = new PostStatusLineViewModel();
            model.Editable(GetUserId(user));            
            model.IsPreview = false;

            return model;
        }

        public async Task<int> DeleteCommentAsync(Guid postId, Guid commentId, ClaimsPrincipal user)
        {
            var userId = GetUserId(user);
            if (userId == null)
                return -1;

            Comment comment = await Get<Comment>($"Posts/{postId}/Comments/{commentId}");            
            if (comment.UserId != userId)
                return -1;

            return await Delete($"Posts/{postId}/Comments/{commentId}");
        }

        public async Task<Hash> GetHashById(Guid postId, Guid hashId)
        {
            return await Get<Hash>($"Posts/{postId}/Hashes/{hashId}");
        }        

        public async Task<Comment> SubmitCommentsAsync(PostViewModel model, ClaimsPrincipal user)
        {
            Comment comment = Mapper.Map<Comment>(model.Comment);
            comment.UserId = GetUserId(user);
            comment.PostId = model.Id;

            return await Post<Comment>($"Posts/{comment.PostId}/Comments/{comment.Id}", comment);
        }    
    }
}