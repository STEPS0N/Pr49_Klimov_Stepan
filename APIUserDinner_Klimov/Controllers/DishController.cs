using APIUserDinner_Klimov.Context;
using APIUserDinner_Klimov.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIUserDinner_Klimov.Controllers
{
    [Route("dishes")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class DishController : Controller
    {
        /// <summary>
        /// Получить список версий
        /// </summary>
        /// <remarks>Данный метод возвращает список доступных версий меню (завтрак, обед, ужин)</remarks>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Проблемы при запросе</response>
        [Route("version")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetVersions()
        {
            DishContext context = new DishContext();

            try
            {
                var versions = context.Dishes.Select(x => x.Version).ToList();

                return Ok(new { version = versions });
            }
            catch
            {
                return StatusCode(400);
            }
        }
    }
}
