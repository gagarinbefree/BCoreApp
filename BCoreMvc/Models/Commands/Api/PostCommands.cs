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
            await Post<Post>($"{ApiURL}/Posts", new Post
            {
                CreatedOn = DateTime.Now,
                UserId = "c93c21f8-ea94-46af-b4d2-6ea2d18a137d"

            });
            return await Get<List<Post>>($"{ApiURL}/Posts");
        }

        public async Task<PostViewModel> GetPostById(Guid id, ClaimsPrincipal user)
        {
            Post post = await Get<Post>(String.Format("{0}/Posts/{1}", ApiURL, id));

            List<Part> parts = await Get<List<Part>>(String.Format("{0}/Posts/{1}/Parts", ApiURL, id));
            post.Parts = parts.OrderBy(f => f.CreatedOn).ToList();

            List<Comment> comments = await Get<List<Comment>>(String.Format("{0}/Posts/{1}/Comments", ApiURL, id));
            post.Comments = comments.OrderByDescending(f => f.CreatedOn).ToList();

            var model = Mapper.Map<PostViewModel>(post);                        

            model.StatusLine = new PostStatusLineViewModel();
            model.IsPreview = false;

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