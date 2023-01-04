using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos.Character;
using BackEnd.Dtos.Weapons;
using BackEnd.models;
using BackEnd.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController :ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService  WeaponService)
        {
            _weaponService = WeaponService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> AddWeapon(AddweaponDTo newweapon)
        {return Ok (await _weaponService.AddWeapon(newweapon));

        }
    }
}