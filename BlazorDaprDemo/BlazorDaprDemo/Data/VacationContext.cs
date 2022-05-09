using BlazorServerSide.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Data
{
    public class VacationContext : DbContext
    {
        public DbSet<VacationModel> Vacations { get; set; }

        public VacationContext(DbContextOptions<VacationContext> options) : base(options)
        {
        }

    }
}
