using AutoMapper;
using Common;
using DataAccess.Data.Domain;
using DataAccess.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO.Order;
using Repositories.Contracts;

namespace RedMango_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private ApiResponse _response;

        public OrderController(IOrderRepository orderRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _mapper = mapper;
            _response = new ApiResponse();

        }

        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> GetOrders(string? userId)
        {
            try
            {   if (!string.IsNullOrEmpty(userId))
                {
                    var orderHeaders = 
                        await _orderRepository.GetAllFilteredAsync(u => u.ApplicationUserId == userId, "OrderDetails.MenuItem");
                    orderHeaders.OrderByDescending(u => u.OrderHeaderId).ToList();

                    var user = await _userManager.FindByIdAsync(userId);
                    var ordersToReturn = _mapper.Map<IList<OrderHeaderDTO>>(orderHeaders);

                    _response.Result = ordersToReturn;
                }
                else
                {
                    var orderHeaders =
                        await _orderRepository.GetAllAsync();
                    orderHeaders.OrderByDescending(u => u.OrderHeaderId).ToList();

                    _response.Result = _mapper.Map<IList<OrderHeaderDTO>>(orderHeaders); ;
                }
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetOrder(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                
                var order =
                        await _orderRepository.GetAsync(u => u.OrderHeaderId == id, "OrderDetails.MenuItem");
                
                    
                if (order == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                var user = await _userManager.FindByIdAsync(order.ApplicationUserId);
                var orderDtoToReturn = _mapper.Map<OrderHeaderDTO>(order);

                if (user != null)
                {
                    
                    //orderDtoToReturn.User.Email = user.Email;
                    //orderDtoToReturn.User.Name  = user.Name;
                    //orderDtoToReturn.User.PhoneNo = user.PhoneNumber;
                }
                _response.Result = orderDtoToReturn;
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
        public async Task<ActionResult<ApiResponse>> CreateOrder([FromBody] OrderHeaderCreateDTO orderHeaderDTO)
        {
            try
            {
                var orderHeader = _mapper.Map<OrderHeader>(orderHeaderDTO);
                orderHeader.OrderDate = DateTime.Now;
                orderHeader.Status = string.IsNullOrEmpty(orderHeaderDTO.Status) ? SD.Status_Pending : orderHeaderDTO.Status;

                if (ModelState.IsValid)
                {
                    await _orderRepository.AddAsync(orderHeader);

                    var orderDetailsList = new List<OrderDetail>();

                    foreach ( var orderDetailDTO in orderHeaderDTO.OrderDetailsDTO)
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            OrderHeaderId = orderHeader.OrderHeaderId,
                            ItemName = orderDetailDTO.ItemName,
                            MenuItemId = orderDetailDTO.MenuItemId,
                            Price = orderDetailDTO.Price,
                            Quantity = orderDetailDTO.Quantity

                        };

                        orderDetailsList.Add(orderDetail);
                    }
                    // Save orderdetails wirh AddRange.
                    await _orderRepository.AddOrderDetails(orderDetailsList);

                    _response.Result = orderHeader;
                    orderHeader.OrderDetails = null;
                    _response.StatusCode = System.Net.HttpStatusCode.Created;
                    return Ok(_response);
                }

                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest; ;
            }
            return _response;
        }

 
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateOrderHeader(int id, [FromBody] OrderHeaderUpdateDTO orderHeaderUpdate)
        {
            try
            {
                if (orderHeaderUpdate == null || id != orderHeaderUpdate.OrderHeaderId) 
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                OrderHeader orderFromDb = await _orderRepository.GetAsync(id);
                if (orderFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdate.PickupName))
                {
                    orderFromDb.PickupName = orderHeaderUpdate.PickupName;
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdate.PickupEmail))
                {
                    orderFromDb.PickupEmail = orderHeaderUpdate.PickupEmail;
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdate.PickupPhoneNumber))
                {
                    orderFromDb.PickupPhoneNumber = orderHeaderUpdate.PickupPhoneNumber;
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdate.Status))
                {
                    orderFromDb.Status = orderHeaderUpdate.Status;
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdate.StripePaymentIntentID))
                {
                    orderFromDb.StripePaymentIntentID = orderHeaderUpdate.StripePaymentIntentID;
                }

                await _orderRepository.UpdateAsync(orderFromDb);
                
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest; ;
            }
            return _response;
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
