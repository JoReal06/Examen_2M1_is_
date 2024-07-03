using System.ComponentModel.DataAnnotations;

namespace Examen_2M1_is_.Dto.ReservasDTo
{
    public class ReservasDto
    {
        [Key]
        public int id { set; get; }
        public int habitacionId { set; get; }
        public DateOnly FechaInicio { set; get; }
        public DateOnly FechaDeFIn { set; get; }
        public string estadoDeReserva { set; get; }
        public Habitaciones Habitacione { set; get; }
    }
}
