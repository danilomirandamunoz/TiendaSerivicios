﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {
        private readonly IMediator mediator;

        public LibroMaterialController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibreriaMaterialDto>>> GetLibros()
        {
            return await mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaMaterialDto>> GetLibroUnico(Guid id)
        {
            return await mediator.Send(new ConsultaFiltro.LibreriaUnico { LibreriaId = id });
        }
    }
}
