using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.models;
using BackEnd.Dtos.Character;
namespace BackEnd.Services.CharachterService
{

    public interface ICharachterService
    {
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters ();
        Task<ServiceResponse<GetCharacterDTO>> GetCharachterByID (int id);
        Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter);
        Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter( UpDateCharacterDTO updatedcharacter) ;
        Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharcter( int ID) ;
        Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill( AddCharacterSkillDTo newcharacterskill) ;

    }
    
}
