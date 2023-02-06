using System;
using System.Collections.Generic;

namespace todoapi.Models;

public partial class TbTodo
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}
