using AutoMapper;
using Examen_2M1_is_.Dto.HabitacionesDto;
using Examen_2M1_is_.Dto.ReservasDTo;

namespace Examen_2M1_is_
{
    public class mapeador:Profile
    {
        public mapeador()
        {
            CreateMap< Habitaciones, HabitacionesDto>().ReverseMap();
            CreateMap<Habitaciones, HabitacionesCreateDTo>().ReverseMap();
            CreateMap<Habitaciones, HabitacionesUpdateDto>().ReverseMap();

            CreateMap<Reservas, ReservasDto>().ReverseMap();
            CreateMap<Reservas, ReservasCreateDto>().ReverseMap();
            CreateMap<Reservas, ReservasUpdateDto>().ReverseMap();

        }
    }
}
