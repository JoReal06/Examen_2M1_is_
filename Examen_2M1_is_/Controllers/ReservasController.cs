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
        private readonly ILogger<ReservasController> _logger;
        private readonly IMapper _mapper;

        public ReservasController(IReservasRepository reservasRepository, ILogger<ReservasController> logger, IMapper mapper)
        {
            _reservasRepository = reservasRepository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservasDto>>> GetReservacion()
        {
            try
            {
                _logger.LogInformation("Obteniendo las Reservaciones presentes");

                var reservaciones = await _reservasRepository.GetAllAsync();

                if (reservaciones == null)
                {
                    _logger.LogError("no hay ninguna reservacion registrada");
                    return NotFound("no hay ninguna reservacion registrada actualmente");
                }


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
        public async Task<ActionResult<ReservasDto>> GEtReserva(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogError($"ID de la reserva no es válido: {id}");
                    return BadRequest("ID de la reserva no es válido");
                }


                _logger.LogInformation("Obteniendo la reserva con el " + id);

                var rerva = await _reservasRepository.GetByIdAsync(id);
                if (rerva == null)
                {
                    _logger.LogWarning($"No se encontró ninguna reserva con este ID: {id}");
                    return NotFound("reserva no encontrada.");
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
        public async Task<ActionResult<ReservasDto>> PostReservacion(ReservasCreateDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("Se recibió una reservacion nula en la solicitud.");
                return BadRequest("la reserva no puede ser nula.");
            }

            try
            {
                _logger.LogInformation($"Creando una nueva reservacion con habitacion id: {dto.habitacionId}");

                var ExisteREserva = await _reservasRepository.GetAsync(e => e.habitacionId == dto.habitacionId);



                if (ExisteREserva != null)
                {
                    _logger.LogWarning($"La reserva que quiere registrar ya existe con esta id de habitaicon{dto.habitacionId}");
                    ModelState.AddModelError("reserva ya existente", "ya existe esta reserva");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de la reserva no es válida.");
                    return BadRequest(ModelState);
                }
                var reserva = _mapper.Map<Reservas>(dto);

                await _reservasRepository.CreateAsycn(reserva);

                _logger.LogInformation($"Nueva reserva con el id de habitacion '{dto.habitacionId}");
                return CreatedAtAction(nameof(GEtReserva), new { id = reserva }, reserva);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear una nueva reserva: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear una nueva reserva.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<ReservasDto>> PutReserva(int id, ReservasUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("Los datos de entrada no son válidos o " +
                    "el ID de la reserva no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando la reserva con ID: {updateDto.id}");

                var existeReserva = await _reservasRepository.GetByIdAsync(id);
                if (existeReserva == null)
                {
                    _logger.LogInformation($"No se encontró ninguna reserva con este id: {updateDto.id}");
                    return NotFound("la reserva no existe.");
                }

                _mapper.Map(updateDto, existeReserva);

                await _reservasRepository.SaveChangesAsync();

                _logger.LogInformation($"la reserva con el  ID {id} se actualizo correctamente.");

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _reservasRepository.ExistsAsync(s => s.id == id))
                {
                    _logger.LogWarning($"No se encontró ningúna reserva con el ID: {id}");
                    return NotFound("la reserva no se encontró durante la actualización");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar la reserva" +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar la reserva.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la reserva {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar la reserva.");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> deleeReserva(int id)
        {

            try
            {
                _logger.LogInformation($"Eliminando reserva con el ID: {id}");

                var reserva = await _reservasRepository.GetByIdAsync(id);
                if (reserva == null)
                {
                    _logger.LogInformation($"La reserva con el : {id} no se encontro");
                    return NotFound("reserva no encontrado.");
                }

                await _reservasRepository.DeleteAsync(reserva);

                _logger.LogInformation($"la reserva con ID {id} eliminado correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la reserva con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar la reserva.");
            }
        }

    }
}
