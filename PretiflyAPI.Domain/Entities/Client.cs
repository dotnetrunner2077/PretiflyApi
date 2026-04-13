using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class Client
{
    public int IdClients { get; set; }

    public int IdCountries { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual Country IdCountriesNavigation { get; set; } = null!;

    public virtual ICollection<MembershipsClient> MembershipsClients { get; set; } = new List<MembershipsClient>();

    public virtual ICollection<View> Views { get; set; } = new List<View>();
}
