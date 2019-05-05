using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GroceryDistibution.BLL.Authentication
{
    public class Salt
    {
        public static string Create()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
