using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteNetExtensions.Extensions;
using SQLite;

namespace Tianhai.OujiangApp.Schedule.Data{
	public class PreferenceDatabase{
		readonly SQLiteConnection db;

		public PreferenceDatabase(string dbPath){
			db=new SQLiteConnection(dbPath);
			db.CreateTable<Models.Preferences.Display>();
			db.CreateTable<Models.Preferences.Token>();
			db.CreateTable<Models.Preferences.OACredential>();
		}

		public void SetDisplay_FirstWeek_Sunday(DateTime fws){
			List<Models.Preferences.Display> displays=db.GetAllWithChildren<Models.Preferences.Display>();
			Models.Preferences.Display display;
			if(displays==null || displays.Count<=0){
				display=new Models.Preferences.Display{
					FirstWeek_Sunday=fws
				};
			}else{
				display=displays[0];
				display.FirstWeek_Sunday=fws;
			}
			db.DeleteAll<Models.Preferences.Display>();
			db.InsertWithChildren(display);
		}

		public DateTime GetDisplay_FirstWeek_Sunday(){
			List<Models.Preferences.Display> displays=db.GetAllWithChildren<Models.Preferences.Display>();
			Models.Preferences.Display display;
			if(displays==null || displays.Count<=0){
				display=new Models.Preferences.Display{
					FirstWeek_Sunday=DateTime.Now.Subtract(new TimeSpan((int)DateTime.Now.DayOfWeek,0,0,0))
				};
				db.InsertWithChildren(display);
			}else{
				display=displays[0];
			}
			return display.FirstWeek_Sunday;
		}

		public Models.Preferences.Token GetToken(){
			List<Models.Preferences.Token> tokens=db.GetAllWithChildren<Models.Preferences.Token>();
			Models.Preferences.Token token=null;
			if(tokens==null || tokens.Count<=0 || tokens[0].ValidBefore<=DateTime.Now){
				db.DeleteAll<Models.Preferences.Token>();
			}else{
				token=tokens[0];
			}
			return token;
		}

		public void SetToken(Models.Preferences.Token token){
			db.InsertOrReplaceWithChildren(token);
		}

		public void RemoveToken(){
			db.DeleteAll<Models.Preferences.Token>();
		}

		public Models.Preferences.OACredential GetOACredential(){
			var result=db.GetAllWithChildren<Models.Preferences.OACredential>();
			if(result==null || result.Count<=0){
				return null;
			}else{
				return result[0];
			}
		}

		public void SetOACredential(Models.Preferences.OACredential credential){
			db.InsertOrReplaceWithChildren(credential);
		}

		public void RemoveOACredential(){
			db.DeleteAll<Models.Preferences.OACredential>();
		}
	}
}
