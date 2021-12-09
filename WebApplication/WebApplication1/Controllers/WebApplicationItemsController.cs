using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApplicationItemsController : ControllerBase
    {
        private readonly WebApplicationContext _context;

        public WebApplicationItemsController(WebApplicationContext context)
        {
            _context = context;
        }

        // GET: api/WebApplicationItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebApplicationItem>>> GetWebApplicationItems()
        {
            return await _context.WebApplicationItems.ToListAsync();
        }

        // GET: api/WebApplicationItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WebApplicationItem>> GetWebApplicationItem(int id)
        {
            var webApplicationItem = await _context.WebApplicationItems.FindAsync(id);

            if (webApplicationItem == null)
            {
                return NotFound();
            }

            return webApplicationItem;
        }

        // PUT: api/WebApplicationItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebApplicationItem(int id, WebApplicationItem webApplicationItem)
        {
            if (id != webApplicationItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(webApplicationItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebApplicationItemExists(id))
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

        // POST: api/WebApplicationItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WebApplicationItem>> PostWebApplicationItem(WebApplicationItem webApplicationItem)
        {
            _context.WebApplicationItems.Add(webApplicationItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWebApplicationItem), new { id = webApplicationItem.Id }, webApplicationItem);

        }

        // DELETE: api/WebApplicationItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebApplicationItem(int id)
        {
            var webApplicationItem = await _context.WebApplicationItems.FindAsync(id);
            if (webApplicationItem == null)
            {
                return NotFound();
            }

            _context.WebApplicationItems.Remove(webApplicationItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebApplicationItemExists(int id)
        {
            return _context.WebApplicationItems.Any(e => e.Id == id);
        }
    }
}
