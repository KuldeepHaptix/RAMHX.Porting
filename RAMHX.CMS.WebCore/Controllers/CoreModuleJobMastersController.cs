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
    public class CoreModuleJobMastersController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleJobMastersController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleJobMasters
        [HttpGet]
        public IEnumerable<CoreModuleJobMaster> GetCoreModuleJobMaster()
        {
            return _context.CoreModuleJobMaster;
        }

        // GET: api/CoreModuleJobMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleJobMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleJobMaster = await _context.CoreModuleJobMaster.FindAsync(id);

            if (coreModuleJobMaster == null)
            {
                return NotFound();
            }

            return Ok(coreModuleJobMaster);
        }

        // PUT: api/CoreModuleJobMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleJobMaster([FromRoute] int id, [FromBody] CoreModuleJobMaster coreModuleJobMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleJobMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleJobMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleJobMasterExists(id))
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

        // POST: api/CoreModuleJobMasters
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleJobMaster([FromBody] CoreModuleJobMaster coreModuleJobMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleJobMaster.Add(coreModuleJobMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleJobMaster", new { id = coreModuleJobMaster.Id }, coreModuleJobMaster);
        }

        // DELETE: api/CoreModuleJobMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleJobMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleJobMaster = await _context.CoreModuleJobMaster.FindAsync(id);
            if (coreModuleJobMaster == null)
            {
                return NotFound();
            }

            _context.CoreModuleJobMaster.Remove(coreModuleJobMaster);
            await _context.SaveChangesAsync();

            return Ok(coreModuleJobMaster);
        }

        private bool CoreModuleJobMasterExists(int id)
        {
            return _context.CoreModuleJobMaster.Any(e => e.Id == id);
        }
    }
}