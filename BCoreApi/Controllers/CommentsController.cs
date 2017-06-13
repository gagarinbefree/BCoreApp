using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCoreDal.Entities;
using BCoreDal.SqlServer;
using BCoreDal.Contracts;

namespace BCoreApi.Controllers
{
    [Produces("application/json")]
    public class CommentsController : Controller
    {
        private IUoW _unit;

        public CommentsController(IUoW unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [Route("api/Posts/{id}/Comments")]
        public async Task<IActionResult> GetComments([FromRoute] Guid id)
        {
            Post post = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (post == null)
                return NotFound();

            return Ok(post.Comments);
        }


        [HttpGet]
        [Route("api/Posts/{id1}/Comments/{id2}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Comment comment = await _unit.CommentRepository.GetAsync(f => f.Id == id2 && f.PostId == id1);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        [HttpPut]
        [Route("api/Posts/{id1}/Comments/{id2}")]
        public async Task<IActionResult> PutComment([FromRoute] Guid id1, [FromRoute] Guid id2, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id1 != comment.PostId)
                return BadRequest();

            if (id2 != comment.Id)
                return BadRequest();

            Comment exist = await _unit.CommentRepository.GetAsync(f => f.Id == id2 && f.PostId == id1);
            if (exist == null)
                return NotFound();

            try
            {
                await _unit.CommentRepository.UpdateAsync(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("api/Posts/{id}/Comments")]
        public async Task<IActionResult> PostComment([FromRoute] Guid id, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Post post = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (post == null)
                return NotFound();

            try
            {
                await _unit.CommentRepository.CreateAsync(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        [Route("api/Posts/{id1}/Comments/{id2}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Comment comment = await _unit.CommentRepository.GetAsync(f => f.Id == id2 && f.PostId == id1);
            if (comment == null)
                return NotFound();

            try
            {
                comment.Removed = DateTime.Now;

                await _unit.CommentRepository.UpdateAsync(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(comment);
        }
    }
}