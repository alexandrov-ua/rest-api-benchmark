using System.Text.Json.Serialization;
using System.Collections.Generic;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
  options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var connectionString = app.Configuration.GetConnectionString("Default")!;
app.MapGet("/api/v1/authors", async () =>
    {
        using var db = new DbContext(connectionString);
        return (await db.GetAuthorList()).Select(t => new AuthorModel()
        {
            Id = t.Id,
            Name = t.Name,
            Bio = t.Bio
        });
    })
    .WithName("getList")
    .WithOpenApi();

app.MapGet("/api/v1/authors/{id}", async (long id) =>
    {
        using var db = new DbContext(connectionString);
        var t = await db.GetAuthor(id);
        if (t != null)
        {
            return Results.Ok(new AuthorModel()
            {
                Id = t.Id,
                Name = t.Name,
                Bio = t.Bio
            });
        }

        return Results.NotFound();
    })
    .WithName("getById")
    .WithOpenApi();


app.MapPost("/api/v1/authors", async (AuthorCreateModel a) =>
    {
        using var db = new DbContext(connectionString);
        var t = await db.CreateAuthor(new DbContext.Author() {Name = a.Name, Bio = a.Bio});
        return new AuthorModel()
        {
            Id = t.Id,
            Name = t.Name,
            Bio = t.Bio
        };
    })
    .WithName("create")
    .WithOpenApi();

app.MapDelete("/api/v1/authors/{id}", async (long id) =>
    {
        using var db = new DbContext(connectionString);
        var t = await db.DeleteAuthor(id);
        if (t != null)
        {
            return Results.Ok();
        }

        return Results.NotFound();
    })
    .WithName("delete")
    .WithOpenApi();

app.Run();

[JsonSerializable(typeof(AuthorModel[]))]
[JsonSerializable(typeof(IEnumerable<AuthorModel>))]
[JsonSerializable(typeof(AuthorModel))]
[JsonSerializable(typeof(AuthorCreateModel))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

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