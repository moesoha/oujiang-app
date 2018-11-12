using System;
using System.Collections.Generic;
using SQLite;

namespace Tianhai.OujiangApp.Schedule.Models.Preferences{
	public class Param{
		[PrimaryKey,Unique]
		public string Key{get;set;}
		public string Value{get;set;}
	}
}
