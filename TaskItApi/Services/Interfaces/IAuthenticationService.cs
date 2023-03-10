using System.Collections.Generic;
using System.Security.Claims;
using TaskItApi.Dtos;
using TaskItApi.Entities;

namespace TaskItApi.Services.Interfaces
{
    /// <summary>
    /// Interface for authenication
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticate User based on Incomingdata.
        /// On succes it will return a JWT string.
        /// </summary>
        /// <param name="userIncomingData">The incoming user data</param>
        /// <returns>Authentication token for the given user</returns>
        TokenDto AuthenicateUser(UserInComingDto userIncomingData);

        /// <summary>
        /// Register user based on incomingdata
        /// </summary>
        /// <param name="userInComingDto">The new user data</param>
        User RegisterUser(UserInComingDto userInComingDto);

        /// <summary>
        /// Check if the user exist
        /// </summary>
        /// <param name="userInComingDto"></param>
        bool UserExist(UserInComingDto userInComingDto);
    }
}
