using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Tianhai.OujiangApp.Schedule.Models.Preferences{
	public class TimeSchedule{
		[PrimaryKey]
		public int Id{get;set;}=1;
		
		[TextBlob("TextBlobTable")]
		// session => TimeScheduleSessionUnit
		public Dictionary<int,TimeScheduleSessionUnit> Table{get;set;}
	}
	public class TimeScheduleSessionUnit{
		public int Session{get;set;}

		// TimeSpan from 00:00:00 of a day
		public TimeSpan Start{get;set;}
		public TimeSpan End{get;set;}
	}
}
