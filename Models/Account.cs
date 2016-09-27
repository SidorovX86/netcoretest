using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace netcoretest.Models
{
    public class Account
    {
		[Key]
		public Guid Id { get; set; }

		[Column("SteamId")]
		public long SteamIdLong { get; set; }

		[NotMapped]
		public ulong SteamId
		{
			get
			{
				return (ulong)this.SteamIdLong;
			}
			set
			{
				this.SteamIdLong = (long)value;
			}
		}

		public string Email { get; set; }

		public string TradeUrl { get; set; }
    }
}
