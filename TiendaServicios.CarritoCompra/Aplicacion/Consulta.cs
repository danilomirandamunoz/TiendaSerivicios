using MediatR;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly ContextoCarrito contextoCarrito;
            private readonly ILibroService libroService;
            private readonly IAutorService autorService;

            public Manejador(ContextoCarrito contextoCarrito, ILibroService libroService, IAutorService autorService)
            {
                this.contextoCarrito = contextoCarrito;
                this.libroService = libroService;
                this.autorService = autorService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = this.contextoCarrito.CarritoSesion.FirstOrDefault(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoDetalle = this.contextoCarrito.CarritoSesionDetalle.Where(x => x.CarritoSesionId == carritoSesion.CarritoSesionId);

                List<CarritoDetalleDto> carritoDetalleDtos = new List<CarritoDetalleDto>();

                foreach (var item in carritoDetalle)
                {
                    var response = await libroService.GetLibro(new Guid(item.ProductoSeleccionado));
                    if (response.response)
                    {
                        var libro = response.libro;

                        var responseAutor = await autorService.GetAutor(libro.AutorLibro.Value);

                        if (responseAutor.response)
                        {
                            carritoDetalleDtos.Add(new CarritoDetalleDto
                            {
                                TituloLibro = libro.Titulo,
                                FechaPublicacion = libro.FechaPublicacion,
                                LibroId = libro.LibreriaMaterialId,
                                AutorLibro = $"{responseAutor.autor.Nombre} {responseAutor.autor.Apellido}"
                            });
                        }

                        
                    }
                }

                CarritoDto carritoDto = new CarritoDto();
                carritoDto.CarritoId = carritoSesion.CarritoSesionId;
                carritoDto.FechaCreacionSesion = carritoSesion.FechaCreacion;
                carritoDto.ListaProductos = carritoDetalleDtos;

                return carritoDto;
            }
        }
    }
}
