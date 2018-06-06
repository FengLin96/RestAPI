using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers.Models
{
    
        public class LibraryContext : DbContext
        {
            public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
            {
            }
            public DbSet<Planet> Planeten { get; set; }
            public DbSet<Stad> Steden { get; set; }
        }
    
}
