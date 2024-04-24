using System;
using System.Collections.Generic;

namespace Mobile5.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<Transport> TransportArrivalCityNavigations { get; set; } = new List<Transport>();

    public virtual ICollection<Transport> TransportDepartureCityNavigations { get; set; } = new List<Transport>();
}
