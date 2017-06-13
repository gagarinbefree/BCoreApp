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
    public class PartsController : Controller
    {
        private IUoW _unit;

        public PartsController(IUoW unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [Route("api/Posts/{id}/Parts")]
        public async Task<IActionResult> GetParts([FromRoute] Guid id)
        {
            Post post = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (post == null)
                return NotFound();

            return Ok(post.Parts);
        }

        
        [HttpGet]
        [Route("api/Posts/{id1}/Parts/{id2}")]
        public async Task<IActionResult> GetPart([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Part part = await _unit.PartRepository.GetAsync(f => f.Id == id2 && f.PostId == id1);
            if (part == null)
                return NotFound();

            return Ok(part);
        }
        
        [HttpPut]
        [Route("api/Posts/{id1}/Parts/{id2}")]
        public async Task<IActionResult> PutPart([FromRoute] Guid id1, [FromRoute] Guid id2, [FromBody] Part part)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id1 != part.PostId)
                return BadRequest();

            if (id2 != part.Id)
                return BadRequest();

            Part exist = await _unit.PartRepository.GetAsync(f => f.Id == id2 && f.PostId == id1);
            if (exist == null)
                return NotFound();

            try
            {
                await _unit.PartRepository.UpdateAsync(part);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            

            return NoContent();
        }

        [HttpPost]
        [Route("api/Posts/{id}/Parts")]
        public async Task<IActionResult> PostPart([FromRoute] Guid id, [FromBody] Part part)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Post post = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (post == null)
                return NotFound();

            try
            {
                await _unit.PartRepository.CreateAsync(part);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetPart", new { id1 = part.PostId, id2 = part.Id }, part);
        }
        
        [Route("api/Posts/{id1}/Parts/{id2}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePart([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Part part = await _unit.PartRepository.GetAsync(f => f.Id == id2 && f.PostId == id1);
            if (part == null)
                return NotFound();

            try
            {
                await _unit.PartRepository.DeleteAsync(id2);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(part);
        }
    }
}