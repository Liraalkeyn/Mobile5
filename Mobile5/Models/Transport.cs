using System;
using System.Collections.Generic;

namespace Mobile5.Models;

public partial class Transport
{
    public int TransportId { get; set; }

    public int EmployeeId { get; set; }

    public int CompanyId { get; set; }

    public int DepartureCity { get; set; }

    public int ArrivalCity { get; set; }

    public int TypeCargoId { get; set; }

    public int Weight { get; set; }

    public virtual City ArrivalCityNavigation { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual City DepartureCityNavigation { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual TypeCargo TypeCargo { get; set; } = null!;
}
