using Examen_2M1_is_.DAta;
using Examen_2M1_is_.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Examen_2M1_is_.Repository
{
    public class ReservasRepository : Repository<Reservas>, IReservasRepository
    {
        private readonly HotelContext _context;
        public ReservasRepository(HotelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Reservas> UpdateAsync(Reservas entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
