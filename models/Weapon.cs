using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.models
{
    public class Weapon
    {
        public  int  ID { get; set; }
        public string  Name { get; set; }
        public int  Damage { get; set; }
        public Charachter Charachter { get; set; }
        public int CharachterID { get; set; }
    }
}