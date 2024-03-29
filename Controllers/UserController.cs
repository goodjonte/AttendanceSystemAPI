﻿using AttendanceSystemAPI.Data;
using AttendanceSystemAPI.DTO;
using AttendanceSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AttendanceSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //FIELDS and PROPERTIES
        public readonly IConfiguration _configuration;
        public AttendanceSystemAPIContext Context { get; set; }

        //CONSTRUCTOR
        public UserController(IConfiguration configuration, AttendanceSystemAPIContext context)
        {
            _configuration = configuration;
            Context = context;
        }

        //METHODS
        //api/User/{id}
        [HttpGet("GetUser")]
        public User GetUserById(Guid id)
        {
            User thisUser = Context.User.First(x => x.Id == id);
            return thisUser;
        }

        //api/User/{id}
        [HttpGet("{id}")]
        public ActionResult<String> GetNameById(Guid id)
        {
            User thisUser = Context.User.First(x => x.Id == id);
            if(thisUser == null)
            {
                return "User Not Found";
            }
            return thisUser.FirstName + " " + thisUser.LastName;
        }

        //api/User/GetTeachers
        [HttpGet("GetTeachers")]
        public async Task<List<User>> GetTeachers()
        {
            return await Context.User.Where(u => u.UsersRole == UserRole.Teacher).ToListAsync();
        }

        //api/User/GetStudents
        [HttpGet("GetStudents")]
        public async Task<List<User>> GetStudents()
        {
            return await Context.User.Where(u => u.UsersRole == UserRole.Student).ToListAsync();
        }

        //api/User/register POST
        //Register User
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserCreationDTO request)
        {
            User user = new();
            if (request.UsersRole != UserRole.Student) //set user properties based on user role
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);//Takes in the password and creates the hash and salt

                user.Id = Guid.NewGuid();
                user.CanLogin = true;
                user.Email = request.Email;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                if (Context.User.Any(x => x.Email == user.Email))//Checks if the username is already taken
                {
                    return BadRequest("Email already signed up!");
                }
            }else
            {
                user.Id = Guid.NewGuid();
                user.CanLogin = false;
                user.ParentName = request.ParentName;
                user.ParentPhone = request.ParentPhone;
            }

            //set universal user properties
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UsersRole = request.UsersRole;
            Context.User.Add(user);//Adds the user to the db context
            await Context.SaveChangesAsync();//Saves the changes to the db

            return Ok(user);
        }

        //api/User/login POST
        //Login User
        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginDTO loginRequest)
        {
            bool UserExists = Context.User.Any(x => x.Email == loginRequest.Email);
            if (!UserExists)//Checks if username is valid
            {
                return BadRequest("User not found!");
            }

            var userLoggingIn = Context.User.FirstOrDefault(x => x.Email == loginRequest.Email);//Gets the user from the db
            if (userLoggingIn != null)
            {
                if (userLoggingIn.PasswordHash != null && userLoggingIn.PasswordSalt != null)
                {
                    if (!VerifyPasswordHash(loginRequest.Password, userLoggingIn.PasswordHash, userLoggingIn.PasswordSalt))//checks if password is valid(hash and salt would come from db in real project(the users row))
                    {
                        return BadRequest("Wrong Password");
                    }
                }
            }
            string token = CreateToken(loginRequest);

            return Ok(token);
        }

        //Creates the JWT token Based off the user
        private string CreateToken(UserLoginDTO tokenRequest)
        {
            User? user = Context.User.FirstOrDefault(x => x.Email == tokenRequest.Email);
            if(user == null)
            {
                return "User not found";
            }

            string userRoleString = user.UsersRole switch
            {
                UserRole.Admin => "0",
                UserRole.Teacher => "1",
                UserRole.Student => "2",
                _ => "3",
            };
            List<Claim> claims = new()
            {
                new Claim("user", user.Id.ToString()),
                new Claim("name", user.FirstName + " " + user.LastName),
                new Claim("Role", userRoleString)
            };

            var keyToken = _configuration.GetSection("AppSettings:Token").Value;
            if (keyToken != null)
            {
                //Creates the key from the token created in appsettings (also turns it into a byte array first)
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyToken));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //creats the creds (pretty much just signature to the key)

                var token = new JwtSecurityToken( //Initialisez the JWT token (putting everything together)
                    issuer: "AttendanceSystem",
                    audience: "AttendanceSystem",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),//Here is the token expiry (Currently just adding 24hours from creation)
                    signingCredentials: cred
                    );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token); //Actualty writes the token now it can be passed to the client

                return jwt;
            }
            return "No KeyToken in Appsettings";
        }

        //creates password hash and salt sets the out params to the created hash and salt (would need to return in actual example and add to db context)
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        //Creates a Hash from the entered password then compares it to the existing hash (Would be from db in real example)
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
