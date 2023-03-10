using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Data;
using BackEnd.Dtos.Character;
using BackEnd.Dtos.Weapons;
using BackEnd.models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context ,IHttpContextAccessor httpContextAccessor,IMapper mapper )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddweaponDTo newWeapon)
        {
            ServiceResponse<GetCharacterDTO> response = new ServiceResponse<GetCharacterDTO>();
            try
            {
                Charachter  charachter = await _context.Characters
                .FirstOrDefaultAsync(c => c.ID == newWeapon.CharachterID && 
                c.User.ID == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(charachter == null){
                    response.Succes = false;
                    response.Message = "Character not Found";
                    return response;
                }
                Weapon weapon =  new Weapon 
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Charachter = charachter                  
                }; 
                  _context.Weapons.Add(weapon);
                  await _context.SaveChangesAsync();
                  response.Data = _mapper.Map<GetCharacterDTO>(charachter);
                  
            }
            catch (System.Exception ex)
            {
                
                response.Succes=false;
                response.Message =ex.Message;
            }
            return response;
        }
    }
}