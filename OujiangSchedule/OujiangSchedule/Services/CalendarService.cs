using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class CalendarService{
		public static async Task<string> CreateCalendar(){
			string username=App.PreferenceDatabase.GetOACredential().Username;

			if(String.IsNullOrWhiteSpace(username)){
				throw new Exception("没有找到登陆过的账号");
			}

			Calendar cal=new Calendar{
				AccountName=String.Format("{0}@ojjx.wzu.edu.cn",username),
				Name=String.Format("{0}@ojjx.wzu.edu.cn",username)
			};

			await CrossCalendars.Current.AddOrUpdateCalendarAsync(cal);

			App.PreferenceDatabase.SetParam(Enums.PreferenceParamKey.CalendarExternalID,cal.ExternalID);
			return cal.ExternalID;
		}

		public static async Task<Calendar> GetCalendar(){
			string id=App.PreferenceDatabase.GetParam(Enums.PreferenceParamKey.CalendarExternalID);

			if(String.IsNullOrWhiteSpace(id)){
				throw new Exception("No calendar ID.");
			}

			return await CrossCalendars.Current.GetCalendarByIdAsync(id);
		}
		
		public static async Task<string> ResetCalendar(){
			string id=App.PreferenceDatabase.GetParam(Enums.PreferenceParamKey.CalendarExternalID);
			if(!String.IsNullOrWhiteSpace(id)){
				await CrossCalendars.Current.DeleteCalendarAsync(await CrossCalendars.Current.GetCalendarByIdAsync(id));
			}
			return await CreateCalendar();
		}
	}
}
