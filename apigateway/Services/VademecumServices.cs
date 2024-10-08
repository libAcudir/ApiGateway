using Common.DTO;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Services
{
    public class VademecumServices : IVademecumServices
    {
        private readonly string? _urlVademecum;
        private readonly string? _urlVademecumBaseMonodroga;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<VademecumServices> _logger;
        public VademecumServices(HttpClient httpClient, ILogger<VademecumServices> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _urlVademecum = _configuration["Url_Vademecum"];
            _urlVademecumBaseMonodroga = _configuration["Url_VademecumBaseMonodroga"];
        }

        public async Task<ResponseVademecumBaseDto> GetVademecumbase(VademecumBaseRequestDto request)
        {
            var responseDto = new ResponseVademecumBaseDto();
            try
            {
                var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_urlVademecum, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };
                var datosDroga = JsonSerializer.Deserialize<IEnumerable<VademecumBaseDto>>(responseBody, options);

                if (datosDroga != null && datosDroga.Any())
                {
                    responseDto.Success = true;
                    responseDto.Medicamentos = datosDroga.ToList();
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Error = "No se encontró una droga con ese nombre";
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

        public async Task<ResponseVademecumBaseMonodrogaDto> GetVademecumBaseMonodroga(string? droga)
        {
            var responseDto = new ResponseVademecumBaseMonodrogaDto();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(SeteoUrlParameters(_urlVademecumBaseMonodroga, droga));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                var datosDroga = JsonSerializer.Deserialize<IEnumerable<VademecumBaseMonodrogaDto>>(responseBody, options);
                if (datosDroga != null && datosDroga.Any())
                {
                    responseDto.Success = true;
                    responseDto.ListDatos = datosDroga;
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Error = "No se encontro una droga con ese nombre";
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
        public string SeteoUrlParameters(string? url, string? droga)
        {
            try
            {
                return string.IsNullOrEmpty(droga) ? url : $"{url}?droga={Uri.EscapeDataString(droga)}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al armar los parametros de la busqueda");
                throw;
            }
        }
    }
}
