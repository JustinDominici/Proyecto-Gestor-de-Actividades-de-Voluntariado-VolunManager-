using System.Net.Http.Json;
using System.Text.Json;
using VolunManager.Client.Models;

namespace VolunManager.Client.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public Task<ServiceResult<List<VoluntarioDto>>> GetVoluntariosAsync()
        => GetAsync<List<VoluntarioDto>>("api/Voluntarios");

    public Task<ServiceResult<List<JornadaDto>>> GetJornadasAsync()
        => GetAsync<List<JornadaDto>>("api/Jornadas");

    public Task<ServiceResult<ReporteHorasDto>> GetReporteHorasAsync(int id)
        => GetAsync<ReporteHorasDto>($"api/Reportes/horas/{id}");

    public Task<ServiceResult<ReporteHorasDto>> GetReporteHorasAsync(int id, DateTime inicio, DateTime fin)
        => GetAsync<ReporteHorasDto>($"api/Reportes/horas/{id}/rango?fechaInicio={inicio:yyyy-MM-dd}&fechaFin={fin:yyyy-MM-dd}");

    public Task<ServiceResult<ReporteAsistenciaDto>> GetReporteAsistenciaAsync(int id)
        => GetAsync<ReporteAsistenciaDto>($"api/Reportes/asistencia/{id}");

    public Task<ServiceResult<ReporteAsistenciaDto>> GetReporteAsistenciaAsync(int id, DateTime inicio, DateTime fin)
        => GetAsync<ReporteAsistenciaDto>($"api/Reportes/asistencia/{id}/rango?fechaInicio={inicio:yyyy-MM-dd}&fechaFin={fin:yyyy-MM-dd}");

    private async Task<ServiceResult<T>> GetAsync<T>(string url)
    {
        try
        {
            var response = await _httpClient.GetAsync(url);
            var result = await response.Content.ReadFromJsonAsync<ServiceResult<T>>(Options);
            return result ?? new ServiceResult<T> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }
        catch (Exception ex)
        {
            return new ServiceResult<T> { Success = false, Message = $"No fue posible conectar con la API: {ex.Message}" };
        }
    }
}
