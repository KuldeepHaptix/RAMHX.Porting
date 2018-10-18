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
    public class CoreModuleBlogsController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleBlogsController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleBlogs
        [HttpGet]
        public IEnumerable<CoreModuleBlogs> GetCoreModuleBlogs()
        {
            return _context.CoreModuleBlogs;
        }

        // GET: api/CoreModuleBlogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleBlogs([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleBlogs = await _context.CoreModuleBlogs.FindAsync(id);

            if (coreModuleBlogs == null)
            {
                return NotFound();
            }

            return Ok(coreModuleBlogs);
        }

        // PUT: api/CoreModuleBlogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleBlogs([FromRoute] int id, [FromBody] CoreModuleBlogs coreModuleBlogs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleBlogs.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleBlogs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleBlogsExists(id))
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

        // POST: api/CoreModuleBlogs
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleBlogs([FromBody] CoreModuleBlogs coreModuleBlogs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleBlogs.Add(coreModuleBlogs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleBlogs", new { id = coreModuleBlogs.Id }, coreModuleBlogs);
        }

        // DELETE: api/CoreModuleBlogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleBlogs([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleBlogs = await _context.CoreModuleBlogs.FindAsync(id);
            if (coreModuleBlogs == null)
            {
                return NotFound();
            }

            _context.CoreModuleBlogs.Remove(coreModuleBlogs);
            await _context.SaveChangesAsync();

            return Ok(coreModuleBlogs);
        }

        private bool CoreModuleBlogsExists(int id)
        {
            return _context.CoreModuleBlogs.Any(e => e.Id == id);
        }
    }
}