using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tianhai.OujiangApp.Schedule;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class ScheduleService:DataService{
		public ScheduleService(){
			
		}

		public static async Task<Models.Schedule> fetchCurrent(string token){
			Models.GeneralReturn<Models.Schedule> result=await DataFetch.get<Models.Schedule>(String.Format(urlGetScheduleCurrent,urlBase,token));
			if(result.Status==200){
				return result.Data;
			}else{
				throw new Exception(result.Return);
			}
		}

		public static async Task<List<Models.Lesson>> RefreshCurrentLessons(){
			Models.Schedule schedule=await fetchCurrent("demo");
			if(schedule!=null){
				App.CurrentInfoDatabase.ResetAsync(schedule.Lessons);
				return schedule.Lessons;
			}else{
				return null;
			}
		}

		public static async Task<List<Models.Lesson>> GetCurrentLessons(){
			var lessons=await App.CurrentInfoDatabase.GetAsync();
			if(lessons.Count==0){
				return await RefreshCurrentLessons();
			}else{
				return lessons;
			}
		}
	}
}
