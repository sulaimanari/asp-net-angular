using System;
using System.Collections.Generic;

namespace to_do.Server.Models;

public partial class ToDoItem
{
    public Guid Id { get; set; }

    public string Item { get; set; } = null!;
}
