using BookWeb.API.Interfaces;
using BookWeb.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        protected Response _response;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            _response = new Response();
        }

        [HttpGet]
        [Route("GetCarts")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAll()
        {
            try
            {
                _response.Result = await _cartService.GetAllAsync();
                _response.DisplayMessage = "Cart list";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet]
        [Route("GetCart/{userId}")]
        public async Task<ActionResult<Cart>> GetCartByUserId(string userId)
        {
            var existingCart = await _cartService.GetByUserIdAsync(userId);
            if (existingCart == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Cart does not exist";
                return NotFound(_response);
            }
            _response.Result = existingCart;
            _response.DisplayMessage = "Cart Information";
            return Ok(_response);
        }

        [HttpPost]
        [Route("{userId}/AddItem/{bookId}")]
        public async Task<ActionResult<Cart>> AddItem(string userId, int bookId)
        {
            try
            {
                var cart = await _cartService.AddItemAsync(userId, bookId);

                if (cart == null)
                {

                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Cart does not exist";
                    return BadRequest(_response);
                }
                _response.Result = cart;
                _response.DisplayMessage = "Item successfully added";
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error while saving the record";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpDelete]
        [Route("{cartId}/RemoveItem/{cartItemId}")]
        public async Task<ActionResult<Cart>> RemoveItem(int cartId, int cartItemId)
        {
            try
            {
                var cart = await _cartService.RemoveItemAsync(cartId, cartItemId);
                
                if (cart != null)
                {
                    _response.Result = cart;
                    _response.DisplayMessage = "Item successfully removed";
                    return Ok(_response);
                }
                _response.IsSuccess = false;
                _response.DisplayMessage = "Cart does not exist";
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

        [HttpDelete]
        [Route("ClearCart/{userId}")]
        public async Task<ActionResult<Cart>> ClearCart(string userId)
        {
            var cart = await _cartService.ClearCartAsync(userId);

            if (cart == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Cart does not exist";
                return NotFound(_response);
            }
            _response.Result = cart;
            _response.DisplayMessage = "Cart Information";
            return Ok(_response);
        }
    }
}
