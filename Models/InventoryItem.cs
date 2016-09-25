using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcoretest.Models
{
    public class InventoryItem
    {
		public int Id { get; set; }
		public string Name { get; set; }

		public string ImageUrl { get; set; }

		public string BackpackUrl { get; set; }
    }
}
