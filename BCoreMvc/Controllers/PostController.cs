using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BCoreMvc.Models.ViewModels.Blog;
using BCoreMvc.Models.Commands;
using Microsoft.AspNetCore.Authorization;

namespace BCore.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostCommands _commands;

        public PostController(IPostCommands commands)
        {
            _commands = commands;
        }

        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync(Guid id)
        {
            var m = await _commands.GetPostById(id, HttpContext.User);

            return View(m);
        }

        [HttpPost]
        [ActionName("CommentSubmit")]
        public async Task<ActionResult> CommentSubmitAsync(PostViewModel m)
        {     
            if (!String.IsNullOrWhiteSpace(m.Comment.Text))      
                await _commands.SubmitCommentsAsync(m, HttpContext.User);

            return Redirect(Url.Action("Index", "Post", new { id = m.Id }) + "#commentAnchor");
        }

        [Authorize]
        [ActionName("DeleteComment")]
        public async Task<ActionResult> DeleteCommentAsync(Guid commentid, Guid postid)
        {            
            await _commands.DeleteCommentAsync(postid, commentid, HttpContext.User);
            
            return Redirect(Url.Action("Index", "Post", new { id = postid }) + "#commentAnchor");
        }
    }
}