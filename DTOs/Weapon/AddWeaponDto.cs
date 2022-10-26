using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.DTOs.Weapon
{
    public class AddWeaponDto
    {
        public string Name { get; set; }

        public int Damage { get; set; }

        public int CharacterID { get; set; }
    }
}