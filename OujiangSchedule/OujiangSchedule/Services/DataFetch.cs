using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class DataFetch{
		public static async Task<Models.GeneralReturn<T>> get<T>(string url){
			HttpClient client=new HttpClient();
			var response=await client.GetAsync(url);
			
			if(response!=null){
				string json=response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<Models.GeneralReturn<T>>(json);
			}else{
				return null;
			}
		}
	}
}
