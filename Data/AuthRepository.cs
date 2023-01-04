using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public IConfiguration _Configuration ;

        public AuthRepository(DataContext context , IConfiguration configuration)
        {
            _context = context;
            _Configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
           var response = new ServiceResponse<string>();
           var user = await _context.Users
           .FirstOrDefaultAsync(u=> u.UserName.ToLower().Equals(username.ToLower()));
           if(user==null)
           {
            response.Succes= false;
            response.Message=" User Not Found";

           }
           else if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
           {
            response.Succes = false;
            response.Message = "Weong Password";
           }
           else 
           {
            response.Data = CreateToken(user);
           }
           return response;


        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response =  new ServiceResponse<int>();
            if (await USerExists(user.UserName))
            {
                response.Succes= false;
                response.Message = "User already exists";
                return response;

            }
 

            CreatePasswordHash(password,out byte[] passwordhash , out byte[] passwordsalt);
            user.PasswordHash = passwordhash;
            user.PasswordSalt = passwordsalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data= user.ID;
            return response;

        }

        public async Task<bool> USerExists(string username)
        {
            if (await _context.Users.AnyAsync( u => u.UserName.ToLower() == username.ToLower()))
            {
                return true;

            }
            return false;
        }
        private void CreatePasswordHash(string password , out byte[] passwordhash , out byte[] passwordsalt)
        {
             using (var hmac = new System.Security.Cryptography.HMACSHA512())
             {
                passwordsalt =hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

             }

        }


        private bool VerifyPasswordHash (string password ,  byte[] passwordhash ,  byte[] passwordsalt)
        {
             using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordsalt))
             {
                
                var Computhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Computhash.SequenceEqual(passwordhash);

             }

        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.ID.ToString()),
                new Claim (ClaimTypes.Name,user.UserName)

            };
             SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
             .GetBytes(_Configuration.GetSection("AppSettings:Token").Value));
             SigningCredentials creds = new SigningCredentials (key ,SecurityAlgorithms.HmacSha512Signature);
             SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
             {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
                
             };
             JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
             SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }



         
         
    }
}