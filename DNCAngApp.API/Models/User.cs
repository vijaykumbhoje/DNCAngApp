namespace DNCAngApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[]  PasswordHashed { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}