using MediatR;
using TiendaServicios.CarritoCompra.Modelo;
using TiendaServicios.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }


        }

        public  class Manejador: IRequestHandler<Ejecuta>
        {
            private readonly ContextoCarrito contextoCarrito;

            public Manejador(ContextoCarrito contextoCarrito )
            {
                this.contextoCarrito = contextoCarrito;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion

                };
                contextoCarrito.CarritoSesion.Add(carritoSesion);
                var result = await contextoCarrito.SaveChangesAsync();

                if(result == 0)
                {
                    throw new Exception("No se ha podido guardar el carrito de compra");
                }

                int id = carritoSesion.CarritoSesionId;

                foreach (string item in request.ProductoLista)
                {
                    var carritoSesionDetalle = new CarritoSesionDetalle
                    {
                        ProductoSeleccionado = item,
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id
                    };

                    contextoCarrito.CarritoSesionDetalle.Add(carritoSesionDetalle);
                    
                }

                result = await contextoCarrito.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("No se ha podido guardar el detalle del carrito de compra");
                }

                return Unit.Value;
            }

        }
    }
}
