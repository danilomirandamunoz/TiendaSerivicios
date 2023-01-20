using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibreriaUnico : MediatR.IRequest<LibreriaMaterialDto>
        {
            public Guid? LibreriaId { get; set; }
        }

        public class Manejador : IRequestHandler<LibreriaUnico, LibreriaMaterialDto>
        {
            private readonly ContextoLibreria contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }
            public async Task<LibreriaMaterialDto> Handle(LibreriaUnico request, CancellationToken cancellationToken)
            {
                var libro = await contexto.LibreriaMaterial.FirstOrDefaultAsync(x => x.LibreriaMaterialId == request.LibreriaId);
                if (libro == null)
                {
                    throw new Exception($"No se encontro el libro con el id:{request.LibreriaId}");
                }

                var libroDto = mapper.Map<LibreriaMaterialDto>(libro);

                return libroDto;

            }
        }


    }
}
