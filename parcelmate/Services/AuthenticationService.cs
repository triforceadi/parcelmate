using System;
using System.Collections.Generic;
using System.Text;

namespace parcelmate.Services
{
    public class AuthenticationService
    {
        private Dictionary<string, string> userCredentials;

        public AuthenticationService()
        {
            userCredentials = new Dictionary<string, string>
            {
                { "admin", "admin" },
                { "user1", "password1" },
                { "user2", "password2" },
            };
        }

        public bool AuthenticateUser(string username, string password)
        {
            if (userCredentials.ContainsKey(username) && userCredentials[username] == password)
            {
                Console.WriteLine($"User {username} authenticated successfully.");
                return true;
            }
            else
            {
                Console.WriteLine($"Authentication failed for user {username}.");
                return false;
            }
        }
    }
}
