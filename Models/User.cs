using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace WebApi.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }

    public class AuthenticateUser
    {
        /// <summary>
        /// The UserName of the User
        /// </summary>
        /// <example>Cinderella2020</example>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// The Password of the User
        /// </summary>
        /// <example>Somepasswordhere</example>
        [Required]
        public string Password { get; set; }
    }
    public class RegisterUser
    {
        /// <summary>
        /// The FirstName of the New User
        /// </summary>
        /// <example>Cinderella</example>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// The LastName of the New User
        /// </summary>
        /// <example>Glass shoes</example>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// The Username of the New User
        /// </summary>
        /// <example>Cinderella2020</example>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// The Password of the New User
        /// </summary>
        /// <example>Somepasswordhere</example>
        [Required]
        public string Password { get; set; }
    }

    public class UpdateUser
    {
        /// <summary>
        /// The Username of User
        /// </summary>
        /// <example>Cinderella2020</example>
        public string Username { get; set; }
        /// <summary>
        /// The firstName of User
        /// </summary>
        /// <example>Cinderella</example>
        public string FirstName { get; set; }
        /// <summary>
        /// The Last Name of User
        /// </summary>
        /// <example>Glass Shoes</example>
        public string LastName { get; set; }
        /// <summary>
        /// The password of User
        /// </summary>
        /// <example>PasswordHere</example>
        public string Password { get; set; }
        /// <summary>
        /// The Role of User
        /// </summary>
        /// <example>user</example>
        public string Role { get; set; }
    }
    public class GetAllUser
    {
        /// <summary>
        /// The User Id of User
        /// </summary>
        /// <example>10</example>
        public int Id { get; set; }
        /// <summary>
        /// The Username of User
        /// </summary>
        /// <example>Cinderella2020</example>
        public string Username { get; set; }
        /// <summary>
        /// The firstName of User
        /// </summary>
        /// <example>Cinderella</example>
        public string FirstName { get; set; }
        /// <summary>
        /// The Last Name of User
        /// </summary>
        /// <example>Glass Shoes</example>
        public string LastName { get; set; }
        /// <summary>
        /// The Role of User
        /// </summary>
        /// <example>user</example>
        public string Role { get; set; }
    }

    public class AuthenticatedUserResponse
    {
        /// <summary>
        /// The User Id of User
        /// </summary>
        /// <example>10</example>
        public int Id { get; set; }
        /// <summary>
        /// The Username of User
        /// </summary>
        /// <example>Cinderella2020</example>
        public string Username { get; set; }
        /// <summary>
        /// The firstName of User
        /// </summary>
        /// <example>Cinderella</example>
        public string FirstName { get; set; }
        /// <summary>
        /// The Last Name of User
        /// </summary>
        /// <example>Glass Shoes</example>
        public string LastName { get; set; }
        /// <summary>
        /// The Role of User
        /// </summary>
        /// <example>user</example>
        public string Role { get; set; }
        /// <summary>
        /// The Authorization Token for the User
        /// </summary>
        /// <example>Token: Tokenhere</example>
        public string Token { get; set; }
    }

    public class BasicUserResponse
    {
        /// <summary>
        /// The User Id of User
        /// </summary>
        /// <example>10</example>
        public int Id { get; set; }
        /// <summary>
        /// The Username of User
        /// </summary>
        /// <example>Cinderella2020</example>
        public string Username { get; set; }
        /// <summary>
        /// The firstName of User
        /// </summary>
        /// <example>Cinderella</example>
        public string FirstName { get; set; }
        /// <summary>
        /// The Last Name of User
        /// </summary>
        /// <example>Glass Shoes</example>
        public string LastName { get; set; }
        /// <summary>
        /// The Role of User
        /// </summary>
        /// <example>user</example>
        public string Role { get; set; }
    }

}
