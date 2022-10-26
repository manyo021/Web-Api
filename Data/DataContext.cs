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

        //Ability to query and save rpg characters name of the database is Characters
        public DbSet<Character> Characters { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Weapon> Weapons { get; set; }

    }
}