using Common.DTO;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;

namespace Services
{
    public class PrescripcionesMedicasServices : IPrescripcionesServices
    {
        private readonly string? _urlReceta;
        private readonly string? _url_Receta_Pdf;
        private readonly string? _urlDetalle;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<GatewayServices> _logger;
        public PrescripcionesMedicasServices(HttpClient httpClient, ILogger<GatewayServices> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _urlReceta = _configuration["Url_Receta"];
            _urlDetalle = _configuration["Url_Detalle"];
            _url_Receta_Pdf = _configuration["Url_Receta_Pdf"];
        }
        public async Task<ResponsePrescripcionDTO> GetPrescripciones(Guid documentTransactionId)
        {
            var responseDto = new ResponsePrescripcionDTO();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(SeteoUrlParameters_Get(_urlReceta, documentTransactionId));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                var datosPrescripcion = JsonSerializer.Deserialize<IEnumerable<PrescripcionesMedicasDTO>>(responseBody, options);
                if (datosPrescripcion != null && datosPrescripcion.Any())
                {
                    responseDto.Success = true;
                    responseDto.ListDatos = datosPrescripcion;
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Error = "No se encontro una prescripcion medica con ese id.";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error al realizar la solicitud HTTP.");
                responseDto.Success = false;
                responseDto.Error = "Error al realizar la solicitud HTTP.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error desconocido al obtener los datos");
                responseDto.Success = false;
                responseDto.Error = "Error desconocido al obtener los datos";
            }

            return responseDto;
        }
        public async Task<ResponseDetalleDTO> GetPrescripcionesDetalle(Guid documentTransactionId)
        {
            var responseDto = new ResponseDetalleDTO();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(SeteoUrlParameters_Get(_urlDetalle, documentTransactionId));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                var datosPrescripcion = JsonSerializer.Deserialize<IEnumerable<PrescripcionDetalleDto>>(responseBody, options);
                if (datosPrescripcion != null && datosPrescripcion.Any())
                {
                    responseDto.Success = true;
                    responseDto.ListDatos = datosPrescripcion;
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Error = "No se encontro el detalle de la prescripcion.";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error al realizar la solicitud HTTP.");
                responseDto.Success = false;
                responseDto.Error = "Error al realizar la solicitud HTTP.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error desconocido el detalle de la prescripcion.");
                responseDto.Success = false;
                responseDto.Error = "Error desconocido el detalle de la prescripcion.";
            }

            return responseDto;
        }
        public async Task<ResponsePrescripcionDto> UpdatePrescripcion(PrescripcionDto prescripcionDto)
        {
            var responseDto = new ResponsePrescripcionDto();
            try
            {
                var jsonContent = JsonSerializer.Serialize(prescripcionDto, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync(_urlReceta, content);
                if (response.IsSuccessStatusCode)
                {
                    responseDto.Success = true;
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Error = $"Error al actualizar la prescripción. Código de estado: {response.StatusCode}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error al realizar la solicitud HTTP.");
                responseDto.Success = false;
                responseDto.Error = "Error al realizar la solicitud HTTP.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error desconocido al actualizar la prescripción.");
                responseDto.Success = false;
                responseDto.Error = "Error desconocido al actualizar la prescripción.";
            }
            return responseDto;
        }
        public async Task<ResponsePrescripcionDto> DeletePrescripcion(int prescripcionId)
        {
            var responseDto = new ResponsePrescripcionDto();
            try
            {
                var url = $"{_urlReceta}/{prescripcionId}";
                HttpResponseMessage response = await _httpClient.DeleteAsync(_urlReceta);
                if (response.IsSuccessStatusCode)
                {
                    responseDto.Success = true;
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Error = $"Error al eliminar la prescripción. Código de estado: {response.StatusCode}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error al realizar la solicitud HTTP.");
                responseDto.Success = false;
                responseDto.Error = "Error al realizar la solicitud HTTP.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error desconocido al eliminar la prescripción.");
                responseDto.Success = false;
                responseDto.Error = "Error desconocido al eliminar la prescripción.";
            }
            return responseDto;
        }
        public string SeteoUrlParameters_Get(string? url, Guid documentTransactionId)
        {
            try
            {
                return $"{url}/{documentTransactionId}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al armar los parametros Receta");
                throw;
            }
        }
        public async Task<ResponsePrescripcionDto> AddPrescripcion(PrescripcionDto prescripcionDto)
        {
            var responseDto = new ResponsePrescripcionDto();
            try
            {
                var jsonContent = JsonSerializer.Serialize(prescripcionDto, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_urlReceta, content);

                if (response.IsSuccessStatusCode)
                {
                    responseDto.Success = true;
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Error = $"Error al generar la prescripción. Código de estado: {response.StatusCode}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error al realizar la solicitud HTTP.");
                responseDto.Success = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error desconocido al generar la prescripción.");
                responseDto.Success = false;
            }
            return responseDto;
        }
        public async Task<ResponseFileDto> GetPrescripcionPdf(RequestPrescripcionPdfDto prescripcionPdfDto)
        {
            var responseDto = new ResponseFileDto();
            try
            {
                var jsonContent = JsonSerializer.Serialize(prescripcionPdfDto, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_url_Receta_Pdf, content);

                if (!response.IsSuccessStatusCode)
                {
                    responseDto.Success = false;
                    responseDto.Error = "Error en la respuesta del servidor.";
                    return responseDto;
                }
                if (response.Content.Headers.ContentType.MediaType == "application/pdf")
                {
                    var fileBytes = await response.Content.ReadAsByteArrayAsync(); // Obtener el archivo como un arreglo de bytes
                    responseDto.Success = true;
                    responseDto.ResponseDtos = new List<FileResponseDto>
            {
                new FileResponseDto
                {
                    FileBytes = fileBytes
                }
            };
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Error = "El formato de la respuesta no es el esperado.";
                }
            }
            catch (Exception ex)
            {
                responseDto.Success = false;
                responseDto.Error = $"Excepción: {ex.Message}";
            }
            return responseDto;
        }
    }
}
