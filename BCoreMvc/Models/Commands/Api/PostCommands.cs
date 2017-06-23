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
        public PostCommands(IConfiguration configuration, IMapper mapper) : base(configuration, mapper)
        {

        }

        public async Task<List<Post>> GetAll()
        {
            return await Get<List<Post>>($"{ApiPath}/Posts");
        }

        public async Task<PostViewModel> GetPostById(Guid id, ClaimsPrincipal user)
        {
            Post post = await Get<Post>($"Posts/{id}");

            List<Part> parts = await Get<List<Part>>($"Posts/{id}/Parts");
            post.Parts = parts.OrderBy(f => f.CreatedOn).ToList();

            List<Comment> comments = await Get<List<Comment>>($"/Posts/{id}/Comments");
            post.Comments = comments.OrderByDescending(f => f.CreatedOn).ToList();

            List<Hash> hashes = await Get<List<Hash>>($"/Posts/{id}/Hashes");
            post.Hashes = hashes;

            var model = Mapper.Map<PostViewModel>(post);
            model.StatusLine = new PostStatusLineViewModel();
            model.IsPreview = false;

            //model.StatusLine.IsEditable = userId == model.UserId;


            return model;
        }

        public Task<int> DeleteCommentAsync(Guid id, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Task<Hash> GetHashById(Guid id)
        {
            throw new NotImplementedException();
        }        

        public Task<Guid> SubmitCommentsAsync(PostViewModel model, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }       
    }
}