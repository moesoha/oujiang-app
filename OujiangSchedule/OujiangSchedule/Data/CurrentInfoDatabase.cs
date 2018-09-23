using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Tianhai.OujiangApp.Schedule.Data{
	public class CurrentInfoDatabase{
		readonly SQLiteAsyncConnection db;

		public CurrentInfoDatabase(string dbPath){
			db=new SQLiteAsyncConnection(dbPath);
			db.CreateTableAsync<Models.Lesson>().Wait();
		}

		public Task<List<Models.Lesson>> GetAsync(){
			return db.Table<Models.Lesson>().ToListAsync();
		}
		
		public async void ResetAsync(List<Models.Lesson> lessons){
			await db.DeleteAllAsync<Models.Lesson>();
			await db.InsertAllAsync(lessons);
			return;
		}
	}
}
