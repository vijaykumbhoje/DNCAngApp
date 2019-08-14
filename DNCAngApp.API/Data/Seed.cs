using System.Collections.Generic;
using System.IO;
using DNCAngApp.API.Models;
using Newtonsoft.Json;

namespace DNCAngApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            var userData = File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach(var user in users)
            {
                byte[] passwordHash, passwordSalt;
                GeneratePassword("password", out passwordHash, out passwordSalt);
                user.PasswordHashed = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();
                
                _context.Users.Add(user);
                
            }
            _context.SaveChanges();
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