using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenId;
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

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

		public static async Task OnSteamAuthenticated(OpenIdAuthenticatedContext context)
		{
			var player = context.User.Value<JObject>(SteamAuthenticationConstants.Parameters.Response)
				?.Value<JArray>(SteamAuthenticationConstants.Parameters.Players)
				?[0];

			ulong steamId = player.Value<ulong>("steamid");

			context.Identity.AddClaim(new Claim("SteamId", player.Value<string>("steamid"), ClaimValueTypes.UInteger64));
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
