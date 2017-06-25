using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BCoreDao;
using BCoreDal;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BCoreApi.Controllers
{
    [Produces("application/json")]    
    public class PostsController : Controller
    {
        private IConfiguration _configuration;
        private IUoW _unit;

        public PostsController(IConfiguration configuration, IUoW unit)
        {
            _configuration = configuration;
            _unit = unit;            
        }

        [HttpGet("/api/Posts")]
        //[Route("api/Posts")]
        public async Task<IActionResult> GetPosts(int? page = null)
        {            
            if (page != null)
            {                
                int pageSize = _configuration.GetValue<int>("DefaultPageSize");                
             
                return Ok(await _unit.PostRepository.GetAllAsync<DateTime>(orderBy: f => f.CreatedOn,
                    sort: SortOrder.Descending,
                    skip: ((page - 1) * pageSize),
                    take: pageSize));
            } 
            else
                return Ok(await _unit.PostRepository.GetAllAsync(take: 1000));            
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