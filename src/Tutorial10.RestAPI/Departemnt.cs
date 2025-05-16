using System;
using System.Collections.Generic;
using Tutorial10.RestAPI.DTO;

namespace Tutorial10.RestAPI;

public partial class Departemnt
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual IEnumerable<Employee>? Employees { get; set; } = new List<Employee>();
}
