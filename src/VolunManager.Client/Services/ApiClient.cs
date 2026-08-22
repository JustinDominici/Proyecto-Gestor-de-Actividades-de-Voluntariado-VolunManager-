using System.Net.Http.Json;
using VolunManager.Client.Models;

namespace VolunManager.Client.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<VoluntarioDto>>> GetVoluntariosAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<List<VoluntarioDto>>>("api/voluntarios");
            return result ?? new ServiceResult<List<VoluntarioDto>> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }

        public async Task<ServiceResult<List<JornadaDto>>> GetJornadasAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<List<JornadaDto>>>("api/jornadas");
            return result ?? new ServiceResult<List<JornadaDto>> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }

        public async Task<ServiceResult<ReporteHorasDto>> GetReporteHorasAsync(int voluntarioId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<ReporteHorasDto>>($"api/reportes/horas/{voluntarioId}");
            return result ?? new ServiceResult<ReporteHorasDto> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }

        public async Task<ServiceResult<ReporteHorasDto>> GetReporteHorasAsync(int voluntarioId, DateTime inicio, DateTime fin)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<ReporteHorasDto>>($"api/reportes/horas/{voluntarioId}?inicio={inicio:yyyy-MM-dd}&fin={fin:yyyy-MM-dd}");
            return result ?? new ServiceResult<ReporteHorasDto> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }

        public async Task<ServiceResult<ReporteAsistenciaDto>> GetReporteAsistenciaAsync(int jornadaId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<ReporteAsistenciaDto>>($"api/reportes/asistencia/{jornadaId}");
            return result ?? new ServiceResult<ReporteAsistenciaDto> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }

        public async Task<ServiceResult<ReporteAsistenciaDto>> GetReporteAsistenciaAsync(int jornadaId, DateTime inicio, DateTime fin)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<ReporteAsistenciaDto>>($"api/reportes/asistencia/{jornadaId}?inicio={inicio:yyyy-MM-dd}&fin={fin:yyyy-MM-dd}");
            return result ?? new ServiceResult<ReporteAsistenciaDto> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }

        // --- NUEVOS MÉTODOS CRUD ---

        public async Task<ServiceResult<VoluntarioDto>> CrearVoluntarioAsync(object voluntario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/voluntarios", voluntario);
            return await response.Content.ReadFromJsonAsync<ServiceResult<VoluntarioDto>>()
                ?? new ServiceResult<VoluntarioDto> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }

        public async Task<ServiceResult<bool>> EliminarVoluntarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/voluntarios/{id}");
            return await response.Content.ReadFromJsonAsync<ServiceResult<bool>>()
                ?? new ServiceResult<bool> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }

        public async Task<ServiceResult<JornadaDto>> CrearJornadaAsync(object jornada)
        {
            var response = await _httpClient.PostAsJsonAsync("api/jornadas", jornada);
            return await response.Content.ReadFromJsonAsync<ServiceResult<JornadaDto>>()
                ?? new ServiceResult<JornadaDto> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }

        public async Task<ServiceResult<bool>> EliminarJornadaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/jornadas/{id}");
            return await response.Content.ReadFromJsonAsync<ServiceResult<bool>>()
                ?? new ServiceResult<bool> { Success = false, Message = "La API devolvió una respuesta vacía." };
        }
    }
}