using System.Text.Json;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteServices
{
    public class AutorService : IAutorService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger logger;

        public AutorService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<(bool, AutorRemote, string)> GetAutor(Guid autorId)
        {
            try
            {
                var cliente = httpClient.CreateClient("Autores");
                var response = await cliente.GetAsync("api/Autor/"+autorId.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<AutorRemote>(contenido, options);

                    return new(true, resultado, String.Empty);
                }

                return new(false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {

                return new(false, null, ex.Message);
            }
        }
    }
}