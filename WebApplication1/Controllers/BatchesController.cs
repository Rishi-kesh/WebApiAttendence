using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/Batches")]
    public class BatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Batches
        [HttpGet]
        public IEnumerable<Batch> GetBatchs()
        {
            return _context.Batchs;
        }

        // GET: api/Batches/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBatch([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var batch = await _context.Batchs.SingleOrDefaultAsync(m => m.Id == id);

            if (batch == null)
            {
                return NotFound();
            }

            return Ok(batch);
        }

        // PUT: api/Batches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatch([FromRoute] int id, [FromBody] Batch batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != batch.Id)
            {
                return BadRequest();
            }

            _context.Entry(batch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(id))
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

        // POST: api/Batches
        [HttpPost]
        public async Task<IActionResult> PostBatch([FromBody] Batch batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Batchs.Add(batch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBatch", new { id = batch.Id }, batch);
        }

        // DELETE: api/Batches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var batch = await _context.Batchs.SingleOrDefaultAsync(m => m.Id == id);
            if (batch == null)
            {
                return NotFound();
            }

            _context.Batchs.Remove(batch);
            await _context.SaveChangesAsync();

            return Ok(batch);
        }

        private bool BatchExists(int id)
        {
            return _context.Batchs.Any(e => e.Id == id);
        }
    }
}