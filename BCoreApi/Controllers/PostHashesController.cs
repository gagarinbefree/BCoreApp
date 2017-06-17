using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCoreDal.Entities;
using BCoreDal.SqlServer;

namespace BCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/PostHashes")]
    public class PostHashesController : Controller
    {
        private readonly SqlServerDbContext _context;

        public PostHashesController(SqlServerDbContext context)
        {
            _context = context;
        }

        // GET: api/PostHashes
        [HttpGet]
        public IEnumerable<PostHash> GetPostHashes()
        {
            return _context.PostHashes;
        }

        // GET: api/PostHashes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostHash([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postHash = await _context.PostHashes.SingleOrDefaultAsync(m => m.Id == id);

            if (postHash == null)
            {
                return NotFound();
            }

            return Ok(postHash);
        }

        // PUT: api/PostHashes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostHash([FromRoute] Guid id, [FromBody] PostHash postHash)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postHash.Id)
            {
                return BadRequest();
            }

            _context.Entry(postHash).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostHashExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PostHashes
        [HttpPost]
        public async Task<IActionResult> PostPostHash([FromBody] PostHash postHash)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PostHashes.Add(postHash);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostHash", new { id = postHash.Id }, postHash);
        }

        // DELETE: api/PostHashes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostHash([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postHash = await _context.PostHashes.SingleOrDefaultAsync(m => m.Id == id);
            if (postHash == null)
            {
                return NotFound();
            }

            _context.PostHashes.Remove(postHash);
            await _context.SaveChangesAsync();

            return Ok(postHash);
        }

        private bool PostHashExists(Guid id)
        {
            return _context.PostHashes.Any(e => e.Id == id);
        }
    }
}