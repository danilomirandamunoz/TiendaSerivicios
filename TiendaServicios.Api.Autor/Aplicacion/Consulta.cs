using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorLibro>>
        {

        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorLibro>>
        {
            private readonly ContextoAutor contextoAutor;

            public Manejador(ContextoAutor contextoAutor)
            {
                this.contextoAutor = contextoAutor;
            }
            public async Task<List<AutorLibro>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                return await this.contextoAutor.AutorLibro.ToListAsync();
            }
        }           
    }
}
    