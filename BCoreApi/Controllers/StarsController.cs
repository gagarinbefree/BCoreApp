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
    public class StarsController : Controller
    {
        private IUoW _unit;

        public StarsController(IUoW unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [Route("api/Posts/{id}/Stars")]
        public async Task<IActionResult> GetStars([FromRoute] Guid id)
        {
            Post post = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (post == null)
                return NotFound();

            return Ok(post.Stars);
        }


        [HttpGet]
        [Route("api/Posts/{id1}/Stars/{id2}")]
        public async Task<IActionResult> GetStar([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Star star = await _unit.StarRepository.GetAsync(f => f.Id == id2 && f.PostId == id1);
            if (star == null)
                return NotFound();

            return Ok(star);
        }

        [HttpPut]
        [Route("api/Posts/{id1}/Stars/{id2}")]
        public IActionResult PutStar([FromRoute] Guid id1, [FromRoute] Guid id2, [FromBody] Star star)
        {            
            return BadRequest();
        }

        [HttpPost]
        [Route("api/Posts/{id}/Stars")]
        public async Task<IActionResult> PostStar([FromRoute] Guid id, [FromBody] Star star)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Post post = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (post == null)
                return NotFound();
            
            try
            {
                await _unit.StarRepository.CreateAsync(star);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetStar", new { id = star.Id }, star);
        }

        [Route("api/Posts/{id1}/Stars/{id2}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteStar([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Star star = await _unit.StarRepository.GetAsync(f => f.Id == id2 && f.PostId == id1);
            if (star == null)
                return NotFound();

            try
            {
                await _unit.StarRepository.DeleteAsync(star.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(star);
        }
    }
}