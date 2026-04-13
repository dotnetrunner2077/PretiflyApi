using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class Category
{
    public int IdCategories { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<CategoriesXContent> CategoriesXContents { get; set; } = new List<CategoriesXContent>();
}
