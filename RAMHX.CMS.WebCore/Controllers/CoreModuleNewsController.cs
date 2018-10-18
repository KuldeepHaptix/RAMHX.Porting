using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RAMHX.CMS.DataAccessCore;

namespace RAMHX.CMS.WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoreModuleNewsController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleNewsController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleNews
        [HttpGet]
        public IEnumerable<CoreModuleNews> GetCoreModuleNews()
        {
            return _context.CoreModuleNews;
        }

        // GET: api/CoreModuleNews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleNews([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleNews = await _context.CoreModuleNews.FindAsync(id);

            if (coreModuleNews == null)
            {
                return NotFound();
            }

            return Ok(coreModuleNews);
        }

        // PUT: api/CoreModuleNews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleNews([FromRoute] int id, [FromBody] CoreModuleNews coreModuleNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleNews.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleNews).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleNewsExists(id))
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

        // POST: api/CoreModuleNews
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleNews([FromBody] CoreModuleNews coreModuleNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleNews.Add(coreModuleNews);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleNews", new { id = coreModuleNews.Id }, coreModuleNews);
        }

        // DELETE: api/CoreModuleNews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleNews([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleNews = await _context.CoreModuleNews.FindAsync(id);
            if (coreModuleNews == null)
            {
                return NotFound();
            }

            _context.CoreModuleNews.Remove(coreModuleNews);
            await _context.SaveChangesAsync();

            return Ok(coreModuleNews);
        }

        private bool CoreModuleNewsExists(int id)
        {
            return _context.CoreModuleNews.Any(e => e.Id == id);
        }
    }
}