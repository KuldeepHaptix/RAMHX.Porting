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
    public class CoreModulePackageMastersController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModulePackageMastersController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModulePackageMasters
        [HttpGet]
        public IEnumerable<CoreModulePackageMaster> GetCoreModulePackageMaster()
        {
            return _context.CoreModulePackageMaster;
        }

        // GET: api/CoreModulePackageMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModulePackageMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModulePackageMaster = await _context.CoreModulePackageMaster.FindAsync(id);

            if (coreModulePackageMaster == null)
            {
                return NotFound();
            }

            return Ok(coreModulePackageMaster);
        }

        // PUT: api/CoreModulePackageMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModulePackageMaster([FromRoute] int id, [FromBody] CoreModulePackageMaster coreModulePackageMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModulePackageMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModulePackageMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModulePackageMasterExists(id))
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

        // POST: api/CoreModulePackageMasters
        [HttpPost]
        public async Task<IActionResult> PostCoreModulePackageMaster([FromBody] CoreModulePackageMaster coreModulePackageMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModulePackageMaster.Add(coreModulePackageMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModulePackageMaster", new { id = coreModulePackageMaster.Id }, coreModulePackageMaster);
        }

        // DELETE: api/CoreModulePackageMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModulePackageMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModulePackageMaster = await _context.CoreModulePackageMaster.FindAsync(id);
            if (coreModulePackageMaster == null)
            {
                return NotFound();
            }

            _context.CoreModulePackageMaster.Remove(coreModulePackageMaster);
            await _context.SaveChangesAsync();

            return Ok(coreModulePackageMaster);
        }

        private bool CoreModulePackageMasterExists(int id)
        {
            return _context.CoreModulePackageMaster.Any(e => e.Id == id);
        }
    }
}