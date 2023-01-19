using BookWeb.API.Interfaces;
using BookWeb.API.Models;
using BookWeb.API.Models.Auth;
using BookWeb.API.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        protected Response _response;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            _response = new Response();
        }

        [HttpGet]
        [Route("GetOrders/{userId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllByUserName(string userId)
        {
            try
            {
                _response.Result = await _orderService.GetAllByUserNameAsync(userId);
                _response.DisplayMessage = "Order List";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Order does not exist";
                return NotFound(_response);
            }
            _response.Result = order;
            _response.DisplayMessage = "Order Information";
            return Ok(_response);
        }

        [HttpPost]
        [Route("create/{userName}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Order>> Create(string userName)
        {
            try
            {
                var newOrder = await _orderService.CreateOrderAsync(userName);
                _response.Result = newOrder;
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error while saving the record";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
