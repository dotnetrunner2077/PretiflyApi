using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class View
{
    public int IdViews { get; set; }

    public int? IdClients { get; set; }

    public int? IdContents { get; set; }

    public DateTime? ViewDate { get; set; }

    public int? Rating { get; set; }

    public virtual Client? IdClientsNavigation { get; set; }

    public virtual Content? IdContentsNavigation { get; set; }
}
