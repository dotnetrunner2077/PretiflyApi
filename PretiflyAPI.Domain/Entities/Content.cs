using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class Content
{
    public int IdContents { get; set; }

    public int? IdContentsTypes { get; set; }

    public string? Title { get; set; }

    public int? ReleaseYear { get; set; }

    public virtual ICollection<CategoriesXContent> CategoriesXContents { get; set; } = new List<CategoriesXContent>();

    public virtual ICollection<ContentLanguage> ContentLanguages { get; set; } = new List<ContentLanguage>();

    public virtual ICollection<ContentLocation> ContentLocations { get; set; } = new List<ContentLocation>();

    public virtual ContentsType? IdContentsTypesNavigation { get; set; }

    public virtual ICollection<Subtitle> Subtitles { get; set; } = new List<Subtitle>();

    public virtual ICollection<View> Views { get; set; } = new List<View>();
}
