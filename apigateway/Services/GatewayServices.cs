using Common.DTO;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Services
{
    public class GatewayServices : IGatewayServices
    {
        private readonly string _baseUrl;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<GatewayServices> _logger;
        public GatewayServices(HttpClient httpClient, ILogger<GatewayServices> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _baseUrl = _configuration["BaseUrl"];
        }
        public async Task<ResponseDatosAfiliadosDTO> GetDatosAfiliados(int? contratoId = null, string? nombre = null, string? nro = null, string? dni = null, bool activo = true, bool buscarOnline = true)
        {
            var responseDto = new ResponseDatosAfiliadosDTO();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(SeteoUrlParameters(contratoId, nombre, nro, dni, activo, buscarOnline));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                var datosAfiliatorios = JsonSerializer.Deserialize<IEnumerable<DatosAfiliatoriosDto>>(responseBody, options);

                if (datosAfiliatorios != null && datosAfiliatorios.Any())
                {
                    responseDto.Success = true;
                    responseDto.ListDatos = datosAfiliatorios;
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Error = "No se encontraron datos afiliatorios.";
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
                _logger.LogError(ex, "Error desconocido al obtener los datos del padrón.");
                responseDto.Success = false;
                responseDto.Error = "Error desconocido al obtener los datos del padrón.";
            }

            return responseDto;
        }
        public string SeteoUrlParameters(int? contratoId = null, string? nombre = null, string? nro = null, string? dni = null, bool activo = true, bool buscarOnline = true)
        {
            try
            {
                // Construir los parámetros de la consulta
                var queryParams = new List<string>
                {
                    $"contratoId={Uri.EscapeDataString(contratoId.ToString())}",
                    $"nombre={Uri.EscapeDataString(nombre ?? string.Empty)}",
                    $"nro={Uri.EscapeDataString(nro ?? string.Empty)}",
                    $"dni={Uri.EscapeDataString(dni ?? string.Empty)}",
                    $"activo={activo.ToString().ToLower()}",
                    $"buscarOnline={buscarOnline.ToString().ToLower()}"
                };
                var queryString = string.Join("&", queryParams);
                return $"{_baseUrl}?{queryString}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al armar los parametros");
                throw;
            }
        }
    }
}
