using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class DataFetch{
		private static readonly HttpClient client=new HttpClient();

		public static async Task<Models.GeneralReturn<T>> get<T>(string url){
			var response=await client.GetAsync(url);
			if(response!=null){
				string json=response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<Models.GeneralReturn<T>>(json);
			}else{
				return null;
			}
		}
		public static async Task<Models.GeneralReturn<T>> post<T>(string url,IDictionary<string,string> postData){
			var response=await client.PostAsync(url,new FormUrlEncodedContent(postData));
			
			if(response!=null){
				string json=response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<Models.GeneralReturn<T>>(json);
			}else{
				return null;
			}
		}
	}
}
