using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator mediator;

        public AutorController(IMediator mediator)
        {
            this.mediator = mediator;
        } 

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await this.mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorLibro>>> Get()
        {
            return await this.mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorLibro>> Get(string id)
        {
            return await this.mediator.Send(new ConsultaPorId.AutorUnico(id));
        }

    }
}
