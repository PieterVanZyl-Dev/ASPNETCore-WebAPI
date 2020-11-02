using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace WebApi.Controllers
{

    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DimensionDataAPIContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(DimensionDataAPIContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllUser>>> GetUser()
        {
            return await _context.User
                .Select(p => new GetAllUser
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Username = p.UserName,
                    Role = p.Role
                }).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            // Gets ID from claims. 
            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();

            // Finds user. using ID converted from string to int
            var userName = await _context.User
            .Where(x => x.Id == Int32.Parse(usernameClaim.Value))
            .FirstOrDefaultAsync();

            //Username Token == null, then Badrequest
            if (userName == null)
            {
                return BadRequest(new { message = "Woops" });
            }
            if (!(userName.Id == id) & userName.Role != "admin")
            {
                return Unauthorized(new { message = "Trying to update other users if you're not an admin is not allowed. Message Administrator" });
            }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            //Password, hash and salt should never be exposed, I don't think admins should ever need to see a users password
            //if needed they can change the password with an update/put request though.
            return Ok(new
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            }); ;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUser user)
        {
            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            // Gets ID from claims. 
            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();

            // Finds user. using ID converted from string to int
            var userName = await _context.User
            .Where(x => x.Id == Int32.Parse(usernameClaim.Value))
            .FirstOrDefaultAsync();



            //Username Token == null, then Badrequest
            if (userName == null)
            {
                return BadRequest(new { message = "Woops" });
            }

            if ((id < 0) & (user.Id == null))
            {

                return BadRequest();
            }

            // if Username.Id (The id of the token != id of the user being edited, unauthorized;
            //and user is not an admin return unauthorized.
            if (!(userName.Id == user.Id.GetValueOrDefault()) & userName.Role != "admin")
            {
                return Unauthorized(new { message = "Trying to update other users if you're not an admin is not allowed. Message Administrator" });
            }

            var userobj = await _context.User
            .FindAsync(id);

            //only admin can adjust role
            if (userobj.Role == "admin" && user.Role != null)
            {
                userobj.Role = user.Role;
            }

            //throw unauthorized, if the user is not an admin and is trying to set a role !
            if (userobj.Role != "admin" && user.Role != null)
            {

                return Unauthorized(new { message = "User can't adjust role, please try a different update command without including role."});
            }

            //if password change is being requested, we need to update the password, hash & salt.
            if(user.Password != null)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
                userobj.PasswordHash = passwordHash;
                userobj.PasswordSalt = passwordSalt;

            }

            if(user.FirstName != null)
            {
                userobj.FirstName = user.FirstName;
            }

            if (user.LastName != null)
            {
                userobj.LastName = user.LastName;
            }

            if (user.Username != null)
            {
                userobj.UserName = user.Username;
            }



            _context.Entry(userobj).State = EntityState.Modified;




            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new{ message = "Updated User: " + userName.Id + " " + userName.UserName });

        }

        /// <summary>
        /// Creates/Registers an API User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created user item</response>
        /// <response code="400">If the item is null, password doesn't exist or username already exists</response>  
        // POST: api/Users
    [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(RegisterUser user)
        {

            if (string.IsNullOrWhiteSpace(user.Password))
                return BadRequest(new { message = "Password is required" });



            User userobj = new User();


            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);



            userobj.PasswordHash = passwordHash;
            userobj.PasswordSalt = passwordSalt;
            userobj.FirstName = user.FirstName;
            userobj.LastName = user.LastName;
            userobj.UserName = user.Username;
            userobj.Role = "user";
            //set default role to user.

            var userName = await _context.User
            .Where(x => x.UserName == user.Username)
            .FirstOrDefaultAsync();

            Console.WriteLine(userName);

            if(userName != null && userName.UserName == user.Username )
            {
                return BadRequest(new { message = "Woops, That UserName Already Exists" });
            }
            _context.User.Add(userobj);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                    throw;
            }

            return CreatedAtAction("GetUser", new { id = userobj.Id }, user);
        }

        /// <summary>
        /// Authenticates an API User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">return basic user info and authentication token</response>
        /// <response code="400">Throws bad request if username or password is incorrect</response>  
        // POST: Users/authenticate
        [ProducesResponseType(typeof(AuthenticatedUserResponse), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateUser user)
        {
            bool Ispassword = false;
            var userobj = _context.User.SingleOrDefault(x => x.UserName == user.Username);

            if (VerifyPasswordHash(user.Password, userobj.PasswordHash, userobj.PasswordSalt))
                Ispassword = true;

            if (Ispassword == false)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AppSettings:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userobj.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = userobj.Id,
                Username = userobj.UserName,
                FirstName = userobj.FirstName,
                LastName = userobj.LastName,
                Role = userobj.Role,
                Token = tokenString
            }); ;
        }



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {



                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
