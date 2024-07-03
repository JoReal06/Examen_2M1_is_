namespace Examen_2M1_is_.Repository.IRepository
{
    public interface IHabitacionesRepository:IRepository<Habitaciones>
    {
        Task<Habitaciones> UpdateAsync(Habitaciones entity);
    }
}
