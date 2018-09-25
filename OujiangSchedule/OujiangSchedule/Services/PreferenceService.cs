using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tianhai.OujiangApp.Schedule;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class PreferenceService:DataService{
		public DateTime Display_FirstWeek_Sunday{get;set;}

		public PreferenceService(){
			violenceInit();
		}

		public async Task violenceInit(){
			await GetDisplay_FirstWeek_Sunday();
			return;
		}

		public async Task<DateTime> GetDisplay_FirstWeek_Sunday(){
			if(Display_FirstWeek_Sunday==DateTime.MinValue || Display_FirstWeek_Sunday==null){
				Display_FirstWeek_Sunday=await App.PreferenceDatabase.GetDisplay_FirstWeek_Sunday();
			}
			return Display_FirstWeek_Sunday;
		}

		public async Task SetDisplay_FirstWeek_Sunday(DateTime fws){
			await App.PreferenceDatabase.SetDisplay_FirstWeek_Sunday(fws);
			Display_FirstWeek_Sunday=fws;
		}

		public int DateTime_WeekNumber(DateTime dt){
			return (int)Math.Ceiling(((dt-Display_FirstWeek_Sunday).Days)/7.0);
		}

		public DateTime WeekNumber_DateTime(int wn){
			return DateTime.Now.Subtract(new TimeSpan((int)DateTime.Now.DayOfWeek+(wn-1)*7,0,0,0));
		}
	}
}
