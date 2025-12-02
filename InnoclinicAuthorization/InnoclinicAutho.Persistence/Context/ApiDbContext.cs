using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoclinicAutho.Domain.Entities;

namespace InnoclinicAutho.Persistence.Context
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
