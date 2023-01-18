using BookWeb.API.Interfaces;
using BookWeb.API.Models;
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

        [HttpGet()]
        [Route("GetOrders/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllByUserId(string userId)
        {
            try
            {
                _response.Result = await _orderService.GetAllByUserIdAsync(userId);
                _response.DisplayMessage = "Order List";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet()]
        [Route("GetOrder/{Id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            try
            {
                if (order != null)
                {
                    _response.Result = order;
                    _response.DisplayMessage = "Order Information";
                    return Ok(_response);
                }
                _response.IsSuccess = false;
                _response.DisplayMessage = "Order does not exist";
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error while saving the record";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
            
        }

        [HttpPost()]
        [Route("create/{userId}")]
        public async Task<ActionResult<Order>> Create(string userId)
        {
            try
            {
                var newOrder = await _orderService.CreateOrderAsync(userId);
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
