using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppVendas.Models
{
    public class AppVendasContext : DbContext
    {
        public AppVendasContext (DbContextOptions<AppVendasContext> options)
            : base(options)
        {
        }

        public DbSet<Departament> Departament { get; set; }
        public DbSet<Saller> Saller{ get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
