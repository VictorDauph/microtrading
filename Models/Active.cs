using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using microTrading.dto;

namespace microTrading.Models;

public partial class Active
{
    
    public Active(string symbol)
    {
        Symbol = symbol;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Symbol { get; set; } = null!;
    public virtual ICollection<RunPerfomance> RunPerfomances { get; set; } = new List<RunPerfomance>();

    public virtual ICollection<ValueRecord> ValueRecords { get; set; } = new List<ValueRecord>();
}
