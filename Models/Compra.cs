using KioscoInformaticoBackend.Enums;
using System;
using System.Collections.Generic;

namespace KioscoInformaticoBackend.Models;

public partial class Compra
{
    public int Id { get; set; }

    public FormaDePagoEnum  FormaDePago { get; set; }

    public int Iva { get; set; }

    public int Total { get; set; }

    public DateTime Fecha { get; set; }

    public int? ProveedorId { get; set; }

    public virtual ICollection<Detallescompra> Detallescompras { get; set; } = new List<Detallescompra>();

    public virtual Proveedor? Proveedor { get; set; }
}
