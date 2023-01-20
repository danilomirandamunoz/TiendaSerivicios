using Microsoft.EntityFrameworkCore;
using TiendaServicios.CarritoCompra.Modelo;

namespace TiendaServicios.CarritoCompra.Persistencia
{
    public class ContextoCarrito : DbContext
    {
        public ContextoCarrito(DbContextOptions<ContextoCarrito> options) : base(options)
        {

        }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }

    }
}
