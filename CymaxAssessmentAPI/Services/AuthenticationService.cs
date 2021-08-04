using CymaxAssessmentAPI.Enums;
using CymaxAssessmentAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CymaxAssessmentAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private string key;

        // Create this simulating users table data from datastore for the sake of time and simplicity for this assessment.
        private readonly List<UserCredentials> users = new List<UserCredentials>();

        public AuthenticationService(string key)
        {
            this.key = key;

            // Populating "Users table" with some data.
            users.Add(new UserCredentials{ EndPointId = (int)AvailableEndpoints.API1, Username = "userAPI1", Password = "passwordAPI1" });
            users.Add(new UserCredentials { EndPointId = (int)AvailableEndpoints.API2, Username = "userAPI2", Password = "passwordAPI2" });
            users.Add(new UserCredentials { EndPointId = (int)AvailableEndpoints.API3, Username = "userAPI3", Password = "passwordAPI3" });

            // Generic data for testing purpose
            users.Add(new UserCredentials { EndPointId = 0, Username = "string", Password = "string" });
        }

        /// <summary>
        /// JWT Authentication Method
        /// Validades username and password to return a valid token.
        /// Probably not the fanciest method, but I would defenitely take a deeper look on how this could be improved.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Authenticate(UserCredentials userCredentials)
        {
            if (!users.Any( u => u.EndPointId == userCredentials.EndPointId && u.Username == userCredentials.Username && u.Password == userCredentials.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userCredentials.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
