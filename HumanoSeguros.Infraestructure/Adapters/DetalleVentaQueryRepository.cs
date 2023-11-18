using HumanoSeguros.Domain.Entities;
using HumanoSeguros.Domain.Ports;
using HumanoSeguros.Infraestructure.Adapters;
using HumanoSeguros.Infraestructure.DataSource;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    [Repository]
    public class DetalleVentaQueryRepository : IDetalleVentaQueryRepository
    {
        private readonly DataContext _context;
        public DetalleVentaQueryRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Estampilla> BuscarEstampillaPorIdAsync(Guid id)
        {
            var estampilla = await _context.Estampilla.FindAsync(id);
            if (estampilla == null)
            {
                throw new ArgumentException("Estampilla no encontrada.");
            }
            return estampilla;
        }
    }
}
