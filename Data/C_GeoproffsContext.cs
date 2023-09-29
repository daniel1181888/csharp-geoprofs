using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using C_Geoproffs.Models;

namespace C_Geoproffs.Data
{
    public class C_GeoproffsContext : DbContext
    {
        public C_GeoproffsContext (DbContextOptions<C_GeoproffsContext> options)
            : base(options)
        {
        }

        public DbSet<C_Geoproffs.Models.Aanvraag> Movie { get; set; } = default!;
    }
}
