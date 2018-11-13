using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class CalendarService{
		public static string GetAccountName(){
			Models.Preferences.OACredential credential=App.PreferenceDatabase.GetOACredential();
			if(credential==null){
				throw new Exceptions.NotLoggedInException("没有找到登陆过的账号");
			}

			return String.Format("{0}@ojjx.wzu.edu.cn",credential.Username);
		}

		public static async Task<Calendar> CreateCalendar(string accountName){
			Calendar cal=new Calendar{
				AccountName=accountName,
				Name=accountName
			};

			await CrossCalendars.Current.AddOrUpdateCalendarAsync(cal);

			App.PreferenceDatabase.SetParam(Enums.PreferenceParamKey.CalendarExternalID,cal.ExternalID);
			return cal;
		}

		public static async Task<Calendar> GetCalendar(){
			string id=App.PreferenceDatabase.GetParam(Enums.PreferenceParamKey.CalendarExternalID);

			if(String.IsNullOrWhiteSpace(id)){
				throw new Exception("No calendar ID.");
			}

			return await CrossCalendars.Current.GetCalendarByIdAsync(id);
		}

		public static async Task RemoveCalendar(){
			string id=App.PreferenceDatabase.GetParam(Enums.PreferenceParamKey.CalendarExternalID);
			if(!String.IsNullOrWhiteSpace(id)){
				await CrossCalendars.Current.DeleteCalendarAsync(await CrossCalendars.Current.GetCalendarByIdAsync(id));
			}
			string accountName=GetAccountName();
			foreach(Calendar c in (await CrossCalendars.Current.GetCalendarsAsync())){
				if(String.Equals(c.Name,accountName)){
					await CrossCalendars.Current.DeleteCalendarAsync(c);
				}
			}
		}
		
		public static async Task<Calendar> ResetCalendar(){
			string accountName=GetAccountName();
			await RemoveCalendar();
			return await CreateCalendar(accountName);
		}

		public static List<CalendarEventReminder> CreateEventReminders(List<TimeSpan> timeBeforeList){
			List<CalendarEventReminder> calendarEventReminders=new List<CalendarEventReminder>();
			timeBeforeList.ForEach(timeBefore=>calendarEventReminders.Add(new CalendarEventReminder{
				TimeBefore=timeBefore,
				Method=CalendarReminderMethod.Default
			}));
			return calendarEventReminders;
		}

		public static async Task<CalendarEvent> AddEvent(Calendar calendar,string eventName,string eventLocation,string eventDescription,DateTime eventTimeStart,DateTime eventTimeEnd,List<CalendarEventReminder> eventReminders,bool eventIsAllDay=false){
			CalendarEvent calEvent=new CalendarEvent{
				Name=eventName,
				Location=eventLocation,
				Description=eventDescription,
				AllDay=eventIsAllDay,
				Start=eventTimeStart,
				End=eventTimeEnd,
				Reminders=eventReminders
			};
			await CrossCalendars.Current.AddOrUpdateEventAsync(calendar,calEvent);
			return calEvent;
		}
	}
}
