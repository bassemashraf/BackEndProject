using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.models;

namespace BackEnd.Dtos.Character
{
    public class UpDateCharacterDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = "Bassem";
        public int HitPoints { get; set; } = 100 ; 
        public int Strength { get; set; }  = 10 ; 
         public int Defence { get; set; } = 10 ; 
         public int Intalligence { get; set; } = 10 ; 

         public RGPClaSS Class { get; set; } = RGPClaSS.Knight;
    }
}