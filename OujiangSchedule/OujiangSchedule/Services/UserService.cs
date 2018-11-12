using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tianhai.OujiangApp.Schedule;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class UserService:DataService{
		public UserService(){}

		public static async Task<string> loginStartWithCaptcha(){
			Models.GeneralReturn<Models.Responses.UserLoginStart> result=await DataFetch.get<Models.Responses.UserLoginStart>(String.Format(urlUserLoginStart,urlBase));
			if(result.Status==200){
				App.PreferenceDatabase.SetToken(new Models.Preferences.Token{
					AccessToken=result.Data.Token,
					ValidBefore=DateTime.Now.AddMinutes(8),
					IsLoggedIn=false
				});
				return result.Data.Captcha;
			}else{
				throw new Exception(result.Return);
			}
		}

		public static async Task<bool> loginSubmit(string username,string password,string captcha){
			Models.Preferences.Token token=App.PreferenceDatabase.GetToken();
			if(token==null){
				throw new Exceptions.SessionTimeoutException();
			}
			Models.GeneralReturn<Models.Responses.UserLoginSubmit> result=await DataFetch.post<Models.Responses.UserLoginSubmit>(String.Format(urlUserLoginSubmit,urlBase,token.AccessToken),new Dictionary<string,string>{
				{ "username", username },
				{ "password", password },
				{ "captcha", captcha }
			});
			if(result.Status==200){
				if(result.Data.Success){
					token.IsLoggedIn=true;
					token.ValidBefore=DateTime.Now.AddMinutes(39);
					App.PreferenceDatabase.SetToken(token);
				}else{
					App.PreferenceDatabase.RemoveToken();
				}
				return result.Data.Success;
			}else{
				throw new Exception(result.Return);
			}
		}
	}
}
