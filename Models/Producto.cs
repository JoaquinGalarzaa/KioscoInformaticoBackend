using System;
using System.Collections.Generic;

namespace KioscoInformaticoBackend.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual ICollection<Detallescompra> Detallescompras { get; set; } = new List<Detallescompra>();

    public virtual ICollection<Detallesventa> Detallesventa { get; set; } = new List<Detallesventa>();
}
