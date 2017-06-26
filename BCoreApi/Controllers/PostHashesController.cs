using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCoreDal.SqlServer;
using BCoreDao;
using Microsoft.Extensions.Configuration;
using BCoreDal;

namespace BCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/PostHashes")]
    public class PostHashesController : Controller
    {
        private IConfiguration _configuration;
        private IUoW _unit;

        public PostHashesController(IConfiguration configuration, IUoW unit)
        {
            _configuration = configuration;
            _unit = unit;
        }
        
        [HttpGet("api/PostHashes/{hashId}")]
        public async Task<IActionResult> GetPostHash([FromRoute] Guid hashId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ICollection<PostHash> postHashes = await _unit.PostHashRepository.GetAllAsync(where: f => f.HashId == hashId, take: 1000);            

            return Ok(postHashes);
        }

        [HttpPost]
        [Route("api/PostHashes")]
        public async Task<IActionResult> PostPostHashes([FromBody] PostHash postHash)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                await _unit.PostHashRepository.CreateAsync(postHash);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetPostHash", new { hashId = postHash.HashId }, postHash);
        }
    }
}