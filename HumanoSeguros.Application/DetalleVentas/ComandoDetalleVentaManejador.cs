using HumanoSeguros.Domain.Dtos;
using HumanoSeguros.Domain.Entities;
using HumanoSeguros.Domain.Services;
using MediatR;

namespace HumanoSeguros.Application.DetalleVentas
{
    public class ComandoDetalleVentaManejador : IRequestHandler<ComandoDetalleVenta, CrearVentaDto>        
    {
        private readonly ServicioVentaEstampilla _servicioVenta;        

        public ComandoDetalleVentaManejador(ServicioVentaEstampilla servicioVenta)
        {
            _servicioVenta = servicioVenta ?? throw new ArgumentNullException(nameof(servicioVenta));            
        }

        public async Task<CrearVentaDto> Handle(ComandoDetalleVenta request, CancellationToken cancellationToken)
        {
            ValidacionParametros(request);

            return await ExecucionRegistrarVentaAsync(request, cancellationToken);
        }

        private static void ValidacionParametros(ComandoDetalleVenta request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "El parametro request no puede ser nulo.");
            }
        }

        private async Task<CrearVentaDto> ExecucionRegistrarVentaAsync(ComandoDetalleVenta request, CancellationToken cancellationToken)
        {
            var detalleVenta = new DetalleVenta(request.idVenta, request.idEstampilla, request.cantVendida, request.precioUnitario, request.total, request.idCliente);
            var resultado = await _servicioVenta.RegistrarVentaEstampillaAsync(detalleVenta, cancellationToken);
            return resultado;
        }

    }
}
