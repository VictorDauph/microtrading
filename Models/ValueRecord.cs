using System;
using System.Collections.Generic;

namespace microTrading.Models;

public partial class ValueRecord
{
    public int Id { get; set; }

    public int ActiveId { get; set; }

    public DateTime RecordDate { get; set; }

    public decimal Value { get; set; }

    public int RunId { get; set; }

    public virtual Active Active { get; set; } = null!;

    public virtual RunPerfomance Run { get; set; } = null!;
}
