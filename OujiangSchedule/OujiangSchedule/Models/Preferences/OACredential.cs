using System;
using SQLite;

namespace Tianhai.OujiangApp.Schedule.Models.Preferences{
	public class OACredential{
		[PrimaryKey]
		public int Id{get;set;}=1;
		
		public string Username{get;set;}
		public string Password{get;set;}
	}
}
