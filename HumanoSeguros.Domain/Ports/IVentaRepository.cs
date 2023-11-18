using HumanoSeguros.Domain.Entities;

namespace HumanoSeguros.Domain.Ports
{
    public interface IVentaRepository
    {
        Task<Venta> GuardarVentaAsync(Venta venta);
    }
}
