using System.ComponentModel.DataAnnotations;

namespace Examen_2M1_is_.Dto.HabitacionesDto
{
    public class HabitacionesDto
    {
        [Key]
        public int id { set; get; }
        public int NumDeHabitacion { set; get; }
        public string TipoDeHabitacion { set; get; }
        public string EstadoDeHabitacion { set; get; }
        public decimal PrecioPorNoche { set; get; }
        public ICollection<Reservas> Reservas { set; get; }

    }
}
