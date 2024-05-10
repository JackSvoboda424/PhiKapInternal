using System;
using System.Collections.Generic;

namespace PhiKapInternal.Models.PKT_Internal;

public partial class Member
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Status { get; set; }

    public string Class { get; set; }

    public string? Position { get; set; }

    public int? Room { get; set; }
}
