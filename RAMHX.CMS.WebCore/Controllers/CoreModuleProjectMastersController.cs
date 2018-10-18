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
    public class CoreModuleProjectMastersController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleProjectMastersController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleProjectMasters
        [HttpGet]
        public IEnumerable<CoreModuleProjectMaster> GetCoreModuleProjectMaster()
        {
            return _context.CoreModuleProjectMaster;
        }

        // GET: api/CoreModuleProjectMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleProjectMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleProjectMaster = await _context.CoreModuleProjectMaster.FindAsync(id);

            if (coreModuleProjectMaster == null)
            {
                return NotFound();
            }

            return Ok(coreModuleProjectMaster);
        }

        // PUT: api/CoreModuleProjectMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleProjectMaster([FromRoute] int id, [FromBody] CoreModuleProjectMaster coreModuleProjectMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleProjectMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleProjectMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleProjectMasterExists(id))
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

        // POST: api/CoreModuleProjectMasters
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleProjectMaster([FromBody] CoreModuleProjectMaster coreModuleProjectMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleProjectMaster.Add(coreModuleProjectMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleProjectMaster", new { id = coreModuleProjectMaster.Id }, coreModuleProjectMaster);
        }

        // DELETE: api/CoreModuleProjectMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleProjectMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleProjectMaster = await _context.CoreModuleProjectMaster.FindAsync(id);
            if (coreModuleProjectMaster == null)
            {
                return NotFound();
            }

            _context.CoreModuleProjectMaster.Remove(coreModuleProjectMaster);
            await _context.SaveChangesAsync();

            return Ok(coreModuleProjectMaster);
        }

        private bool CoreModuleProjectMasterExists(int id)
        {
            return _context.CoreModuleProjectMaster.Any(e => e.Id == id);
        }
    }
}