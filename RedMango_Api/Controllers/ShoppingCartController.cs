using AutoMapper;
using DataAccess.Data.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Models;
using Models.DTO;
using Repositories.Contracts;

namespace RedMango_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;
        private ApiResponse _response;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _response = new ApiResponse();
            _shoppingCartRepository = shoppingCartRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetShoppingCart(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId)) {

                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "Bad Request - User not specified!" };
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                ShoppingCart shoppingCart = await _shoppingCartRepository.GetAsync(u => u.UserId == userId,"CartItems.MenuItem");
                
                if (shoppingCart.CartItems != null && shoppingCart.CartItems.Count() > 0) 
                {
                    shoppingCart.CartTotal = shoppingCart.CartItems.Sum(u => u.Quantity * u.MenuItem!.Price);
                }
                //_response.Result = shoppingCart;
                _response.Result = _mapper.Map<ShoppingCartDTO>(shoppingCart);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest; 
            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddOrUpdateItemsInCart(string userId, int menuItemId, int updateQuantityBy)
        {
            ShoppingCart shoppingCart = await _shoppingCartRepository.GetAsync(u => u.UserId == userId,"CartItems");
            MenuItem menuItem = await _menuItemRepository.GetAsync(menuItemId);

            if (menuItem == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            if (shoppingCart == null && updateQuantityBy>0)
            {
                // create shopping cart and add new cart item
                ShoppingCart newCart = new()
                {
                    UserId = userId
                };

                await _shoppingCartRepository.AddAsync(newCart);

                CartItem newCartItem = new()
                {
                    MenuItemId = menuItemId,
                    Quantity = updateQuantityBy,
                    ShoppingCartId = newCart.Id,
                    //MenuItem = null
                };

                await _shoppingCartRepository.AddCartItem(newCartItem);
            } 
            else
            {
                // Shopping Cart already exist -  add Cartitem or update CartItem

                CartItem cartItemInShoppingCart = shoppingCart.CartItems.FirstOrDefault(x => x.MenuItemId == menuItemId);
                if (cartItemInShoppingCart == null)
                {
                    // Item does not exist in shopping cart
                    CartItem newCartItem = new()
                    {
                        MenuItemId = menuItemId,
                        Quantity = updateQuantityBy,
                        ShoppingCartId = shoppingCart.Id,
                        //MenuItem = null
                    };
                    await _shoppingCartRepository.AddCartItem(newCartItem);
                }
                else
                {
                    // item exist in the cart and we update the quantity
                    int newQuantity = cartItemInShoppingCart.Quantity + updateQuantityBy;
                    if (updateQuantityBy == 0 || newQuantity <=0)
                    {
                        // remove item from cart and if only item remove shopping cart also
                        await _shoppingCartRepository.DeleteCartItem(cartItemInShoppingCart);
                        if (shoppingCart.CartItems.Count() == 0) 
                        {
                            await _shoppingCartRepository.DeleteAsync(shoppingCart.Id);
                        }
                    }
                    else
                    {
                        cartItemInShoppingCart.Quantity = newQuantity;
                        await _shoppingCartRepository.UpdateCartItem(cartItemInShoppingCart);
                    }
                }
            }

            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
