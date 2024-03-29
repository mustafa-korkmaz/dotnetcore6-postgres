using Application.Dto.Product;
using Application.Services.Product;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares.Validations;
using Presentation.ViewModels;
using Presentation.ViewModels.Product;
using System.Net;
using Application.Dto;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [ModelStateValidation]
        [HttpGet]
        [ProducesResponseType(typeof(ListViewModelResponse<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromQuery] ListViewModelRequest model)
        {
            var request = _mapper.Map<ListDtoRequest>(model);

            var dtos = await _productService.ListAsync(request);

            var resp = _mapper.Map<ListViewModelResponse<ProductViewModel>>(dtos);

            return Ok(resp);
        }

        [ModelStateValidation]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var o = await _productService.GetByIdAsync(id);

            if (o == null)
            {
                return NotFound();
            }

            var viewMmodel = _mapper.Map<ProductViewModel>(o);

            return Ok(viewMmodel);
        }

        [ModelStateValidation]
        [HttpPost]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] AddEditProductViewModel model)
        {
            var productDto = _mapper.Map<ProductDto>(model);

            await _productService.AddAsync(productDto);

            var product = _mapper.Map<ProductViewModel>(productDto);

            return Created($"products/{product.Id}", product);
        }

        [ModelStateValidation]
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] AddEditProductViewModel model)
        {
            var productDto = _mapper.Map<ProductDto>(model);

            productDto.Id = id;

            await _productService.UpdateAsync(productDto);

            return NoContent();
        }

        [ModelStateValidation]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _productService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}