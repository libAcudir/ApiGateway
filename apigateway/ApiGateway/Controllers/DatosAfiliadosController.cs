using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatosAfiliadosController : ControllerBase
    {
        private readonly ILogger<DatosAfiliadosController> _logger;
        private readonly IGatewayServices _igatewayService;
        public DatosAfiliadosController(IGatewayServices igatewayService, ILogger<DatosAfiliadosController> logger)
        {
            _igatewayService = igatewayService;
            _logger = logger;

        }
        [HttpGet("Datos-Afiliados")]
        public async Task<IActionResult> GetDatosAfiliados([FromQuery] int? contratoId = null, [FromQuery] string? nombre = null, [FromQuery] string? nro = null, [FromQuery] string? dni = null, [FromQuery] bool activo = true, [FromQuery] bool buscarOnline = true)
        {
            try
            {
                if (contratoId == null && nombre == null && nro == null && dni == null)
                {
                    return BadRequest("Debe in al menogresar al menos uno de los parámetros: contrato, nombre, nro o dni.");
                }
                var datos = await _igatewayService.GetDatosAfiliados(contratoId, nombre, nro, dni, activo, buscarOnline);
                return Ok(datos);
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error al obtener datos afiliatorios desde el API Gateway.");
                return StatusCode(502, "Error en el servidor del API Gateway.");
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al procesar la respuesta del servidor.");
                return StatusCode(500, "Error al procesar los datos recibidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener datos afiliatorios.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
