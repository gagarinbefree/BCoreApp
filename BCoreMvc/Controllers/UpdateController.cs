using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BCoreMvc.Models.ViewModels.Blog;
using BCoreMvc.Models.Commands;

namespace BCoreMvc.Controllers
{
   
    public class UpdateController : Controller
    {
        private readonly IUpdateCommands _commands;

        public UpdateController(IUpdateCommands commands)
        {
            _commands = commands;
        }

        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            UpdateViewModel m = await _commands.GetPostsByUserAsync(HttpContext.User);

            return View(m);
        }

        [HttpPost]
        public IActionResult Post(UpdateViewModel m)
        {
            ModelState.Clear();

            _commands.AddPartToPost(m);

            ViewData.TemplateInfo.HtmlFieldPrefix = nameof(m.PreviewPost);

            return PartialView("_Post", m.PreviewPost);
        }

        [HttpPost]
        [ActionName("Submit")]
        public async Task<ActionResult> SubmitAsync(UpdateViewModel m)
        {
            ModelState.Clear();

            await _commands.SubmitPostAsync(m, HttpContext.User);

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _commands.DeletePostAsync(id, HttpContext.User);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Page(int page)
        {
            UpdateViewModel model = await _commands.GetPostsByUserAsync(HttpContext.User, page);

            return PartialView("_Page", model);
        }
    }
}
