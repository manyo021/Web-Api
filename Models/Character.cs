using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; } = "CashLord";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public RpgClass Class { get; set; } = RpgClass.knight;

        public User? User { get; set; }

        public Weapon Weapon { get; set; }

        public List<Skill> Skills { get; set; }
    }
}