using System;
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
	}
}
