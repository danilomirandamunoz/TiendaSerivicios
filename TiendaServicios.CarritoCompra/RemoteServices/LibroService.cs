using System.Text.Json;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteServices
{
    public class LibroService : ILibroService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger logger;

        public LibroService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<(bool, LibroRemote, string)> GetLibro(Guid libroId)
        {
            try
            {
                var cliente = httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync("libromaterial/"+libroId.ToString());
                if(response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);

                    return new (true,resultado,String.Empty);
                }

                return new (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {

                return new (false, null, ex.Message);
            }
        }
    }
}
