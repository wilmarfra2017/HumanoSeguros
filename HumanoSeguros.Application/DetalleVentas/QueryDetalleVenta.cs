using HumanoSeguros.Domain.Dtos;
using MediatR;

namespace HumanoSeguros.Application.DetalleVentas;

public record QueryDetalleVenta() : IRequest<IEnumerable<CrearVentaDto>>;
