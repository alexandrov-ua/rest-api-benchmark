using Dapper;
using Npgsql;

public class DbContext : IDisposable
{
    private readonly NpgsqlConnection _connection;

    public DbContext(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
    }

    public async Task<IEnumerable<Author>> GetAuthorList()
    {
        return await _connection.QueryAsync<Author>("SELECT * FROM authors ORDER BY name");
    }

    public async Task<Author?> GetAuthor(Int64 id)
    {
        return await _connection.QuerySingleOrDefaultAsync<Author>("SELECT * FROM authors WHERE id = @Id LIMIT 1;",
            new {id});
    }

    public async Task<Author> CreateAuthor(Author a)
    {
        return await _connection.QueryFirstAsync<Author>(
            "INSERT INTO authors (\n  name, bio\n) VALUES (\n  @name, @bio\n)\nRETURNING *;", new
            {
                name = a.Name,
                bio = a.Bio
            });
    }

    public async Task<Int64?> DeleteAuthor(Int64 id)
    {
        return await _connection.QueryFirstOrDefaultAsync<Int64>("DELETE FROM authors\nWHERE id = @id\nRETURNING Id;",
            new
            {
                id
            });
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
    }
}