using Microsoft.EntityFrameworkCore;
using PretiflyAPI.Application.Common.Interfaces;
using PretiflyAPI.Domain.Entities;
using PretiflyAPI.Infrastructure.Data;

namespace PretiflyAPI.Application.Contents.Queries;

public class GetAllContentsQueryHandler : IQueryHandler<GetAllContentsQuery, List<Content>>
{
    private readonly PretiflyDbContext _context;

    public GetAllContentsQueryHandler(PretiflyDbContext context)
    {
        _context = context;
    }

    public List<Content> Handle(GetAllContentsQuery query)
    {
        return _context.Contents.ToList();
    }
}
