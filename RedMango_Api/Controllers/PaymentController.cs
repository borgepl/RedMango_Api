using DataAccess.Data.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Contracts;
using Stripe;
using System.Drawing;

namespace RedMango_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        protected ApiResponse _response;
        public PaymentController(IShoppingCartRepository shoppingCartRepository)
        {
            _response = new ApiResponse();
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> MakePayment(string userId)
        {
            ShoppingCart shoppingCart = await _shoppingCartRepository.GetAsync(u => u.UserId == userId, "CartItems.MenuItem");

            if (shoppingCart == null || shoppingCart.CartItems == null || shoppingCart.CartItems.Count() == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            #region Create Payment Intent

            shoppingCart.CartTotal = shoppingCart.CartItems.Sum(u => u.Quantity * u.MenuItem.Price);

            PaymentIntentCreateOptions options = new()
            {
                Amount = (int)(shoppingCart.CartTotal * 100),
                Currency = "eur",
                PaymentMethodTypes = new List<string>
                  {
                    "card",
                  },
            };
            PaymentIntentService service = new();
            PaymentIntent response = service.Create(options);
            shoppingCart.StripePaymentIntentId = response.Id;
            shoppingCart.ClientSecret = response.ClientSecret;


            #endregion

            _response.Result = shoppingCart;
            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        }
    }
}
