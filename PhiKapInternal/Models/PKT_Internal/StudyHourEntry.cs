using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PhiKapInternal.Models.PKT_Internal;

public partial class StudyHourEntry
{
    public int Id { get; set; }

    [DisplayName("Associate ID")]
    public int AssociateId { get; set; }

    [DisplayName("Proctor ID")]
    public int ProctorId { get; set; }

    public double Length { get; set; }

    public DateTime Date { get; set; }
}
