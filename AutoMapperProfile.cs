using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.models;
using BackEnd.Dtos.Character;
using BackEnd.Dtos.Weapons;
using BackEnd.Dtos.Skill;
using BackEnd.Dtos.Fight;

namespace BackEnd
{
    public class AutoMapperProfile : Profile
    {
    
         public AutoMapperProfile()
         {
          CreateMap<Charachter,GetCharacterDTO>(); 
          CreateMap<AddCharacterDTO,Charachter>();  
          CreateMap<UpDateCharacterDTO,Charachter>();
          CreateMap<Weapon,GetWeaponDto>();
          CreateMap<Skill,GetSkillDto>();
          CreateMap<Charachter, HighscoreDto>();


         }
    }
}