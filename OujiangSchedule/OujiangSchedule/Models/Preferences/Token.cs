using System;
using SQLite;

namespace Tianhai.OujiangApp.Schedule.Models.Preferences{
	public class Token{
		[PrimaryKey]
		public int Id{get;set;}=1;
		
		public DateTime ValidBefore{get;set;}
		public string AccessToken{get;set;}
		public bool IsLoggedIn{get;set;}=false;
	}
}
