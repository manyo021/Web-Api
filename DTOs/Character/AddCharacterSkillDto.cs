using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.DTOs.Character
{
    public class AddCharacterSkillDto
    {
        public int CharacterId { get; set; }

        public int SkillId { get; set; }
    }
}