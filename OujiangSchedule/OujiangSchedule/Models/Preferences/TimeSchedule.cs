using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Tianhai.OujiangApp.Schedule.Models.Preferences{
	public class TimeSchedule{
		[PrimaryKey]
		public int Id{get;set;}=1;
		
		// session => [Start,End]
		[TextBlob("TextBlobTable")]
		public Dictionary<int,List<TimeSpan>> Table{get;set;}
		public string TextBlobTable{get;set;}
	}
}
