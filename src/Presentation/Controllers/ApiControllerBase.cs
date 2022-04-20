using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// returns user id from user principal claims.
        /// If not exists throws error
        /// </summary>
        /// <returns></returns>
        protected Guid GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(p => p.Type == "id");

            var userId = userIdClaim?.Value;

            if (userId == null)
            {
                throw new NullReferenceException("UserId cannot be null");
            }

            return Guid.Parse(userId);
        }

        /// <summary>
        /// returns user email from user principal claims.
        /// If not exists returns null
        /// </summary>
        /// <returns></returns>
        protected string GetUserEmail()
        {
            var userIdClaim = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email);

            return userIdClaim!.Value;
        }

        /// <summary>
        /// returns user name from user principal claims.
        /// If not exists returns null
        /// </summary>
        /// <returns></returns>
        protected string GetUsername()
        {
            var userNameClaim = User.Claims.FirstOrDefault(p => p.Type == "username");

            return userNameClaim!.Value;
        }
    }
}