using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCoreDal;
using BCoreDal.Entities;
using BCoreDal.Contracts;

namespace BCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Collections")]
    public class CollectionsController : Controller
    {
        private IUoW _unit;

        public CollectionsController(IUoW unit)
        {
            _unit = unit;
        }

        // GET: api/Collections
        [HttpGet]
        public async Task<IEnumerable<Collection>> GetCollections()
        {
            return await _unit.CollectionRepository.GetAllAsync(take: 1000);
        }

        // GET: api/Collections/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollection([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Collection collection = await _unit.CollectionRepository.GetAsync(f => f.Id == id);
            if (collection == null)
                return NotFound();                

            return Ok(collection);
        }

        // PUT: api/Collections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection([FromRoute] Guid id, [FromBody] Collection collection)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != collection.Id)
                return BadRequest();

            try
            {
                await _unit.CollectionRepository.UpdateAsync(collection);
            }
            catch (Exception ex)
            {                
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Collections
        [HttpPost]
        public async Task<IActionResult> PostCollection([FromBody] Collection collection)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Guid id = await _unit.CollectionRepository.CreateAsync(collection);

            return CreatedAtAction("GetCollection", new { id = id }, collection);
        }

        // DELETE: api/Collections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Collection collection = await _unit.CollectionRepository.GetAsync(f => f.Id == id);
            if (collection == null)
                return NotFound();

            await _unit.CollectionRepository.DeleteAsync(collection);

            return Ok(collection);
        }

        [HttpGet]
        [Route("api/Collections/{id}/Items")]
        public async Task<IActionResult> GetItems([FromRoute] Guid id)
        {
            Collection collection = await _unit.CollectionRepository.GetAsync(f => f.Id == id);

            return Ok(collection.Items);
        }

        [HttpGet]
        [Route("api/Collections/{id1}/Item/{id2}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            Collection collection = await _unit.CollectionRepository.GetAsync(f => f.Id == id1);

            return Ok(collection.Items.SingleOrDefault(f => f.Id == id2));
       }

        [HttpPut]
        [Route("api/Collections/{id1}/Item/{id2}")]
        public async Task<IActionResult> PutItem([FromRoute] Guid id1, [FromRoute] Guid id2, [FromBody] Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (item.Collection.Id != id1)
                return BadRequest();

            if (item.Id != id2)
                return BadRequest();

            try
            {
                await _unit.ItemRepository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("api/Collections/{id}/Item")]
        public async Task<IActionResult> PostCollection([FromRoute] Guid id, [FromBody] Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (item.Collection.Id != id)
                return BadRequest();

            Guid resid = await _unit.ItemRepository.CreateAsync(item);

            return CreatedAtAction("GetCollection", new { id = resid }, item);
        }
        
        [HttpDelete]
        [Route("api/Collections/{id1}/Item/{id2}")]
        public async Task<IActionResult> DeleteCollection([FromRoute] Guid id1, [FromRoute] Guid id2)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Collection collection = await _unit.CollectionRepository.GetAsync(f => f.Id == id1);
            if (collection == null)
                return NotFound();

            Item item = await _unit.ItemRepository.GetAsync(f => f.Id == id2);
            if (item == null)
                return NotFound();

            await _unit.ItemRepository.DeleteAsync(item);

            return Ok(collection);
        }
    }
}