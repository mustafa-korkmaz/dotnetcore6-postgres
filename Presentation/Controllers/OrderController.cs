using Application.Dto.Order;
using Application.Services.Order;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares.Validations;
using Presentation.ViewModels;
using Presentation.ViewModels.Order;
using System.Net;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ModelStateValidation]
        [HttpGet]
        [ProducesResponseType(typeof(ListViewModelResponse<OrderViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromQuery] ListViewModelRequest model)
        {
            var resp = await ListAsync(model);

            return Ok(resp);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var o = await _orderService.GetByIdAsync(id);

            if (o == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<OrderViewModel>(o);

            return Ok(viewModel);
        }

        [ModelStateValidation]
        [HttpPost]
        [ProducesResponseType(typeof(OrderViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] AddEditOrderViewModel model)
        {
            var orderDto = _mapper.Map<OrderDto>(model);

            await _orderService.AddAsync(orderDto);

            var order = _mapper.Map<OrderViewModel>(orderDto);

            return Created($"orders/{order.Id}", order);
        }

        [ModelStateValidation]
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] AddEditOrderViewModel model)
        {
            var orderDto = _mapper.Map<OrderDto>(model);

            orderDto.Id = id;

            await _orderService.UpdateAsync(orderDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _orderService.DeleteByIdAsync(id);

            return NoContent();
        }

        private async Task<ListViewModelResponse<OrderViewModel>> ListAsync(ListViewModelRequest model)
        {
            throw new NotImplementedException();
            //var resp = await _orderService.ListAsync(model.Offset, model.Limit);

            //return _mapper.Map<ListViewModelResponse<OrderViewModel>>(resp);
        }
    }
}