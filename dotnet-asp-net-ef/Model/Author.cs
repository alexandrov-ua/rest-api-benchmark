using System;
using System.Collections.Generic;

namespace AuthorsDbRest.EF.Model;

public partial class Author
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Bio { get; set; }
}
