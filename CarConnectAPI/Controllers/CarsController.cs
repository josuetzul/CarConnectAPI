using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarConnectAPI.Context;
using CarConnectAPI.Models;

namespace CarConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCars(string? query, string? type, string? tier, string? state)
        {
            var results = _context.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                results = results.Where(v => v.Name.Contains(query) || v.Id.ToString().Contains(query));
            }

            if (!string.IsNullOrEmpty(type))
            {
                results = results.Where(v => v.Type == type);
            }
            if (!string.IsNullOrEmpty(tier))
            {
                results = results.Where(v => v.Tier == tier);
            }
            if (!string.IsNullOrEmpty(state))
            {
                results = results.Where(v => v.State == state);
            }

            return Ok(await results.ToListAsync());
        }


        [HttpGet("types")]
        public async Task<IActionResult> GetCarTypes()
        {
            var types = await _context.Cars
                .Select(c => c.Type)
                .Distinct()
                .ToListAsync();
            return Ok(types);
        }

        [HttpGet("tiers")]
        public async Task<IActionResult> GetCarTiers()
        {
            var tiers = await _context.Cars
                .Select(c => c.Tier)
                .Distinct()
                .ToListAsync();
            return Ok(tiers);
        }

        [HttpGet("states")]
        public async Task<IActionResult> GetCarStates()
        {
            var states = await _context.Cars
                .Select(c => c.State)
                .Distinct()
                .ToListAsync();
            return Ok(states);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
