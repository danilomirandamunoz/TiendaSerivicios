using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaPorId
    {
        public class AutorUnico : IRequest<AutorLibro>
        {
            public AutorUnico()
            {

            }
            public AutorUnico(string guid)
            {
                this.AutorLibroGuid = guid;
            }
            public string AutorLibroGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorLibro>
        {
            private readonly ContextoAutor contextoAutor;

            public Manejador(ContextoAutor contextoAutor)
            {
                this.contextoAutor = contextoAutor;
            }
            public async Task<AutorLibro> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await this.contextoAutor.AutorLibro.FirstOrDefaultAsync(x=>x.AutorLibroGuid == request.AutorLibroGuid);

                if(autor == null)
                {
                    throw new Exception("No se ha encontrado el autor");
                }

                return autor;
            }
        }
    }
}
