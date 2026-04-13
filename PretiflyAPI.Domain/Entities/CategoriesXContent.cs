using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class CategoriesXContent
{
    public int IdCategoriesXContents { get; set; }

    public int IdCategories { get; set; }

    public int IdContents { get; set; }

    public virtual Category IdCategoriesNavigation { get; set; } = null!;

    public virtual Content IdContentsNavigation { get; set; } = null!;
}
