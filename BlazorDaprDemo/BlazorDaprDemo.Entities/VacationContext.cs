using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDaprDemo.Entities
{
    public class VacationContext : DbContext
    {
        public DbSet<VacationModel> Vacations => Set<VacationModel>();        

        public VacationContext(DbContextOptions<VacationContext> options) : base(options)
        {
        }

    }
}
