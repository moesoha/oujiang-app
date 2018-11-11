using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteNetExtensionsAsync.Extensions;
using SQLite;

namespace Tianhai.OujiangApp.Schedule.Data{
	public class PreferenceDatabase{

		readonly SQLiteAsyncConnection db;

		public PreferenceDatabase(string dbPath){
			db=new SQLiteAsyncConnection(dbPath);
			db.CreateTableAsync<Models.Preferences.Display>().Wait();
			db.CreateTableAsync<Models.Preferences.Token>().Wait();
			db.CreateTableAsync<Models.Preferences.OACredential>().Wait();
		}

		public async Task SetDisplay_FirstWeek_Sunday(DateTime fws){
			List<Models.Preferences.Display> displays=await db.GetAllWithChildrenAsync<Models.Preferences.Display>();
			Models.Preferences.Display display;
			if(displays==null || displays.Count<=0){
				display=new Models.Preferences.Display{
					FirstWeek_Sunday=fws
				};
			}else{
				display=displays[0];
				display.FirstWeek_Sunday=fws;
			}
			await db.DeleteAllAsync<Models.Preferences.Display>();
			await db.InsertWithChildrenAsync(display);
		}

		public async Task<DateTime> GetDisplay_FirstWeek_Sunday(){
			List<Models.Preferences.Display> displays=await db.GetAllWithChildrenAsync<Models.Preferences.Display>();
			Models.Preferences.Display display;
			if(displays==null || displays.Count<=0){
				display=new Models.Preferences.Display{
					FirstWeek_Sunday=DateTime.Now.Subtract(new TimeSpan((int)DateTime.Now.DayOfWeek,0,0,0))
				};
				await db.InsertWithChildrenAsync(display);
			}else{
				display=displays[0];
			}
			return display.FirstWeek_Sunday;
		}

		public async Task<Models.Preferences.Token> GetToken(){
			List<Models.Preferences.Token> tokens=await db.GetAllWithChildrenAsync<Models.Preferences.Token>();
			Models.Preferences.Token token=null;
			if(tokens==null || tokens.Count<=0 || tokens[0].ValidBefore<=DateTime.Now){
				//token=new Models.Preferences.Token();
				//await db.InsertWithChildrenAsync(token);
				await db.DeleteAllAsync<Models.Preferences.Token>();
			}else{
				token=tokens[0];
			}
			return token;
		}

		public async Task SetToken(Models.Preferences.Token token){
			await db.InsertOrReplaceWithChildrenAsync(token);
		}

		public async Task RemoveToken(){
			await db.DeleteAllAsync<Models.Preferences.Token>();
		}

		public async Task<Models.Preferences.OACredential> GetOACredential(){
			var result=await db.GetAllWithChildrenAsync<Models.Preferences.OACredential>();
			if(result==null || result.Count<=0){
				return null;
			}else{
				return result[0];
			}
		}

		public async Task SetOACredential(Models.Preferences.OACredential credential){
			await db.InsertOrReplaceWithChildrenAsync(credential);
		}

		public async Task RemoveOACredential(){
			await db.DeleteAllAsync<Models.Preferences.OACredential>();
		}
	}
}
