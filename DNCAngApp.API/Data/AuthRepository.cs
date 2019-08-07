using System;
using System.Linq;
using System.Threading.Tasks;
using DNCAngApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DNCAngApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(u=>u.Username==username))
                return true;

            return false;    
        }
 
        public async Task<User> Login(string username, string password)
        {
           var user = await _context.Users.FirstOrDefaultAsync(u=>u.Username==username);
           if(user==null)
           return null;

           if(!verifyPasswordHash(password, user.PasswordHashed, user.PasswordSalt))
                return null;
            
            return user;
        }

        private bool verifyPasswordHash(string password, byte[] passwordHashed, byte[] passwordSalt)
        {
             using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {               
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i<computedHash.Length; i++)
                {
                    if(computedHash[i]!= passwordHashed[i])
                    return false;                    
                }
            }
             return true;   
            
        }

        public async Task<User> Register(User user, string password)
        {
          byte[] PasswordHash, PasswordSalt;
          GeneratePassword(password, out PasswordHash, out PasswordSalt);
          user.PasswordHashed = PasswordHash;
          user.PasswordSalt = PasswordSalt;
          await _context.Users.AddAsync(user);
          await _context.SaveChangesAsync();
          return user;
        }

        private void GeneratePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}