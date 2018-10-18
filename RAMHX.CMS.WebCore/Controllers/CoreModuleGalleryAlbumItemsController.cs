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
    public class CoreModuleGalleryAlbumItemsController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleGalleryAlbumItemsController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleGalleryAlbumItems
        [HttpGet]
        public IEnumerable<CoreModuleGalleryAlbumItem> GetCoreModuleGalleryAlbumItem()
        {
            return _context.CoreModuleGalleryAlbumItem;
        }

        // GET: api/CoreModuleGalleryAlbumItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleGalleryAlbumItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleGalleryAlbumItem = await _context.CoreModuleGalleryAlbumItem.FindAsync(id);

            if (coreModuleGalleryAlbumItem == null)
            {
                return NotFound();
            }

            return Ok(coreModuleGalleryAlbumItem);
        }

        // PUT: api/CoreModuleGalleryAlbumItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleGalleryAlbumItem([FromRoute] int id, [FromBody] CoreModuleGalleryAlbumItem coreModuleGalleryAlbumItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleGalleryAlbumItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleGalleryAlbumItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleGalleryAlbumItemExists(id))
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

        // POST: api/CoreModuleGalleryAlbumItems
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleGalleryAlbumItem([FromBody] CoreModuleGalleryAlbumItem coreModuleGalleryAlbumItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleGalleryAlbumItem.Add(coreModuleGalleryAlbumItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleGalleryAlbumItem", new { id = coreModuleGalleryAlbumItem.Id }, coreModuleGalleryAlbumItem);
        }

        // DELETE: api/CoreModuleGalleryAlbumItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleGalleryAlbumItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleGalleryAlbumItem = await _context.CoreModuleGalleryAlbumItem.FindAsync(id);
            if (coreModuleGalleryAlbumItem == null)
            {
                return NotFound();
            }

            _context.CoreModuleGalleryAlbumItem.Remove(coreModuleGalleryAlbumItem);
            await _context.SaveChangesAsync();

            return Ok(coreModuleGalleryAlbumItem);
        }

        private bool CoreModuleGalleryAlbumItemExists(int id)
        {
            return _context.CoreModuleGalleryAlbumItem.Any(e => e.Id == id);
        }
    }
}