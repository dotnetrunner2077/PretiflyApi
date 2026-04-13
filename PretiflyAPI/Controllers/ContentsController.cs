using Microsoft.AspNetCore.Mvc;
using PretiflyAPI.Application.Contents.Queries;

namespace PretiflyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentsController : ControllerBase
{
    private readonly GetAllContentsQueryHandler _getAllContentsHandler;

    public ContentsController(GetAllContentsQueryHandler getAllContentsHandler)
    {
        _getAllContentsHandler = getAllContentsHandler;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var query = new GetAllContentsQuery();
        var contents = _getAllContentsHandler.Handle(query);
        return Ok(contents);
    }
}
