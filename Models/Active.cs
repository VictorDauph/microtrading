using System;
using System.Collections.Generic;

namespace microTrading.Models;

public partial class Active
{
    public int Id { get; set; }

    public string Symbol { get; set; } = null!;

    public virtual ICollection<RunPerfomance> RunPerfomances { get; set; } = new List<RunPerfomance>();

    public virtual ICollection<ValueRecord> ValueRecords { get; set; } = new List<ValueRecord>();
}
