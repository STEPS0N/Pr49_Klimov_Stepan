using APIUserDinner_Klimov.Context;
using APIUserDinner_Klimov.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIUserDinner_Klimov.Controllers
{
    [Route("api/dishes")]
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
        public ActionResult GetVersions()
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

        /// <summary>
        /// Получить список блюд
        /// </summary>
        /// <remarks>Данный метод возвращает список блюд</remarks>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Проблемы при запросе</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult GetDishes()
        {
            DishContext context = new DishContext();

            try
            {
                var dishes = context.Dishes.ToList();

                return Ok(dishes);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// Получить историю
        /// </summary>
        /// <remarks>Данный метод осуществляет получение истории</remarks>
        /// <response code="200">История получена</response>
        /// <response code="400">Проблемы при запросе</response>
        /// <response code="401">Неавторизированный доступ</response>
        [Route("histories")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult GetHistory()
        {
            DishContext context = new DishContext();

            try
            {
                var history = context.Orders.Select(x => new
                {
                    x.Address,
                    x.Date,
                    x.Dishes
                }).ToList();

                return Ok(history);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [ApiExplorerSettings(GroupName = "v1")]
        /// <summary>
        /// Отправить заказ
        /// </summary>
        /// <remarks>Данный метод осуществляет отправку заказа</remarks>
        /// <response code="200">Заказ принят</response>
        /// <response code="400">Проблемы при запросе</response>
        /// <response code="401">Неавторизированный доступ</response>
        [Route("order")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult CreateOrder([FromBody] Order order)
        {
            DishContext context = new DishContext();

            try
            {
                context.Orders.Add(order);
                context.SaveChanges();

                return StatusCode(200);
            }
            catch
            {
                return StatusCode(400);
            }
        }
    }
}
