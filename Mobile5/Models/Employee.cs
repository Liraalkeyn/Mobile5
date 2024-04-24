using System;
using System.Collections.Generic;

namespace Mobile5.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronimyc { get; set; } = null!;

    public string TelephoneNumber { get; set; } = null!;

    public string EMail { get; set; } = null!;

    public int PositionId { get; set; }

    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}
