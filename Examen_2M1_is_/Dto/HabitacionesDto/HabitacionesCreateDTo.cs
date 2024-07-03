namespace Examen_2M1_is_.Dto.HabitacionesDto
{
    public class HabitacionesCreateDTo
    {
        public int NumDeHabitacion { set; get; }
        public string TipoDeHabitacion { set; get; }
        public string EstadoDeHabitacion { set; get; }
        public decimal PrecioPorNoche { set; get; }
        public ICollection<Reservas> Reservas { set; get; }

    }
}
