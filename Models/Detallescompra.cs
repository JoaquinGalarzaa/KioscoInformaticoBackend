using System;
using System.Collections.Generic;

namespace KioscoInformaticoBackend.Models;

public partial class Detallescompra
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public decimal PrecioUnitario { get; set; }

    public int Cantidad { get; set; }

    public int CompraId { get; set; }

    public virtual Compra Compra { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
