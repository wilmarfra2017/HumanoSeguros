using HumanoSeguros.Domain.Dtos;
using MediatR;

namespace HumanoSeguros.Application.DetalleVentas;

public record ComandoDetalleVenta(Guid idVenta, Guid idEstampilla, int cantVendida, double precioUnitario, double total, string idCliente) : IRequest<CrearVentaDto>;


