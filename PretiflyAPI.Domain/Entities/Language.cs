using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class Language
{
    public int IdLanguage { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ContentLanguage> ContentLanguages { get; set; } = new List<ContentLanguage>();

    public virtual ICollection<Subtitle> Subtitles { get; set; } = new List<Subtitle>();
}
