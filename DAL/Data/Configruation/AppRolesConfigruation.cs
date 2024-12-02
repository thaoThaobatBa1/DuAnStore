﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configruation
{
	public class AppRolesConfigruation : IEntityTypeConfiguration<AppRoles>
	{
		public void Configure(EntityTypeBuilder<AppRoles> builder)
		{
			builder.ToTable("AppRoles");
			builder.Property(x=>x.Description).IsRequired().HasColumnType("nvarchar(225)");
		}
	}
}
