using KioscoInformaticoBackend.Enums;
using System;
using System.Collections.Generic;

namespace KioscoInformaticoBackend.Models;

public partial class Venta
{
    public int Id { get; set; }

    public FormaDePagoEnum FormaPago { get; set; }

    public int ClienteId { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Iva { get; set; }

    public decimal Total { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<Detallesventa> Detallesventa { get; set; } = new List<Detallesventa>();
}
