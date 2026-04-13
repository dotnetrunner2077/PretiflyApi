using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class MembershipsType
{
    public int IdMemebershipsTypes { get; set; }

    public string Description { get; set; } = null!;

    public decimal Value { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<MembershipsClient> MembershipsClients { get; set; } = new List<MembershipsClient>();
}
