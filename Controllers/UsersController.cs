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

        /// <summary>
        /// Gets a list of API Users with varying detail depending on Role.
        /// </summary>
        /// <returns>A list of User Objects</returns>
        /// <response code="200">A list of User Objects</response>
        /// <response code="400">Token Provider User Doesn't exist any more</response>  
        // GET: api/Users
        [ProducesResponseType(typeof(List<GetAllUser>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllUser>>> GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            IEnumerable<Claim> claim = identity.Claims;

            var RoleClaim = claim
                .Where(x => x.Type == ClaimTypes.Role)
                .FirstOrDefault();

            //if admin expose Usernames
            if (RoleClaim.Value == "admin")
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
            else //do not expose usernames
            {
                return await _context.User
                   .Select(p => new GetAllUser
                   {
                       Id = p.Id,
                       FirstName = p.FirstName,
                       LastName = p.LastName,
                       Role = p.Role
                   }).ToListAsync();
            }
        }

        /// <summary>
        /// Gets an API User Details.
        /// </summary>
        /// <param name="id" example="10"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Returns the user item</response>
        /// <response code="400">Token Provider User Doesn't exist any more</response>  
        /// <response code="401">If the User is Unauthorized</response>  
        /// <response code="404">If Username Not found</response>  
        // GET: api/Users/5
        [ProducesResponseType(typeof(BasicUserResponse), 200)]
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


            if (userName == null)
            {
                return BadRequest(new { message = "Woops" });
            } 

            if (!(userName.Id == id) & userName.Role != "admin")
            {
                return Unauthorized(new { message = "Trying to get other users if you're not an admin is not allowed. Message Administrator" });
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            //Password, hash and salt should never be exposed, I don't think admins should ever need to see a users password
            //if needed they can change the password with an update request though.
            return Ok(new BasicUserResponse
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            }); ;
        }

        /// <summary>
        /// Updates an API Users Details.
        /// </summary>
        /// <param name="id" example="10"></param>
        /// <param name="user"></param>
        /// <returns>A message saying user was updated.</returns>
        /// <response code="200">Returns the Updated user Id and Name</response>
        /// <response code="400">Token Provider User Doesn't exist any more</response>  
        /// <response code="401">If the User is Unauthorized</response>  
        /// <response code="404">If Username Not found</response>  
        /// 
        // PUT: api/Users/5 (UPDATE)
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
            .AsNoTracking()
            .FirstOrDefaultAsync();




            //Username Token == null, then Badrequest
            if (userName == null)
            {
                return BadRequest(new { message = "Woops" });
            }

            bool isadmin = (userName.Role != "admin");

            if ((id < 0))
            {
                return BadRequest();
            }

            // if Username.Id (The id of the token != id of the user being edited, unauthorized;
            //and user is not an admin return unauthorized.
            if (!(userName.Id == id) & isadmin)
            {
                return Unauthorized(new { message = "Trying to update other users if you're not an admin is not allowed. Message Administrator" });
            }
            //throw unauthorized, if the user is not an admin and is trying to set a role !
            if (!(user.Role == null) & isadmin)
            {
                return Unauthorized(new { message = "User can't adjust role, please try a different update command without including role." });
            }

            var userobj = await _context.User
            .FindAsync(id);

            //only admin can adjust role
            if (isadmin & user.Role != null)
            {
                userobj.Role = user.Role;
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

            return Ok(new{ message = "Updated User: " + userobj.Id + " " + userobj.UserName });

        }

        /// <summary>
        /// Creates/Registers an API User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created user item</response>
        /// <response code="400">If the item is null, password doesn't exist or username already exists</response>  
        // POST: api/Users
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
                    new Claim(ClaimTypes.Name, userobj.Id.ToString()),
                    new Claim(ClaimTypes.Role, userobj.Role.ToString())
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


        /// <summary>
        /// Deletes an API User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return deleted user info</returns>
        /// <response code="200">return deleted user info</response>
        /// <response code="403">Throws forbidden if role user tries to delete anything</response>  
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            IEnumerable<Claim> claim = identity.Claims;

            var RoleClaim = claim
                .Where(x => x.Type == ClaimTypes.Role)
                .FirstOrDefault();
            //if admin Delete User
            if (RoleClaim.Value == "admin")
            {
                var user = await _context.User.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.User.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(new BasicUserResponse
                {
                    Id = user.Id,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                }); ;

            }
            else //if not admin throw forbidden
            {
                return Forbid();
            }



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
