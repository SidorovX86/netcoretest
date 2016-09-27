﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcoretest.Models
{
    public class AccountSettings
    {
		[Key]
		public Guid Id { get; set; }

		public string Email { get; set; }

		public string TradeUrl { get; set; }
    }
}
