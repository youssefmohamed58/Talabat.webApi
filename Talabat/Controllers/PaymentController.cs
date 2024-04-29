using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Services;

namespace Talabat.Apis.Controllers;

[Authorize]
public class PaymentController : BaseController
{
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;

    public PaymentController(IPaymentService paymentService, IMapper mapper)
    {
        _paymentService = paymentService;
        _mapper = mapper;
    }
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntend(string basketId)
    {
        var Basket = await _paymentService.CreateOrUpdatePaymentIntendId(basketId);
        if (Basket is null)
            return BadRequest("There is a problem with your basket");
        var MappedBasket = _mapper.Map<CustomerBasket , CustomerBasketDto >(Basket);  
        return MappedBasket;   
    }
}

