namespace Examen_2M1_is_
{
    public class Reservas
    {
        public int id { set; get; }
        public int habitacionId { set; get; }
        public DateOnly FechaInicio { set; get; }
        public DateOnly FechaDeFIn { set; get; }
        public string estadoDeReserva { set; get; }
        public Habitaciones Habitacione { set; get; }
    }
}
