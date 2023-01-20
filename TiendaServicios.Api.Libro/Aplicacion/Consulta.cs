using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<List<LibreriaMaterialDto>>
        {

        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaMaterialDto>>
        {
            private readonly ContextoLibreria contextoLibreria;
            private readonly IMapper autoMapper;

            public Manejador(ContextoLibreria contextoLibreria, IMapper autoMapper)
            {
                this.contextoLibreria = contextoLibreria;
                this.autoMapper = autoMapper;
            }

            public async Task<List<LibreriaMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await contextoLibreria.LibreriaMaterial.ToListAsync();
                var librosDto = this.autoMapper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDto>>(libros);
                return librosDto;
            }
        }
    }
}
