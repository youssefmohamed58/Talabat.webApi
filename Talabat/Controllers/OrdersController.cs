using System.Collections.Specialized;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Talabat.Apis.DTOS;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Services;
using Talabat.Repository;

namespace Talabat.Apis.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService orderService;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IMapper mapper , IOrderService _orderService , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            orderService = _orderService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("CreateOrder")]
        [Authorize]
        public async Task<ActionResult<Orders>> CreateOrder(OrderDto order)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto , Address>(order.shippingAddress);
            var Order = await orderService.CreateOrderAsync(BuyerEmail, order.BasketId , order.DelivaryMethodId , MappedAddress);
            if (Order is null)
                return BadRequest("There is an problem when return order");
            return Order;
        }

        [HttpGet("GetOrder")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<OrderItemDto>>> GetOrders()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await orderService.GetOrdersForSpecificUserAsync(BuyerEmail);
            if (Orders is null)
                return NotFound("There is no Orders for this user");
            var MappedOrders = _mapper.Map<IReadOnlyList<Orders>, IReadOnlyList<OrderToReturnDto>>(Orders);
            return Ok(MappedOrders);  
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderItemDto>> GetOrder(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await orderService.GetOrderByIdForSpecificUserAsync(BuyerEmail ,id);
            if (Order is null)
                return NotFound("There is no Order with this id For this User");
            var MappedOrder = _mapper.Map<Orders, OrderToReturnDto>(Order);
            return Ok(MappedOrder);
        }
        [HttpGet("GetDelivaryMethod")]
        public async Task<ActionResult<IReadOnlyList<DelivaryMethod>>> GetDelivaryMethod()
        {
            var DelivaryMethods = await _unitOfWork.Repositories<DelivaryMethod>().GetAllAsync();
            return Ok(DelivaryMethods); 
           
        }
    }
}
