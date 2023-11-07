using Newtonsoft.Json;
using parcelmate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace parcelmate.Services
{
    public class AuthenticationService
    {
        private DataModel users;
        public AuthenticationService()
        {
        }
        public DataModel InitializeData()
        {
            try
            {
                users = JsonConvert.DeserializeObject<DataModel>("{\n  \"users\": [\n    {\n      \"username\": \"adrian\",\n      \"password\": \"admin0\",\n      \"id\": 1\n    },\n    {\n      \"username\": \"mihai\",\n      \"password\": \"admin1\",\n      \"id\": 2\n    },\n    {\n      \"username\": \"danut\",\n      \"password\": \"admin2\",\n      \"id\": 3\n    }\n  ]\n}");
            }
            catch (Exception ex)
            {
            }

            return users;
        }

        public bool AuthenticateUser(string username, string password)
        {
            try
            {
                if (users.Users.Any(user => user.Username == username && user.Password == password))
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
            catch (Exception ex)
            {

            }
            return false;

        }


        public int ReturnUserIdByUsername(string username)
        {
            var user = users.Users.FirstOrDefault(u => u.Username == username);
            return user != null ? user.Id : 0;
        }
    }
}
