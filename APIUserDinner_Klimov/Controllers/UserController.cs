using APIUserDinner_Klimov.Context;
using APIUserDinner_Klimov.Models;
using APIUserDinner_Klimov.Models.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace APIUserDinner_Klimov.Controllers
{
    [Route("api/auth")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserController : Controller
    {
        /// <summary>
        /// Регистрация
        /// </summary>
        ///<remarks>Данный метод осуществляет регистрацию пользователя</remarks>
        /// <response code="201">Успешная регистрация</response>
        /// <response code="400">Проблемы при регистрации</response>
        [Route("register")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]

        public ActionResult RegIn([FromBody] UserRegistrationDto userDto)
        {
            UserContext userContext = new UserContext();

            try
            {
                var User = userContext.User.FirstOrDefault(x => x.Login == userDto.Login);

                if (User != null)
                {
                    return Conflict("Такой пользователь есть");
                }

                var newUser = new User
                {
                    Login = userDto.Login,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
                };

                userContext.User.Add(newUser);
                userContext.SaveChanges();

                return CreatedAtAction(nameof(RegIn), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// Аутентификация в системе
        /// </summary>
        ///<remarks>Данный метод осуществляет аутентификацию пользователя</remarks>
        /// <response code="200">Успешная аутентификация</response>
        /// <response code="400">Проблема аутентификации</response>
        /// <response code="401">Пользователь не найден</response>
        [Route("login")]
        [HttpPost]
        [ProducesResponseType(typeof(TokenGet), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public ActionResult LogIn([FromBody] UserLoginDto loginDto)
        {
            UserContext userContext = new UserContext();

            var user = userContext.User.FirstOrDefault(x => x.Email == loginDto.Email);

            if (user == null)
            {
                return StatusCode(401);
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return StatusCode(400);
            }

            string token = HashUserId(user.Id);
            return Ok(new TokenGet { Token = token });

        }
        private string HashUserId(int id)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(id.ToString());
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

}
