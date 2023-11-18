using HumanoSeguros.Domain.Entities;

namespace HumanoSeguros.Domain.Ports
{
    public interface IDetalleVentaQueryRepository
    {
        Task<Estampilla> BuscarEstampillaPorIdAsync(Guid id);
    }
}
