using HumanoSeguros.Domain.Entities;
using HumanoSeguros.Domain.Ports;
using HumanoSeguros.Infraestructure.Ports;

namespace HumanoSeguros.Infraestructure.Adapters
{
    [Repository]
    public class VentaRepository : IVentaRepository
    {
        readonly IRepository<Venta> _ventaDataSource;

        public VentaRepository(IRepository<Venta> ventaDataSource)
        {
            _ventaDataSource = ventaDataSource ?? throw new ArgumentNullException(nameof(ventaDataSource));
        }

        public async Task<Venta> GuardarVentaAsync(Venta venta) => await _ventaDataSource.AddAsync(venta);
    }
}
