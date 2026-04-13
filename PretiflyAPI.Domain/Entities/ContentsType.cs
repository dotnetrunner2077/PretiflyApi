using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class ContentsType
{
    public int IdContentsTypes { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
}
