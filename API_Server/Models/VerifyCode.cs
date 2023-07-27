using System;

namespace API_Server.Models
{
    public class VerifyCode
    {
        public int Owner { get; set; } // User's Id
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiredTime { get; set; }

        public VerifyCode(int userId)
        {
            Random random = new Random();

            Owner = userId;
            Code = new string(Enumerable.Repeat("0123456789", 6).Select(s => s[random.Next(s.Length)]).ToArray());
            ExpiredTime = DateTime.Now.AddMinutes(1);
        }
    }
}
