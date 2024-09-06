using System;
using System.Collections.Generic;

namespace KioscoInformaticoBackend.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefonos { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public int? LocalidadId { get; set; }

    public virtual Localidad? Localidad { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
