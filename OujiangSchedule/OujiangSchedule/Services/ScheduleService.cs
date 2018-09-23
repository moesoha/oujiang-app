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

		public static async Task<List<Models.Lesson>> GetCurrentLessons(){
			Console.WriteLine("Start GCL()");
			var lessons=await App.CurrentInfoDatabase.GetAsync();
			Console.WriteLine("GCL: ReadDB Finish!");
			if(lessons.Count==0){
				Console.WriteLine("GCL: count==0");
				Models.Schedule schedule=await fetchCurrent("demo");
				Console.WriteLine("GCL: Fetch through Web!");
				if(schedule!=null){
					App.CurrentInfoDatabase.ResetAsync(schedule.Lessons);
					return schedule.Lessons;
				}else{
					return null;
				}
			}else{
				Console.WriteLine("GCL: count!=0");
				return lessons;
			}
		}
	}
}
