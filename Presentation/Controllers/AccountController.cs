using Application.Dto.Identity;
using Application.Services.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares.Validations;
using Presentation.ViewModels.Identity;
using System.Net;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [ModelStateValidation]
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.Created)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AddUserViewModel model)
        {
            var userDto = _mapper.Map<UserDto>(model);

            await _accountService.RegisterAsync(userDto, model.Password!);

            var viewModel = _mapper.Map<UserViewModel>(userDto);

            return Created($"users/{viewModel.Id}", viewModel);
        }
    }
}