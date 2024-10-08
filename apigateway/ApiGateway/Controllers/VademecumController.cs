using Common.DTO;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    public class VademecumController : ControllerBase
    {
        private readonly ILogger<VademecumController> _logger;
        private readonly IVademecumServices _ivademecumService;
        public VademecumController(IVademecumServices ivademecumService, ILogger<VademecumController> logger)
        {
            _ivademecumService = ivademecumService;
            _logger = logger;
        }
        [HttpGet("GetMonodroga")]
        public async Task<IActionResult> GetVademecum([FromQuery] string? droga=null)
        {
            try
            {
                var datos = await _ivademecumService.GetVademecumBaseMonodroga(droga);
                return Ok(datos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener datos de la droga.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
        [HttpPost("VademecumBase")]
        public async Task<IActionResult> AddVademecum([FromBody] VademecumBaseRequestDto request)
        {
            try
            {
                var result = await _ivademecumService.GetVademecumbase(request);

                if (result.Success)
                {
                    return Ok(result.Medicamentos);
                }
                else
                {
                    return BadRequest(result.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el vademécum.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}