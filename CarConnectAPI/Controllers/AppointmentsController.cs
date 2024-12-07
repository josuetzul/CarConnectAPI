using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarConnectAPI.Context;
using CarConnectAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CarConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        // GET: api/Appointments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // GET: api/Appointments/filter
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByType([FromQuery] string tipo)
        {
            DateTime today = DateTime.Now.Date;
            DateTime tomorrow = today.AddDays(1);

            IQueryable<Appointment> query = _context.Appointments;

            switch (tipo?.ToLower())
            {
                case "pasadas":
                    query = query.Where(a => a.FechaCita < today);
                    break;

                case "futuras":
                    query = query.Where(a => a.FechaCita > tomorrow);
                    break;

                case "hoy":
                    query = query.Where(a => a.FechaCita.Date == today);
                    break;

                case "maniana":
                    query = query.Where(a => a.FechaCita.Date == tomorrow);
                    break;

                default:
                    return BadRequest(new { error = "El parámetro 'tipo' es inválido. Use 'pasadas', 'futuras', 'hoy' o 'maniana'." });
            }

            return await query.ToListAsync();
        }


        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            if (appointment.FechaCita < DateTime.UtcNow)
            {
                return BadRequest("La fecha no puede ser anterior a la fecha actual.");
            }
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);

        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
