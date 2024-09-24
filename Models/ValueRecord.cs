using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace microTrading.Models;

public partial class ValueRecord
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ActiveId { get; set; }

    public DateTime RecordDate { get; set; }

    //close  (shift from open price)
    public decimal CloseValue { get; set; }
    public decimal OpenValue { get; set; }
    //  (shift from open price)
    public decimal HighValue { get; set; }
    // (shift from open price)
    public decimal LowValue { get; set; }

    //(High+Low+Close)/3
    public decimal TypicalPrice { get; set; }

    //(High+Low+2×Close)/4
    public decimal WeightedClosePrice { get; set; }

    //(High+Low)/2
    public decimal MedianPrice { get; set; }

    //(Open+High+Low+Close)/4
    public decimal OHLCAverage { get; set; } 
    public int RunId { get; set; } 
    public float vol { get; set; }
    public virtual Active Active { get; set; } = null!;
    public virtual RunPerfomance Run { get; set; } = null!;
}
