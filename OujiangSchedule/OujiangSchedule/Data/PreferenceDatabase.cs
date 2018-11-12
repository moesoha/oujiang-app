using System;
using System.Collections.Generic;
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
			db.CreateTable<Models.Preferences.TimeSchedule>();

			
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

		public Dictionary<int,List<TimeSpan>> GetTimeSchedule(){
			var result=db.GetAllWithChildren<Models.Preferences.TimeSchedule>();
			if(result==null || result.Count<=0){
				return ResetTimeSchedule();
			}else{
				return result[0].Table;
			}
		}

		public void SetTimeSchedule(Dictionary<int,List<TimeSpan>> table){
			db.InsertOrReplaceWithChildren(new Models.Preferences.TimeSchedule{
				Table=table
			});
		}

		public Dictionary<int,List<TimeSpan>> ResetTimeSchedule(){
			db.DeleteAll<Models.Preferences.TimeSchedule>();
			SetTimeSchedule(defaultTimeTable);
			return defaultTimeTable;
		}

		private static Dictionary<int,List<TimeSpan>> defaultTimeTable=new Dictionary<int,List<TimeSpan>>{
			{1,new List<TimeSpan>{new TimeSpan(08,10,00),new TimeSpan(08,55,00)}},
			{2,new List<TimeSpan>{new TimeSpan(09,05,00),new TimeSpan(09,50,00)}},
			{3,new List<TimeSpan>{new TimeSpan(10,10,00),new TimeSpan(10,55,00)}},
			{4,new List<TimeSpan>{new TimeSpan(11,05,00),new TimeSpan(11,50,00)}},
			{5,new List<TimeSpan>{new TimeSpan(13,30,00),new TimeSpan(14,15,00)}},
			{6,new List<TimeSpan>{new TimeSpan(14,25,00),new TimeSpan(15,10,00)}},
			{7,new List<TimeSpan>{new TimeSpan(15,30,00),new TimeSpan(16,15,00)}},
			{8,new List<TimeSpan>{new TimeSpan(16,25,00),new TimeSpan(17,10,00)}},
			{9,new List<TimeSpan>{new TimeSpan(18,30,00),new TimeSpan(19,15,00)}},
			{10,new List<TimeSpan>{new TimeSpan(19,25,00),new TimeSpan(20,10,00)}},
			{11,new List<TimeSpan>{new TimeSpan(20,30,00),new TimeSpan(21,15,00)}},
			{12,new List<TimeSpan>{new TimeSpan(21,25,00),new TimeSpan(22,10,00)}}
		};
	}
}
