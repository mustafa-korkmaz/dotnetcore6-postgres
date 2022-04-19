using Application.Constants;
using Application.Dto.Identity;
using Application.Exceptions;
using AutoMapper;
using Domain.Aggregates.Identity;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Application.Services.Account
{
    public class AccountService : ServiceBase<IUserRepository, User, UserDto, Guid>, IAccountService
    {
        private readonly ILogger<AccountService> _logger;

        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork uow, ILogger<AccountService> logger, IMapper mapper /*IEmailService emailService*/ )
          : base(uow, logger, mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public Task ChangePassword(Guid userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> ConfirmPasswordReset(string password, string securityCode)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetToken(UserDto userDto, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid?> GetUserId(string email)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(UserDto userDto, string password)
        {
            var userByName = await Repository.GetByUsernameAsync(userDto.Username.Normalize());

            if (userByName != null)
            {
                throw new ValidationException(ErrorMessages.UserExists);
            }

            var userByEmail = await Repository.GetByEmailAsync(userDto.Email.Normalize());

            if (userByEmail != null)
            {
                throw new ValidationException(ErrorMessages.UserExists);
            }

            userDto.PasswordHash = HashPassword(password);

            var user = _mapper.Map<User>(userDto);

            await Repository.AddAsync(user);

            await Uow.SaveAsync();

            userDto.Id = user.Id;

            //todo add default user roles also
        }

        public Task ResetAccount(string emailOrUsername)
        {
            throw new NotImplementedException();
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
