using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace netcoretest.Controllers
{
    //[Route("api/[controller]")]
	[Authorize]
	public class InventoryController : Controller
    {
        // GET: api/values
        [HttpGet]

		public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
    }
}
