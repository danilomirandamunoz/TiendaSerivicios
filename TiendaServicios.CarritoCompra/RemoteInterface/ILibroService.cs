using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool response, LibroRemote? libro, string mensaje)> GetLibro(Guid libroId);
    }
}
