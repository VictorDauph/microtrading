using System;
using System.Collections.Generic;

namespace microTrading.Models;

public partial class RunPerfomance
{
    public int Id { get; set; }

    public DateTime? RunStart { get; set; }

    public DateTime? RunStop { get; set; }

    public int? IdActive { get; set; }

    public virtual Active? IdActiveNavigation { get; set; }

    public virtual ICollection<ValueRecord> ValueRecords { get; set; } = new List<ValueRecord>();
}
