using HumanoSeguros.Application.DetalleVentas;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HumanoSeguros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasEstampillasController : ControllerBase
    {

        private readonly IMediator _mediator;

        public VentasEstampillasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta([FromBody] ComandoDetalleVenta comandoDetalleVenta)
        {

            Log.Information("Metodo CrearVenta - Controlador API");
            var venta = await _mediator.Send(comandoDetalleVenta);
            
            return Created(new Uri($"/api/ventas-estampillas/{venta.IDVenta}", UriKind.Relative), venta);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVentas()
        {
            var query = new QueryDetalleVenta();
            var listaVentas = await _mediator.Send(query);
            return Ok(listaVentas);
        }



    }
}
