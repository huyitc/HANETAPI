using HANNET.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HANNET.Data.Context
{
    public class HanNetContext : DbContext
    {
        public HanNetContext()
        {
        }
        public HanNetContext(DbContextOptions<HanNetContext> options): base(options)
        {

        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PersonImage> PersonImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
