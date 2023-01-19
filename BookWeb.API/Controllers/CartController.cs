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
    [Authorize]
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
        [Authorize(Roles = UserRoles.User)]
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
        [Route("GetCart/{userName}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Cart>> GetCartByUserName(string userName)
        {
            var existingCart = await _cartService.GetByUserNameAsync(userName);
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
        [Route("{userName}/AddItem/{bookId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Cart>> AddItem(string userName, int bookId)
        {
            try
            {
                var cart = await _cartService.AddItemAsync(userName, bookId);

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
        [Authorize(Roles = UserRoles.User)]
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
        [Route("ClearCart/{userName}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Cart>> ClearCart(string userName)
        {
            var cart = await _cartService.ClearCartAsync(userName);

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
