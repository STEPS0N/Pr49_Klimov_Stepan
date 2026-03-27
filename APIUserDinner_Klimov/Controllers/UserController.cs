using APIUserDinner_Klimov.Context;
using APIUserDinner_Klimov.Models;
using APIUserDinner_Klimov.Models.UserDTO;
using Microsoft.AspNetCore.Mvc;

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
                    Password = userDto.Password
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
    }
}
