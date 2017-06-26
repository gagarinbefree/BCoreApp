using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BCoreDao;
using BCoreDal;

namespace BCoreApi.Controllers
{
    [Produces("application/json")]
    public class HashesController : Controller
    {
        private IUoW _unit;

        public HashesController(IUoW unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [Route("api/Hashes/{tag}")]
        public async Task<IActionResult> GetHashes([FromRoute] string tag)
        {
            Hash hash = await _unit.HashRepository.GetAsync(f => f.Tag.ToUpper().Trim() == tag);
            if (hash == null)
                return NotFound();
            
            return Ok(hash);
        }

        [HttpGet]
        [Route("api/Posts/{id}/Hashes")]
        public async Task<IActionResult> GetHashes([FromRoute] Guid id)
        {
            ICollection<PostHash> postHashes = await _unit.PostHashRepository.GetAllAsync(f => f.PostId == id);
            if (postHashes.Count() == 0)
                return NotFound();

            IEnumerable<Hash> hashes = postHashes.Select(f => f.Hash);
            if (hashes.Count() == 0)
                return NotFound();

            return Ok(hashes);
        }

        [HttpGet]
        [Route("api/Posts/{id1}/Hashes/{id2}")]
        public async Task<IActionResult> GetHash([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            ICollection<PostHash> postHashes = await _unit.PostHashRepository.GetAllAsync(f => f.PostId == id1);
            if (postHashes.Count() == 0)
                return NotFound();

            Hash hash = postHashes.Select(f => f.Hash).SingleOrDefault(f => f.Id == id2);
            if (hash == null)
                return NotFound();

            return Ok(hash);
        }

        [HttpPut]
        [Route("api/Posts/{id1}/Hashes/{id2}")]
        public IActionResult PutHash([FromRoute] Guid id1, [FromRoute] Guid id2, [FromBody] Hash hash)
        {
            return BadRequest();
        }

        [HttpPost]
        [Route("api/Posts/{id}/Hashes")]
        public async Task<IActionResult> PostHash([FromRoute] Guid id, [FromBody] Hash hash)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Post post = await _unit.PostRepository.GetAsync(f => f.Id == id);
            if (post == null)
                return NotFound();

            try
            {
                Guid hashId = await _unit.HashRepository.CreateAsync(hash);

                await _unit.PostHashRepository.CreateAsync(new PostHash
                {
                    PostId = id,
                    HashId = hashId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetHash", new { id = hash.Id }, hash);
        }

        [Route("api/Posts/{id1}/Hashes/{id2}")]
        [HttpDelete]
        public IActionResult DeleteHash([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            return BadRequest();
        }
    }
}