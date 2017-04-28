/*********************************************
Author Name:    Umar Farooq
Date:           03 Feb 2014, 10:40 PM
/********************************************/


using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ViewModel
{
	public class DashBoardChart
	{
		public long CarJan { get; set; }
		public long CarFeb { get; set; }
		public long CarMar { get; set; }
		public long CarApr { get; set; }
		public long CarMay { get; set; }
		public long CarJune { get; set; }
		public long CarJuly { get; set; }
		public long CarAug { get; set; }
		public long CarSept { get; set; }
		public long CarOct { get; set; }
		public long CarNov { get; set; }
		public long CarDec { get; set; }

		public long VanJan { get; set; }
		public long VanFeb { get; set; }
		public long VanMar { get; set; }
		public long VanApr { get; set; }
		public long VanMay { get; set; }
		public long VanJune { get; set; }
		public long VanJuly { get; set; }
		public long VanAug { get; set; }
		public long VanSept { get; set; }
		public long VanOct { get; set; }
		public long VanNov { get; set; }
		public long VanDec { get; set; }

		public decimal AvgJan { get; set; }
		public decimal AvgFeb { get; set; }
		public decimal AvgMar { get; set; }
		public decimal AvgApr { get; set; }
		public decimal AvgMay { get; set; }
		public decimal AvgJune { get; set; }
		public decimal AvgJuly { get; set; }
		public decimal AvgAug { get; set; }
		public decimal AvgSept { get; set; }
		public decimal AvgOct { get; set; }
		public decimal AvgNov { get; set; }
		public decimal AvgDec { get; set; }

		public long TotalCar { get; set; }
		public long TotalVan { get; set; }

	}
}