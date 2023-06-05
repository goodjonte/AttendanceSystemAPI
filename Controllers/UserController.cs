using AttendanceSystemAPI.Data;
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
        public static User user = new User();
        public readonly IConfiguration _configuration;
        public AttendanceSystemAPIContext _context { get; set; }

        //CONSTRUCTOR
        public UserController(IConfiguration configuration, AttendanceSystemAPIContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        //METHODS
        //api/User/register POST
        //Register User
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserCreationDTO request)
        {
            if (request.UsersRole != UserRole.Student) //set user properties based on user role
            {
                if (request.Email == "" || request.Password == "" || request.Email == null || request.Password == null)//Checks if the username or password is null
                {
                    return BadRequest("Please enter both a Email and Password when creating a Teacer Account");
                }
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);//Takes in the password and creates the hash and salt

                user.Id = Guid.NewGuid();
                user.CanLogin = true;
                user.Email = request.Email;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                if (_context.User.Any(x => x.Email == user.Email))//Checks if the username is already taken
                {
                    return BadRequest("Email already signed up!");
                }
            }else
            {
                user.Id = Guid.NewGuid();
                user.CanLogin = false;
                user.ParentName = request.ParentName;

            }

            //set universal user properties
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.SchoolId = request.SchoolId;
            user.UsersRole = request.UsersRole;
            _context.User.Add(user);//Adds the user to the db context
            await _context.SaveChangesAsync();//Saves the changes to the db

            return Ok(user);

        }

        //api/User/login POST
        //Login User
        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginDTO loginRequest)
        {
            bool UserExists = _context.User.Any(x => x.Email == loginRequest.Email);
            if (UserExists == false)//Checks if username is valid
            {
                return BadRequest("User not found!");
            }

            var userLoggingIn = _context.User.FirstOrDefault(x => x.Email == loginRequest.Email);//Gets the user from the db
            if (userLoggingIn != null)
            {
                if (!VerifyPasswordHash(loginRequest.Password, userLoggingIn.PasswordHash, userLoggingIn.PasswordSalt))//checks if password is valid(hash and salt would come from db in real project(the users row))
                {
                    return BadRequest("Wrong Password");
                }
            }
            string token = CreateToken(loginRequest);

            return Ok(token);
        }

        //Creates the JWT token Based off the user
        private string CreateToken(UserLoginDTO tokenRequest)
        {
            UserRole userRole;
            string userRoleString;
            User user = _context.User.FirstOrDefault(x => x.Email == tokenRequest.Email);
            switch (user.UsersRole)
            {
                case UserRole.Admin:
                    userRoleString = "0";
                    break;
                case UserRole.Teacher:
                    userRoleString = "1";
                    break;
                default:
                    userRoleString = "2";
                    break;
            }





            List<Claim> claims = new List<Claim>//Creates claims to assign to the jwt token 
            {
                new Claim("user", user.Id.ToString()),
                new Claim("Role", userRoleString)
            };

            var keyToken = _configuration.GetSection("AppSettings:Token").Value;
            if (keyToken != null)
            {
                //Creates the key from the token created in appsettings (also turns it into a byte array first)
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyToken));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //creats the creds (pretty much just signature to the key)

                var token = new JwtSecurityToken( //Initialisez the JWT token (putting everything together)
                    claims: claims,
                    issuer: "AttendanceSystem",
                    audience: "AttendanceSystem",
                    expires: DateTime.Now.AddDays(1),//Here is the token expiry (Currently just adding 24hours from creation)
                    signingCredentials: cred
                    );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token); //Actualty writes the token now it can be passed to the client

                return jwt;
            }
            return "No KeyToken in Appsettings";

        }


        //creates password hash and salt sets the out params to the created hash and salt (would need to return in actual example and add to db context)
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //Creates a Hash from the entered password then compares it to the existing hash (Would be from db in real example)
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
