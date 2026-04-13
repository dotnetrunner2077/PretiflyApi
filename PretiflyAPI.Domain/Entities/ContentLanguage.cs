using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class ContentLanguage
{
    public int IdContentLanguage { get; set; }

    public int IdContents { get; set; }

    public int IdLanguage { get; set; }

    public string? Url { get; set; }

    public virtual Content IdContentsNavigation { get; set; } = null!;

    public virtual Language IdLanguageNavigation { get; set; } = null!;
}
