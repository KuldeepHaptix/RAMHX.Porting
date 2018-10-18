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
    public class CoreModuleTestimonialMastersController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleTestimonialMastersController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleTestimonialMasters
        [HttpGet]
        public IEnumerable<CoreModuleTestimonialMaster> GetCoreModuleTestimonialMaster()
        {
            return _context.CoreModuleTestimonialMaster;
        }

        // GET: api/CoreModuleTestimonialMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleTestimonialMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleTestimonialMaster = await _context.CoreModuleTestimonialMaster.FindAsync(id);

            if (coreModuleTestimonialMaster == null)
            {
                return NotFound();
            }

            return Ok(coreModuleTestimonialMaster);
        }

        // PUT: api/CoreModuleTestimonialMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleTestimonialMaster([FromRoute] int id, [FromBody] CoreModuleTestimonialMaster coreModuleTestimonialMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleTestimonialMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleTestimonialMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleTestimonialMasterExists(id))
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

        // POST: api/CoreModuleTestimonialMasters
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleTestimonialMaster([FromBody] CoreModuleTestimonialMaster coreModuleTestimonialMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleTestimonialMaster.Add(coreModuleTestimonialMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleTestimonialMaster", new { id = coreModuleTestimonialMaster.Id }, coreModuleTestimonialMaster);
        }

        // DELETE: api/CoreModuleTestimonialMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleTestimonialMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleTestimonialMaster = await _context.CoreModuleTestimonialMaster.FindAsync(id);
            if (coreModuleTestimonialMaster == null)
            {
                return NotFound();
            }

            _context.CoreModuleTestimonialMaster.Remove(coreModuleTestimonialMaster);
            await _context.SaveChangesAsync();

            return Ok(coreModuleTestimonialMaster);
        }

        private bool CoreModuleTestimonialMasterExists(int id)
        {
            return _context.CoreModuleTestimonialMaster.Any(e => e.Id == id);
        }
    }
}