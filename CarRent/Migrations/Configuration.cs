/*********************************************
Author Name:    Umar Farooq
Date:           14 Jan 2014, 12:40 PM
/********************************************/

namespace FinanceApp.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<Repositories.Repository>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(Repositories.Repository context)
		{
		}
	}
}
