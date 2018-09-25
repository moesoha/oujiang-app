using System;
using SQLite;

namespace Tianhai.OujiangApp.Schedule.Models.Preferences{
	public class Display{
		[PrimaryKey,AutoIncrement]
		public int Id { get; set; }

		public DateTime FirstWeek_Sunday{get;set;}
	}
}
