using PretiflyAPI.Infrastructure.Data;

namespace PretiflyAPI.Application.Common.Interfaces;

public interface IPretiflyDbContext
{
    PretiflyDbContext DbContext { get; }
}
