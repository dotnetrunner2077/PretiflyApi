using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class ContentLocation
{
    public int IdContentLocation { get; set; }

    public int IdContents { get; set; }

    public string Url { get; set; } = null!;

    public string? Quality { get; set; }

    public string? Format { get; set; }

    public virtual Content IdContentsNavigation { get; set; } = null!;
}
