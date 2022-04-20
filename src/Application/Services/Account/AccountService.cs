using Application.Constants;
using Application.Dto.Identity;
using Application.Exceptions;
using AutoMapper;
using Domain.Aggregates.Identity;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

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

        public Task ChangePasswordAsync(Guid userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> ConfirmPasswordResetAsync(string password, string securityCode)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetTokenAsync(UserDto userDto, string password)
        {
            User? user;

            if ((userDto.Username != null && userDto.Username.Contains("@")) || userDto.Email != null) // login via email
            {
                user = await Repository.GetByEmailAsync(userDto.Email!);
            }
            else // login via username
            {
                user = await Repository.GetByUsernameAsync(userDto.Username!);
            }

            if (user == null)
            {
                throw new ValidationException(ErrorMessages.UserNotFound);
            }

            var isPasswordValid = VerifyHashedPassword(user.PasswordHash, password);

            if (!isPasswordValid)
            {
                throw new ValidationException(ErrorMessages.IncorrectUsernameOrPassword);
            }

            userDto.Id = user.Id;
            userDto.Username = user.Username;
            userDto.Email = user.Email;
            userDto.NameSurname = user.NameSurname;

            return GenerateToken(userDto);
        }

        public async Task<UserDto> GetUserAsync(Guid userId)
        {
            var user = await Repository.GetByIdAsync(userId);

            //todo: fetch claims, roles 
            return _mapper.Map<UserDto>(user);
        }

        public Task<Guid?> GetUserIdAsync(string email)
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

        public Task ResetAccountAsync(string emailOrUsername)
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

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return AreHashesEqual(buffer3, buffer4);
        }

        private string GenerateToken(UserDto user)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<System.Security.Claims.Claim>();

            foreach (var userRole in user.Roles)
            {
                var roleIdentifierClaim = new System.Security.Claims.Claim(ClaimTypes.Role, userRole, ClaimValueTypes.String);

                claims.Add(roleIdentifierClaim);
            }

            var nameIdentifierClaim = new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String);
            var emailClaim = new System.Security.Claims.Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String);

            claims.Add(new System.Security.Claims.Claim("id", user.Id.ToString()));
            claims.Add(new System.Security.Claims.Claim("username", user.Username));

            claims.Add(nameIdentifierClaim);
            claims.Add(emailClaim);

            foreach (var userClaim in user.Claims)
            {
                claims.Add(new System.Security.Claims.Claim("permission", userClaim));
            }

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.Email, "Token"), claims);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenConstants.Issuer,
                Audience = JwtTokenConstants.Audience,
                SigningCredentials = JwtTokenConstants.SigningCredentials,
                Subject = identity,
                Expires = DateTime.UtcNow.Add(JwtTokenConstants.TokenExpirationTime),
                NotBefore = DateTime.UtcNow
            });

            return handler.WriteToken(securityToken);
        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
