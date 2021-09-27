using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkersOrder.Models;

namespace WorkersOrder
{
    public class Context:DbContext
    {
        public Context()
        {

        }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Reservations> reservations { get; set; }
        public DbSet<WorkPlaces> workplaces { get; set; }
        public DbSet<Devices> devices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-L6E2SIG6\\SQLEXPRESS;Database=WorkUsers;Trusted_Connection=True;MultipleActiveResultSets=True");
            }
        }
    }
}
