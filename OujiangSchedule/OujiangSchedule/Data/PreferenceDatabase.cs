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

		public Dictionary<int,Models.Preferences.TimeScheduleSessionUnit> GetTimeSchedule(){
			var result=db.GetAllWithChildren<Models.Preferences.TimeSchedule>();
			if(result==null || result.Count<=0){
				return ResetTimeSchedule();
			}else{
				return result[0].Table;
			}
		}

		public void SetTimeSchedule(Dictionary<int,Models.Preferences.TimeScheduleSessionUnit> credential){
			db.InsertOrReplaceWithChildren(new Models.Preferences.TimeSchedule{
				Table=defaultTimeTable
			});
		}

		public Dictionary<int,Models.Preferences.TimeScheduleSessionUnit> ResetTimeSchedule(){
			db.DeleteAll<Models.Preferences.TimeSchedule>();
			SetTimeSchedule(defaultTimeTable);
			return defaultTimeTable;
		}

		private static Dictionary<int, Models.Preferences.TimeScheduleSessionUnit> defaultTimeTable=new Dictionary<int, Models.Preferences.TimeScheduleSessionUnit>{
			{1,new Models.Preferences.TimeScheduleSessionUnit{Session=1,Start=new TimeSpan(08,10,00),End=new TimeSpan(08,55,00)}},
			{2,new Models.Preferences.TimeScheduleSessionUnit{Session=2,Start=new TimeSpan(09,05,00),End=new TimeSpan(09,50,00)}},
			{3,new Models.Preferences.TimeScheduleSessionUnit{Session=3,Start=new TimeSpan(10,10,00),End=new TimeSpan(10,55,00)}},
			{4,new Models.Preferences.TimeScheduleSessionUnit{Session=4,Start=new TimeSpan(11,05,00),End=new TimeSpan(11,50,00)}},
			{5,new Models.Preferences.TimeScheduleSessionUnit{Session=5,Start=new TimeSpan(13,30,00),End=new TimeSpan(14,15,00)}},
			{6,new Models.Preferences.TimeScheduleSessionUnit{Session=6,Start=new TimeSpan(14,25,00),End=new TimeSpan(15,10,00)}},
			{7,new Models.Preferences.TimeScheduleSessionUnit{Session=7,Start=new TimeSpan(15,30,00),End=new TimeSpan(16,15,00)}},
			{8,new Models.Preferences.TimeScheduleSessionUnit{Session=8,Start=new TimeSpan(16,25,00),End=new TimeSpan(17,10,00)}},
			{9,new Models.Preferences.TimeScheduleSessionUnit{Session=9,Start=new TimeSpan(18,30,00),End=new TimeSpan(19,15,00)}},
			{10,new Models.Preferences.TimeScheduleSessionUnit{Session=10,Start=new TimeSpan(19,25,00),End=new TimeSpan(20,10,00)}},
			{11,new Models.Preferences.TimeScheduleSessionUnit{Session=11,Start=new TimeSpan(20,30,00),End=new TimeSpan(21,15,00)}},
			{12,new Models.Preferences.TimeScheduleSessionUnit{Session=12,Start=new TimeSpan(21,25,00),End=new TimeSpan(22,10,00)}}
		};
	}
}
