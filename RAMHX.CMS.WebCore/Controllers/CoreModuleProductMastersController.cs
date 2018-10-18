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
    public class CoreModuleProductMastersController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleProductMastersController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleProductMasters
        [HttpGet]
        public IEnumerable<CoreModuleProductMaster> GetCoreModuleProductMaster()
        {
            return _context.CoreModuleProductMaster;
        }

        // GET: api/CoreModuleProductMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleProductMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleProductMaster = await _context.CoreModuleProductMaster.FindAsync(id);

            if (coreModuleProductMaster == null)
            {
                return NotFound();
            }

            return Ok(coreModuleProductMaster);
        }

        // PUT: api/CoreModuleProductMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleProductMaster([FromRoute] int id, [FromBody] CoreModuleProductMaster coreModuleProductMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleProductMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleProductMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleProductMasterExists(id))
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

        // POST: api/CoreModuleProductMasters
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleProductMaster([FromBody] CoreModuleProductMaster coreModuleProductMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleProductMaster.Add(coreModuleProductMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleProductMaster", new { id = coreModuleProductMaster.Id }, coreModuleProductMaster);
        }

        // DELETE: api/CoreModuleProductMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleProductMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleProductMaster = await _context.CoreModuleProductMaster.FindAsync(id);
            if (coreModuleProductMaster == null)
            {
                return NotFound();
            }

            _context.CoreModuleProductMaster.Remove(coreModuleProductMaster);
            await _context.SaveChangesAsync();

            return Ok(coreModuleProductMaster);
        }

        private bool CoreModuleProductMasterExists(int id)
        {
            return _context.CoreModuleProductMaster.Any(e => e.Id == id);
        }
    }
}