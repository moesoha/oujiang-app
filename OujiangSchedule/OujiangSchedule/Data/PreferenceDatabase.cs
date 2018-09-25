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
	}
}
