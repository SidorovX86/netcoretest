using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace netcoretest.Controllers
{
	[Authorize]
	public class AccountController : Controller
    {
		//[HttpGet]
		//public IActionResult Login(string returnUrl)
		//{
		//	return View();
		//}

		[HttpGet]
		[HttpPost]
		[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		public IActionResult Login(string returnUrl)
        {
			if (returnUrl == null)
				returnUrl = "/";

			return Challenge
				(
					new AuthenticationProperties
					{
						RedirectUri = returnUrl,
						IsPersistent = true,
						
					}
					, "Steam"
				);
        }

		[HttpGet, HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return LocalRedirect("/");
		}

		public static async Task ValidatePrincipalAsync(CookieValidatePrincipalContext context)
		{
			// https://docs.asp.net/en/latest/security/authentication/cookie.html

			bool isDisabled = false;

			if (isDisabled)
			{
				context.RejectPrincipal();

				await context.HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			}
		}
	}
}
