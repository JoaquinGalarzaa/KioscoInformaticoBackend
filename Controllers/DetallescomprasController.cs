using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KioscoInformaticoBackend.DataContext;
using KioscoInformaticoBackend.Models;

namespace KioscoInformaticoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallescomprasController : ControllerBase
    {
        private readonly KioscoContext _context;

        public DetallescomprasController(KioscoContext context)
        {
            _context = context;
        }

        // GET: api/Detallescompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detallescompra>>> GetDetallescompras()
        {
            return await _context.Detallescompras.ToListAsync();
        }

        // GET: api/Detallescompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detallescompra>> GetDetallescompra(int id)
        {
            var detallescompra = await _context.Detallescompras.FindAsync(id);

            if (detallescompra == null)
            {
                return NotFound();
            }

            return detallescompra;
        }

        // PUT: api/Detallescompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallescompra(int id, Detallescompra detallescompra)
        {
            if (id != detallescompra.Id)
            {
                return BadRequest();
            }

            _context.Entry(detallescompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallescompraExists(id))
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

        // POST: api/Detallescompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detallescompra>> PostDetallescompra(Detallescompra detallescompra)
        {
            _context.Detallescompras.Add(detallescompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallescompra", new { id = detallescompra.Id }, detallescompra);
        }

        // DELETE: api/Detallescompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallescompra(int id)
        {
            var detallescompra = await _context.Detallescompras.FindAsync(id);
            if (detallescompra == null)
            {
                return NotFound();
            }

            _context.Detallescompras.Remove(detallescompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallescompraExists(int id)
        {
            return _context.Detallescompras.Any(e => e.Id == id);
        }
    }
}
