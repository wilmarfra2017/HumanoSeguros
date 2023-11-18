

using HumanoSeguros.Domain.Entities;

namespace HumanoSeguros.Domain.Ports
{
    public interface IDetalleVentaRepository
    {
        Task<DetalleVenta> GuardarDetalleVentaAsync(DetalleVenta detalleVenta);

        Task ActualizarEstadoEstampillaAsync(Estampilla estampilla);
    }
}
