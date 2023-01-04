using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos.Character
{
    public class AddCharacterSkillDTo
    {
        public  int  CharacterID { get; set; }
        public int SkillID { get; set; }
    }
}