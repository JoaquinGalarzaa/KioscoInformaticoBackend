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
    public class DetallesventasController : ControllerBase
    {
        private readonly KioscoContext _context;

        public DetallesventasController(KioscoContext context)
        {
            _context = context;
        }

        // GET: api/Detallesventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detallesventa>>> GetDetallesventas()
        {
            return await _context.Detallesventas.ToListAsync();
        }

        // GET: api/Detallesventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detallesventa>> GetDetallesventa(int id)
        {
            var detallesventa = await _context.Detallesventas.FindAsync(id);

            if (detallesventa == null)
            {
                return NotFound();
            }

            return detallesventa;
        }

        // PUT: api/Detallesventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallesventa(int id, Detallesventa detallesventa)
        {
            if (id != detallesventa.Id)
            {
                return BadRequest();
            }

            _context.Entry(detallesventa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallesventaExists(id))
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

        // POST: api/Detallesventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detallesventa>> PostDetallesventa(Detallesventa detallesventa)
        {
            _context.Detallesventas.Add(detallesventa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallesventa", new { id = detallesventa.Id }, detallesventa);
        }

        // DELETE: api/Detallesventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallesventa(int id)
        {
            var detallesventa = await _context.Detallesventas.FindAsync(id);
            if (detallesventa == null)
            {
                return NotFound();
            }

            _context.Detallesventas.Remove(detallesventa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallesventaExists(int id)
        {
            return _context.Detallesventas.Any(e => e.Id == id);
        }
    }
}
