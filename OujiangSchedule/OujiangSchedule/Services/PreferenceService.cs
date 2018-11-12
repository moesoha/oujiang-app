using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tianhai.OujiangApp.Schedule;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class PreferenceService:DataService{
		public static DateTime GetDisplay_FirstWeek_Sunday(){
			return App.PreferenceDatabase.GetDisplay_FirstWeek_Sunday();
		}

		public static void SetDisplay_FirstWeek_Sunday(DateTime fws){
			App.PreferenceDatabase.SetDisplay_FirstWeek_Sunday(fws);
		}

		public static int DateTime_WeekNumber(DateTime dt){
			return (int)Math.Ceiling(((dt-GetDisplay_FirstWeek_Sunday()).Days+1)/7.0);
		}

		public static DateTime WeekNumber_DateTime(int wn){
			return DateTime.Now.Subtract(new TimeSpan((int)DateTime.Now.DayOfWeek+(wn-1)*7,0,0,0));
		}
	}
}
