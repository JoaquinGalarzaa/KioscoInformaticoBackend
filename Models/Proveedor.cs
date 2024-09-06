using KioscoInformaticoBackend.Enums;
using System;
using System.Collections.Generic;

namespace KioscoInformaticoBackend.Models;

public partial class Proveedor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefonos { get; set; } = null!;

    public string Cbu { get; set; } = null!;

    public CondicionIvaEnum CondicionIvaEnum { get; set; }

    public int? LocalidadId { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Localidad? Localidad { get; set; }
}
