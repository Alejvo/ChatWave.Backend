using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hubs
{
    public class AppUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {

            var userId = connection.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? "jijijija";
            Console.WriteLine($"User Id From JWT: {userId}");
            return userId;
        }
    }
}
