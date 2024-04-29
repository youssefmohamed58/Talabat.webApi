using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Apis.Controllers
{
    public class basketController : BaseController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public basketController(IBasketRepository Basket , IMapper mapper)
      
         {
                _basketRepo = Basket;
                _mapper = mapper;          
         }
         
        [HttpGet]
        public async Task<ActionResult<CustomerBasket?>> GetBasket(string id) 
        { 
            var basket = await _basketRepo.GetCustomerBasket(id);
            if (basket is not null)
              return basket;
            return new CustomerBasket(id);


        }
        // Update or create
        [HttpPost]
        public async Task<ActionResult<CustomerBasket?>> createOrUpdateBasket(CustomerBasketDto basket)
        {
            var MappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var Basket = await _basketRepo.CreateOrUpdateBasket(MappedBasket);
            if (Basket is null)
                return BadRequest();
            return Ok(Basket);

        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepo.DeleteBasketAsync(id);

        }
       


     }


 }

