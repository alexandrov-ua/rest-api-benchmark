using AuthorsDbRest.EF.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthorsDbRest.EF.Controllers;

[Controller]
[Route("/api/v1")]
public class AuthorsController : Controller
{
    private readonly MyDbContext _myDbContext;

    public AuthorsController(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    [HttpGet("authors")]
    public async Task<IActionResult> GetList()
    {
        var result = await _myDbContext.Authors.Select(t => new AuthorModel()
        {
            Id = t.Id,
            Name = t.Name,
            Bio = t.Bio
        }).ToListAsync();

        return Ok(result);
    }

    [HttpGet("authors/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _myDbContext.Authors.Where(t => t.Id == id).Take(1).Select(t => new AuthorModel()
        {
            Id = t.Id,
            Name = t.Name,
            Bio = t.Bio
        }).FirstOrDefaultAsync();
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }
    
    [HttpPost("authors")]
    public async Task<IActionResult> Create([FromBody]AuthorCreateModel model)
    {
        var result = await _myDbContext.Authors.AddAsync(new Author()
        {
            Name = model.Name,
            Bio = model.Bio
        });
        await _myDbContext.SaveChangesAsync();

        return Ok(result.Entity);
    }
    
    
    [HttpDelete("authors/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _myDbContext.Authors.Where(t => t.Id == id).ExecuteDeleteAsync();
        await _myDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return Ok();
        }
        return NotFound();
    }
}

public class AuthorModel
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Bio { get; set; }
}

public class AuthorCreateModel
{
    public string Name { get; set; } = null!;
    public string? Bio { get; set; }
}