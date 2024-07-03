using Microsoft.EntityFrameworkCore;

namespace Examen_2M1_is_.DAta
{
    public class HotelContext:DbContext
    {
        public DbSet<Habitaciones> Habitaciones { get; set; }
        public DbSet<Reservas> Reservas { get; set; }

        public HotelContext(DbContextOptions<HotelContext> options):base(options)
        {
                
        }
    }
}
