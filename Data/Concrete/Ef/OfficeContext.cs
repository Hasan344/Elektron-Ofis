using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Concrete
{
    public class OfficeContext:IdentityDbContext
    {
        public DbSet<Gelir> Gelirler { get; set; }
        public DbSet<GelirTeyinati> GelirTeyinatlari { get; set; }
        public DbSet<Xerc> Xercler { get; set; }
        public DbSet<XercTeyinati> XercTeyinatlari { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"server=.\sqlexpress;database=FF;integrated security=true");
            base.OnConfiguring(builder);
        }
    }
}
