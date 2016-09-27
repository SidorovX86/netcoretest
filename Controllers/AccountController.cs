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
using netcoretest.Models;
using netcoretest.Data;
using Microsoft.EntityFrameworkCore;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace netcoretest.Controllers
{
	[Authorize]
	public class AccountController : Controller
    {
		private readonly SteamPalDbContext dbContext;

		public AccountController(SteamPalDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

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
			context.Identity.AddClaim(new Claim("UserInfo", player.ToString()));
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

		static string email = "a_sidorov@mail.ru";
		static string tradeUrl = "https://steamcommunity.com/tradelink";

		[HttpGet("api/[controller]/settings")]
		public async Task<IActionResult> GetSettings()
		{
			ulong steamId = UInt64.Parse(HttpContext.User.FindFirstValue("SteamId"));

			Account account = await this.dbContext.Accounts.AsNoTracking().SingleOrDefaultAsync(a => a.SteamId == steamId);

			if (account == null)
			{
				return NotFound();
			}

			AccountSettings settings = new AccountSettings();

			settings.Email = account.Email;
			settings.TradeUrl = account.TradeUrl;

			return new ObjectResult(settings);
		}

		[HttpPut("api/[controller]/settings")]
		//[ValidateAntiForgeryToken] // https://stackoverflow.com/questions/36628666/validateantiforgerytoken-in-ajax-request-with-aspnet-core-mvc
		public async Task<IActionResult> SetSettings([FromBody] AccountSettings settings)
		{
			if (settings == null)
			{
				return BadRequest();
			}

			ulong steamId = UInt64.Parse(HttpContext.User.FindFirstValue("SteamId"));

			Account account = await this.dbContext.Accounts.SingleOrDefaultAsync(a => a.SteamId == steamId);

			if (account == null)
			{
				return NotFound();
			}

			account.Email = settings.Email;
			account.TradeUrl = settings.TradeUrl;

			await this.dbContext.SaveChangesAsync();

			return Ok();
		}
	}
}
