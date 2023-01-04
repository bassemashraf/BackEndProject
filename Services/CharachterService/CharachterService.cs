using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Data;
using BackEnd.Dtos.Character;
using BackEnd.models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.CharachterService
{
    public class CharachterService : ICharachterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context ;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static List<Charachter> Charachters = new List<Charachter>
        {
            new Charachter() , new Charachter{ Name = "Ramy"} , new Charachter{ Name = "Ashraf" ,ID = 1 } 
        };

        public CharachterService(IMapper mapper , DataContext context  , IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserID() => int.Parse(_httpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            
            var ServiceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Charachter charachter = _mapper.Map<Charachter>(newCharacter);
            charachter.User = await _context.Users.FirstOrDefaultAsync(u => u.ID == GetUserID());
            _context.Characters.Add(charachter);
            await _context.SaveChangesAsync();
            ServiceResponse.Data = await _context.Characters
            .Where(c=> c.User.ID == GetUserID() )
            .Select( c=> _mapper.Map<GetCharacterDTO>(c))
            .ToListAsync();
            return  ServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDTO>>();
            try
            {
                 var dbCharacters = await  _context.Characters
                 .Where(c => c.User.ID == GetUserID())
                 .ToListAsync();
                 response.Data =  dbCharacters.Select( c=> _mapper.Map<GetCharacterDTO>(c)).ToList();
                
            }
            catch (System.Exception ex)
            {
                response.Succes= false;
                response.Message = ex.Message;
            }
           
            return response;
        }

        public async  Task<ServiceResponse<GetCharacterDTO>> GetCharachterByID(int id)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDTO>();
            var Charachter = _context.Characters
            .Include(c=> c.weapon)
            .Include(c=> c.Skills)
            .FirstOrDefault(c=> c.ID    == id && c.User.ID == GetUserID());
            ServiceResponse.Data =_mapper.Map<GetCharacterDTO>(Charachter);
              
            return ServiceResponse ;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpDateCharacterDTO updatedcharacter)
        {

             ServiceResponse<GetCharacterDTO> response = new ServiceResponse<GetCharacterDTO>();
             try
             {
                var Charachter = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(c=> c.ID == updatedcharacter.ID); 
                if( Charachter.User.ID == GetUserID())
                {
                    _mapper.Map(updatedcharacter,Charachter);
                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetCharacterDTO>(Charachter);
             
                }
                else {
                    response.Succes = false ;
                    response.Message = "User Not Found";
                }
                
             }
             catch ( System.Exception ex)
             {
                response.Succes = false;
                response.Message =ex.Message;
                
             }
             return response; 


             
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharcter(int ID)
        {
            ServiceResponse<List<GetCharacterDTO>> response = new ServiceResponse<List<GetCharacterDTO>>();
             try
             {
                var Charachter = await _context.Characters.FirstOrDefaultAsync(c=> c.ID == ID && c.User.ID == GetUserID());
                if (Charachter != null )
                {
                _context.Characters.Remove(Charachter);
                await _context.SaveChangesAsync();
                response.Data =  _context.Characters.
                Where(c => c.User.ID == GetUserID())
                .Select( c=> _mapper.Map<GetCharacterDTO>(c)).ToList();
                }else { 
                    response.Succes = false;
                    response.Message = "Character Not Found";
                }
             }
             catch ( System.Exception ex)
             {
                response.Succes = false;
                response.Message =ex.Message;
                
             } 
             return response; 

        }

        public async Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTo newcharacterskill)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            try
            {
                 var charachter = await _context.Characters
                 .Include(c=> c.weapon)
                 .Include(c=> c.Skills)
                 .FirstOrDefaultAsync(c=> c.ID == newcharacterskill.CharacterID &&
                 c.User.ID ==GetUserID());
                 if(charachter == null )
                 {
                    response.Succes = false;
                    response.Message ="Character Not Found";
                     return response;
                 }
                 var Skill = await _context.Skills.FirstOrDefaultAsync(s => s.ID == newcharacterskill.SkillID);
                  if(Skill == null )
                 {
                    response.Succes = false;
                    response.Message ="Skill Not Found";
                    return response;
                 }
                 charachter.Skills.Add(Skill);
                 await _context.SaveChangesAsync();
                 response.Data = _mapper.Map<GetCharacterDTO>(charachter);
                 return response;
            }
            catch (System.Exception ex)
            {
                
                response.Succes=false;
                response.Message=ex.Message;

            }
            return response;


        }
    }
}