namespace Examen_2M1_is_.Repository.IRepository
{
    public interface IReservasRepository:IRepository<Reservas>
    {
        Task<Reservas> UpdateAsync(Reservas entity);
    }
}
