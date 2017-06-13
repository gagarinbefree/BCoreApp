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
    public class PostsController : Controller
    {
        private IUoW _unit;

        public PostsController(IUoW unit)
        {
            _unit = unit;
        }
        
        [HttpGet]
        [Route("api/Posts")]
        public async Task<IActionResult> GetPosts()
        {
            ICollection<Post> posts = await _unit.PostRepository.GetAllAsync(take: 1000);

            return Ok(posts);
        }
        
        [HttpGet]
        [Route("api/Posts/{id}")]        
        public async Task<IActionResult> GetPost([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Post post = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }
        
        [HttpPut]
        [Route("api/Posts/{id}")]
        public async Task<IActionResult> PutPost([FromRoute] Guid id, [FromBody] Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != post.Id)
                return BadRequest();
            
            Post exsist = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (exsist == null)
                return NotFound();

            try
            {                     
                await _unit.PostRepository.UpdateAsync(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
        
        [HttpPost]
        [Route("api/Posts")]
        public async Task<IActionResult> PostPost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                await _unit.PostRepository.CreateAsync(post);                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        [HttpDelete]
        [Route("api/Posts/{id}")]        
        public async Task<IActionResult> DeletePost([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Post post = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (post == null)
                return NotFound();

            try
            {
                post.Removed = DateTime.Now;

                await _unit.PostRepository.UpdateAsync(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(post);
        }
    }
}