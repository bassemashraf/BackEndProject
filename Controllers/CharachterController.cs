using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackEnd.models;
using BackEnd.Services.CharachterService;
using BackEnd.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [Authorize]
    [ApiController]  
    [Route("api/[controller]")]
    
    public class CharachterController : ControllerBase
        

    { 
        private readonly ICharachterService characterservice;
        public CharachterController( ICharachterService characterservice)
        {
            this.characterservice = characterservice;
            
        }
 

        [HttpGet("{ID}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetCharacterbyid( int ID )
        {
            return Ok(await this.characterservice.GetCharachterByID(ID));
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
          
            return Ok(await this.characterservice.GetAllCharacters());
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> CrerateCharacter (AddCharacterDTO NewCharachter )
        {
            
            return Ok(await  this.characterservice.AddCharacter(NewCharachter));

        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> UpdateCharacter (UpDateCharacterDTO updatedcharacter)
        {
            return Ok(await this.characterservice.UpdateCharacter(updatedcharacter));
        } 
         [HttpDelete("{ID}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> Delete ( int ID )
        {
            var response = await this.characterservice.DeleteCharcter(ID);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> AddCharacterSkill ( AddCharacterSkillDTo newcharacterskill )
        {
            return Ok (await this.characterservice.AddCharacterSkill(newcharacterskill));
            
        }




    }
}