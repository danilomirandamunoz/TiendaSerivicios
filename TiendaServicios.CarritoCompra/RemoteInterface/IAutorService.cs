using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteInterface
{
    public interface IAutorService
    {
        Task<(bool response, AutorRemote? autor, string mensaje)> GetAutor(Guid autorId);
    }
}
