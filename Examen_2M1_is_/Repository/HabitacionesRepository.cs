using Examen_2M1_is_.DAta;
using Examen_2M1_is_.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Examen_2M1_is_.Repository
{
    public class HabitacionesRepository : Repository<Habitaciones>,IHabitacionesRepository
    {
        private HotelContext _context;

        public HabitacionesRepository(HotelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Habitaciones> UpdateAsync(Habitaciones entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
