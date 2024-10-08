using Common.DTO;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    public class PrescripcionMedicaController : ControllerBase
    {
        private readonly ILogger<PrescripcionMedicaController> _logger;
        private readonly IPrescripcionesServices _iprescripcionService;
        public PrescripcionMedicaController(IPrescripcionesServices iprescripcioneService, ILogger<PrescripcionMedicaController> logger)
        {
            _iprescripcionService = iprescripcioneService;
            _logger = logger;
        }
        [HttpGet("GetPrescripciones-Medicas")]
        public async Task<IActionResult> GetPrescripcionById([FromQuery] Guid documentTransactionId)
        {
            try
            {
                var datos = await _iprescripcionService.GetPrescripciones(documentTransactionId);
                return Ok(datos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener datos de la prescripcion.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
        [HttpGet("GetPrescripcionMedica-Detalle")]
        public async Task<IActionResult> GetPrescripcionDetalleById([FromQuery] Guid documentTransactionId)
        {
            try
            {
                var datos = await _iprescripcionService.GetPrescripcionesDetalle(documentTransactionId);
                return Ok(datos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener datos de la prescripcion.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
       
        [HttpPut("UpdatePrescripcion")]
        public async Task<IActionResult> UpdatePrescripcion([FromBody] PrescripcionDto prescripcionDto)
        {
            if (prescripcionDto == null)
            {
                return BadRequest("Datos de prescripción inválidos.");
            }
            try
            {
                var prescripcion = await _iprescripcionService.UpdatePrescripcion(prescripcionDto);

                if (prescripcion.Success)
                {
                    return Ok(prescripcion);
                }
                else
                {
                    return BadRequest(prescripcion.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la prescripción.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("DeletePrescripcion")]
        public async Task<IActionResult> DeletePrescripcion([FromQuery] int prescripcionId)
        {
            try
            {
                var prescripcion = await _iprescripcionService.DeletePrescripcion(prescripcionId);

                if (prescripcion.Success)
                {
                    return Ok(prescripcion);
                }
                else
                {
                    return BadRequest(prescripcion.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al querer borrar la prescripción.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost("AddPrescripcion")]
        public async Task<IActionResult> AddPrescripcion([FromBody] PrescripcionDto prescripcionDto)
        {
            if (prescripcionDto == null)
            {
                return BadRequest("Datos de prescripción inválidos.");
            }
            try
            {
                var prescripcion = await _iprescripcionService.AddPrescripcion(prescripcionDto);

                if (prescripcion.Success)
                {
                    return Ok(prescripcion);
                }
                else
                { return BadRequest(); }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la prescripción.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
        [HttpPost("AddPrescripcionPdf")]
        public async Task<IActionResult> AddPrescripcionPdf([FromBody] RequestPrescripcionPdfDto prescripcionPdfDto)
        {
            if (prescripcionPdfDto == null)
            {
                return BadRequest("Datos de prescripción inválidos.");
            }

            try
            {
                var prescripcion = await _iprescripcionService.GetPrescripcionPdf(prescripcionPdfDto);

                if (prescripcion.Success)
                {
                    var fileBytes = prescripcion.ResponseDtos.FirstOrDefault()?.FileBytes;

                    if (fileBytes == null || !fileBytes.Any())
                    {
                        return BadRequest("No se pudo obtener el archivo PDF.");
                    }

                    return File(fileBytes, "application/pdf", "prescripcion.pdf");
                }
                else
                {
                    return BadRequest(prescripcion.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pdf de la prescripción.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}