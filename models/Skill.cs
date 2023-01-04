using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.models
{
    public class Skill
    {
        public int  ID { get; set; }
        public  string   Name  { get; set; } =string.Empty;
        public int Damage { get; set; }
        public List<Charachter> charachters { get; set; }
    }
}