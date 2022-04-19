
using Application.Dto.Identity;

namespace Application.Services.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// Checks for user by username or email. Sets user info and returns a valid token
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> GetToken(UserDto userDto, string password);

        /// <summary>
        /// Creates user and sets user info
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task RegisterAsync(UserDto userDto, string password);

        Task ResetAccount(string emailOrUsername);

        /// <summary>
        /// validates security code and returns respective user
        /// </summary>
        /// <param name="password"></param>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        Task<Guid> ConfirmPasswordReset(string password, string securityCode);

        /// <summary>
        /// changes user password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task ChangePassword(Guid userId, string newPassword);

        Task ChangePassword(Guid userId, string oldPassword, string newPassword);

        /// <summary>
        /// returns current user info by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserDto> GetUser(string userId);

        Task<Guid?> GetUserId(string email);
    }
}
