using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using to_do.Server.Models;

namespace to_do.Server.Data
{
    public class to_doServerContext : DbContext
    {
        public to_doServerContext (DbContextOptions<to_doServerContext> options)
            : base(options)
        {
        }

        public DbSet<to_do.Server.Models.ToDoItem> ToDoItem { get; set; } = default!;
    }
}
