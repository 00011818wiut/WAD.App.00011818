using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using API.Domain;
using API.Models;

namespace API.Controllers
{
	[Route("api/")]
	public class AuthController : Controller
	{
		private readonly IHttpContextAccessor _accessor;

        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

		public AuthController(
			IHttpContextAccessor accessor,
			IUserRepository userRepository,
			IAuthService authService)
		{
			this._accessor = accessor;
			this._userRepository = userRepository;
			this._authService = authService;
		}

		[HttpPost("register")]
		public IActionResult Register([FromBody] Register user)
		{
			if (user.Password != user.ConfirmPassword)
			{
				return new BadRequestObjectResult("Password and confirmation doesnt match");
			}

			var existsUser = _userRepository.FindUserByEmail(user.Email);

			if (existsUser != null)
			{
				return new BadRequestObjectResult("User already exists");
			}

			var newUser = new User();

			newUser.Email = user.Email;
            newUser.Name = user.Name;
            newUser.Surname = user.Surname;
            newUser.Password = user.Password;
            newUser.Token = _authService.GenerateToken(user.Email, user.Password);

			_userRepository.CreateUser(newUser);

            var userData = new
            {
                id = newUser.ID,
                name = newUser.Name,
                surname = newUser.Surname,
                email = newUser.Email,
                token = newUser.Token
            };

            return new OkObjectResult(userData);
		}

		[HttpPost("login")]
        public IActionResult Login([FromBody] Login user)
        {
			var existsUser = _userRepository.FindUserByEmail(user.Email);

			if (existsUser == null)
			{
				return new NotFoundObjectResult("User not found");
			}

			if (existsUser.Password != user.Password)
			{
				return new BadRequestObjectResult("Email or password wrong");
			}

			var userData = new {
				id = existsUser.ID,
				name = existsUser.Name,
				surname = existsUser.Surname,
				email = existsUser.Email,
				token = existsUser.Token
			};

            return new OkObjectResult(userData);
        }

		[HttpGet("verify")]
		public IActionResult verifyToken([FromHeader(Name = "Access-Token")] string accessToken)
		{
			if (_accessor.HttpContext != null)
			{
				User? user = (User?) _accessor.HttpContext.Items["user"];

				if (user != null)
				{
                    var userData = new
                    {
                        id = user.ID,
                        name = user.Name,
                        surname = user.Surname,
                        email = user.Email,
                        token = user.Token
                    };

					return new OkObjectResult(userData);
                }
				else
				{
                    return new UnauthorizedObjectResult("Token not verified. Because user with token not found.");
                }
            }
			else
			{
				return new UnauthorizedObjectResult("Token not verified. Because http context not found.");
			}
		}

		[HttpGet("/auth/users")]
		public IActionResult getAllUsers()
		{
			var users = _userRepository.FindAllUsers();
			return new OkObjectResult(users);
		}
    }
}