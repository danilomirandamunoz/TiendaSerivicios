using MediatR;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta :IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoAutor context;

            public Manejador(ContextoAutor context)
            {

                this.context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                AutorLibro autorLibro = new AutorLibro();
                autorLibro.Nombre = request.Nombre;
                autorLibro.Apellido = request.Apellido;
                autorLibro.FechaNacimiento = request.FechaNacimiento;
                autorLibro.AutorLibroGuid = Guid.NewGuid().ToString();

                await this.context.AutorLibro.AddAsync(autorLibro);
                var res = await context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el autor del libro");
            }
        }
    }
}
