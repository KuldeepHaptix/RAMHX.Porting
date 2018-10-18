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
    public class CoreModuleGalleryAlbumsController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleGalleryAlbumsController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleGalleryAlbums
        [HttpGet]
        public IEnumerable<CoreModuleGalleryAlbum> GetCoreModuleGalleryAlbum()
        {
            return _context.CoreModuleGalleryAlbum;
        }

        // GET: api/CoreModuleGalleryAlbums/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleGalleryAlbum([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleGalleryAlbum = await _context.CoreModuleGalleryAlbum.FindAsync(id);

            if (coreModuleGalleryAlbum == null)
            {
                return NotFound();
            }

            return Ok(coreModuleGalleryAlbum);
        }

        // PUT: api/CoreModuleGalleryAlbums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleGalleryAlbum([FromRoute] int id, [FromBody] CoreModuleGalleryAlbum coreModuleGalleryAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleGalleryAlbum.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleGalleryAlbum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleGalleryAlbumExists(id))
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

        // POST: api/CoreModuleGalleryAlbums
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleGalleryAlbum([FromBody] CoreModuleGalleryAlbum coreModuleGalleryAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleGalleryAlbum.Add(coreModuleGalleryAlbum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleGalleryAlbum", new { id = coreModuleGalleryAlbum.Id }, coreModuleGalleryAlbum);
        }

        // DELETE: api/CoreModuleGalleryAlbums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleGalleryAlbum([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleGalleryAlbum = await _context.CoreModuleGalleryAlbum.FindAsync(id);
            if (coreModuleGalleryAlbum == null)
            {
                return NotFound();
            }

            _context.CoreModuleGalleryAlbum.Remove(coreModuleGalleryAlbum);
            await _context.SaveChangesAsync();

            return Ok(coreModuleGalleryAlbum);
        }

        private bool CoreModuleGalleryAlbumExists(int id)
        {
            return _context.CoreModuleGalleryAlbum.Any(e => e.Id == id);
        }
    }
}