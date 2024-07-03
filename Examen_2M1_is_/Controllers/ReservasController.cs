using AutoMapper;
using Examen_2M1_is_.Dto.HabitacionesDto;
using Examen_2M1_is_.Dto.ReservasDTo;
using Examen_2M1_is_.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen_2M1_is_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservasRepository _reservasRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ReservasController(IReservasRepository reservasRepository, ILogger logger, IMapper mapper)
        {
            _reservasRepository = reservasRepository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabitacionesDto>>> GetReservacion()
        {
            try
            {
                _logger.LogInformation("Obteniendo las Reservaciones presentes");

                var reservaciones = await _reservasRepository.GetAllAsyn();

                return Ok(_mapper.Map<IEnumerable<ReservasDto>>(reservaciones));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener las reservaciones: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener las reservaciones.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HabitacionesDto>>> GEtReserva(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogError($"ID de la reserva no es válido: {id}");
                    return BadRequest("ID de la reserva no es válido");
                }


                _logger.LogInformation("Obteniendo la reserva con el " + id);

                var rerva = await _reservasRepository.GetByIdAsyn(id);
                if (rerva == null)
                {
                    _logger.LogWarning($"No se encontró ninguna reserva con este ID: {id}");
                    return NotFound("habitacion no encontrada.");
                }
                return Ok(_mapper.Map<IEnumerable<ReservasDto>>(rerva));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener las reserva: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener las reserva.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<HabitacionesDto>>> PostHabitaciones(HabitacionesCreateDTo dto)
        {
            if (dto == null)
            {
                _logger.LogError("Se recibió una habitacion nula en la solicitud.");
                return BadRequest("la habitacion no puede ser nula.");
            }

            try
            {
                _logger.LogInformation($"Creando una nueva habitacion de tipo {dto.TipoDeHabitacion}");

                var ExisteHabitacion = await _habitacionesRepository.GetAsync(e => e.NumDeHabitacion == dto.NumDeHabitacion);



                if (ExisteHabitacion != null)
                {
                    _logger.LogWarning($"La habitacion que sequiere registrar ya existe {dto.NumDeHabitacion}");
                    ModelState.AddModelError("Habitacion ya existente", "ya existe esta habitacion");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de la habitacion no es válida.");
                    return BadRequest(ModelState);
                }
                var nuevaHabitaicon = _mapper.Map<Habitaciones>(dto);

                await _habitacionesRepository.CreateAsycn(nuevaHabitaicon);

                _logger.LogInformation($"Nueva habitacion con el numero de habitacion '{dto.NumDeHabitacion}");
                return CreatedAtAction(nameof(GetHabitacion), new { id = nuevaHabitaicon.id }, nuevaHabitaicon);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear una nueva habitacion: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear una nueva habitacion.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutHabitacion(int id, HabitacionesUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("Los datos de entrada no son válidos o " +
                    "el ID de la deduccion no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando la habitacion con ID: {updateDto.id}");

                var existehabitaciones = await _habitacionesRepository.GetByIdAsyn(id);
                if (existehabitaciones == null)
                {
                    _logger.LogInformation($"No se encontró ninguna habitacion con este numero: {updateDto.NumDeHabitacion}");
                    return NotFound("la habitacion no existe.");
                }

                _mapper.Map(updateDto, existehabitaciones);

                await _habitacionesRepository.SaveChangesAsync();

                _logger.LogInformation($"la habitacion con el  ID {id} se actualizo correctamente.");

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _habitacionesRepository.ExistsAsync(s => s.id == id))
                {
                    _logger.LogWarning($"No se encontró ningúna habitacion con el ID: {id}");
                    return NotFound("la habitacion no se encontró durante la actualización");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar la habitacion" +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar la habitacion.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la habitacion {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar la habitacion.");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHabitacion(int id)
        {

            try
            {
                _logger.LogInformation($"Eliminando habitaciones con el ID: {id}");

                var habitacion = await _habitacionesRepository.GetByIdAsyn(id);
                if (habitacion == null)
                {
                    _logger.LogInformation($"La habitacion con el : {id} no se encontro");
                    return NotFound("habitacion no encontrado.");
                }

                await _habitacionesRepository.DeleteAsync(habitacion);

                _logger.LogInformation($"la habitacion con ID {id} eliminado correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la habitacione con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar la habitacion.");
            }
        }

    }
}
