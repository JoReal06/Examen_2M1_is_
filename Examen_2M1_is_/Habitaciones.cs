namespace Examen_2M1_is_
{
    public class Habitaciones
    {
        public int id { set; get; }
        public int NumDeHabitacion { set; get; }
        public string TipoDeHabitacion { set; get;  }
        public string EstadoDeHabitacion { set; get;}
        public decimal PrecioPorNoche { set; get; }
        public ICollection<Reservas> Reservas { set; get; }
        
    }
}
