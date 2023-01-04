using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos.Character;
using BackEnd.Dtos.Weapons;
using BackEnd.models;

namespace BackEnd.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddweaponDTo newWeapon);
    }
}