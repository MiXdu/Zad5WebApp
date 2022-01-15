using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6Api.Database.Entities;

namespace WebApplication6Api.Database
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Parcel> Parcels { get; set; }

        public ApiDbContext(DbContextOptions options) : base(options)
        {
            
        }

    }
}

