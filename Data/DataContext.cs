using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_Api.Models;

namespace Web_Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }



        //Seeding Data into table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
             new Skill { Id = 1, Name = "Fireball", Damage = 30 },
             new Skill { Id = 2, Name = "Electricity", Damage = 40 },
             new Skill { Id = 3, Name = "Domination", Damage = 10 }
            );
        }


        //Ability to query and save rpg characters name of the database is Characters
        public DbSet<Character> Characters { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Weapon> Weapons { get; set; }

        public DbSet<Skill> Skills { get; set; }

    }
}