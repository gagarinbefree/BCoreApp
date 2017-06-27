using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BCoreMvc.Models.ViewModels.Blog;
using BCoreMvc.Models.Commands;


namespace BCoreMvc.Controllers
{
    public class TopController : Controller
    {
        private readonly ITopCommands _commands;

        public TopController(ITopCommands commands)
        {
            _commands = commands;
        }

        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            TopViewModel model = await _commands.GetTopPostsAsync(HttpContext.User);

            return View(model);
        }

        public async Task<IActionResult> Page(int page)
        {
            TopViewModel model = await _commands.GetTopPostsAsync(HttpContext.User, page);

            return PartialView("_Page", model);
        }
    }
}
