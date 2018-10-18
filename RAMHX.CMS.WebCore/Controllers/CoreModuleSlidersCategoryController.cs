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
    public class CoreModuleSlidersCategoryController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleSlidersCategoryController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleSlidersCategory
        [HttpGet]
        public IEnumerable<CoreModuleSliders> GetCoreModuleSliders()
        {
            return _context.CoreModuleSliders;
        }

        // GET: api/CoreModuleSlidersCategory/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleSliders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleSliders = await _context.CoreModuleSliders.FindAsync(id);

            if (coreModuleSliders == null)
            {
                return NotFound();
            }

            return Ok(coreModuleSliders);
        }

        // PUT: api/CoreModuleSlidersCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleSliders([FromRoute] int id, [FromBody] CoreModuleSliders coreModuleSliders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleSliders.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleSliders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleSlidersExists(id))
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

        // POST: api/CoreModuleSlidersCategory
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleSliders([FromBody] CoreModuleSliders coreModuleSliders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleSliders.Add(coreModuleSliders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleSliders", new { id = coreModuleSliders.Id }, coreModuleSliders);
        }

        // DELETE: api/CoreModuleSlidersCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleSliders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleSliders = await _context.CoreModuleSliders.FindAsync(id);
            if (coreModuleSliders == null)
            {
                return NotFound();
            }

            _context.CoreModuleSliders.Remove(coreModuleSliders);
            await _context.SaveChangesAsync();

            return Ok(coreModuleSliders);
        }

        private bool CoreModuleSlidersExists(int id)
        {
            return _context.CoreModuleSliders.Any(e => e.Id == id);
        }
    }
}