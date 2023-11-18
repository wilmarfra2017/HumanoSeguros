using HumanoSeguros.Domain.Dtos;

namespace HumanoSeguros.Domain.Ports
{
    public interface IVentasQueryRepository
    {
        Task<IEnumerable<CrearVentaDto>> ConsultarVentasDetallesAsync();
    }
}
