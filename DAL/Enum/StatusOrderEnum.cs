﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enum
{
	public enum StatusOrderEnum
	{
		Pending,
		Processing,
		Shipped,
		Delivered,
		Cancelled,
		Refunded
	}
}