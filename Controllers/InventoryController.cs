using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcoretest.Models;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace netcoretest.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class InventoryController : Controller
	{
		// GET: api/values
		[HttpGet("{appId:int}")]
		public async Task<IActionResult> Get(int appId)
        {
			if (appId <= 0)
			{
				return BadRequest();
			}

			string steamId = HttpContext.User.FindFirst("SteamId")?.Value;

			if (steamId == null)
			{
				return BadRequest();
			}

			List<InventoryItem> list = new List<InventoryItem>();

			// Request user game inventory from Steam.

			string json;

			using (HttpClient httpClient = new HttpClient())
			{
				json = await httpClient.GetStringAsync($"https://steamcommunity.com/profiles/{steamId}/inventory/json/{appId}/2");
			}

			var response = JObject.Parse(json);

			if (response.Value<bool>("success"))
			{
				// Build item description dictionary.

				Dictionary<string, JToken> descriptionList = new Dictionary<string, JToken>();

				foreach (var item in response.Value<JObject>("rgDescriptions"))
					descriptionList.Add(item.Key, item.Value);

				// Enumerate items.

				foreach (var item in response.Value<JObject>("rgInventory"))
				{
					ulong itemId = item.Value.Value<ulong>("id");

					string descKey = $"{item.Value.Value<string>("classid")}_{item.Value.Value<string>("instanceid")}";
					JToken desc = descriptionList[descKey];

					// Only tradable items.

					if (desc.Value<bool>("tradable"))
					{
						list.Add(new InventoryItem()
						{
							Id = itemId,
							Name = desc.Value<string>("market_name"),
							ImageUrl = /*"https://steamcommunity-a.akamaihd.net/economy/image/" +*/ desc.Value<string>("icon_url")
						});
					}
				}
			}
			
			return new ObjectResult(list);
        }
    }
}
