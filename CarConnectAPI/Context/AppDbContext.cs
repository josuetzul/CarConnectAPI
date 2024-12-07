using CarConnectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarConnectAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
               
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<Appointment> Appointments { get; set; }
    }
}
