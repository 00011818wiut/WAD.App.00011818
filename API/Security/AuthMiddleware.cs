using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace API.Security
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUserRepository userRepository)
        {
            var token = httpContext.Request.Headers["accessToken"].ToString();

            Console.WriteLine("Token:");
            Console.WriteLine(token);

            if (token == null || token == "")
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync("Token doesn`t exists");
            }
            else
            {
                var user = userRepository.FindUserByToken(token);

                if (user == null)
                {
                    httpContext.Response.StatusCode = 403;
                    await httpContext.Response.WriteAsync("Invalid token");
                }
                else
                {
                    httpContext.Items["user"] = user;
                    await _next.Invoke(httpContext);
                }
            }
        }
    }
}