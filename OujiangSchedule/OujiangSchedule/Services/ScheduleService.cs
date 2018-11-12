using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tianhai.OujiangApp.Schedule;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class ScheduleService:DataService{
		public ScheduleService(){
			
		}

		public static async Task<Models.Schedule> fetchCurrent(string token){
			Models.GeneralReturn<Models.Schedule> result=await DataFetch.post<Models.Schedule>(String.Format(urlGetScheduleCurrent,urlBase,token),new Dictionary<string,string>{});
			if(result.Status==200){
				return result.Data;
			}else{
				throw new Exception(result.Return);
			}
		}

		public static async Task<List<Models.Lesson>> RefreshCurrentLessons(){
			Models.Preferences.Token token=await App.PreferenceDatabase.GetToken();
			if(token==null || !token.IsLoggedIn){
				throw new Exceptions.SessionTimeoutException();
			}
			Models.Schedule schedule=await fetchCurrent(token.AccessToken);
			if(schedule!=null){
				App.CurrentInfoDatabase.ResetAsync(schedule.Lessons);
				return schedule.Lessons;
			}else{
				return null;
			}
		}

		public static async Task<List<Models.Lesson>> GetCurrentLessons(){
			return await App.CurrentInfoDatabase.GetAsync();
		}
	}
}
