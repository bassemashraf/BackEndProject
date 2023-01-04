using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackEnd.models;

namespace BackEnd.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }
        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill {ID = 1 , Name = "FireBall" , Damage = 30},
                new Skill {ID = 2 , Name = "Frenzy" , Damage = 20},
                new Skill {ID = 3 , Name = "Blizzard" , Damage = 50}
            );

        }
        public DbSet<Charachter> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }

    }
}