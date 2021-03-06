﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Tianhai.OujiangApp.Schedule.Models;
using Tianhai.OujiangApp.Schedule.Views;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class LessonListViewModel:BaseViewModel{
		public ObservableCollection<Models.Lesson> Lessons{get;set;}
		public Command LoadLessonsCommand{get;set;}
		
		public DateTime firstWeek_Sunday{get;set;}=Services.PreferenceService.GetDisplay_FirstWeek_Sunday();
		public DateTime currentWeek_Sunday{
			get{
				return firstWeek_Sunday.AddDays((currentWeek_Number-1)*7);
			}
		}
		private int _currentWeek_Number=Services.PreferenceService.DateTime_WeekNumber(DateTime.Now);
		public int currentWeek_Number{
			get{
				return _currentWeek_Number;
			}
			set{
				_currentWeek_Number=value;
				this.Title=String.Format("第 {0} 周",_currentWeek_Number);
				OnPropertyChanged();
			}
		}
		public Enums.WeekType currentWeek_Type{
			get{
				if((currentWeek_Number%2)==0){
					return Enums.WeekType.Even;
				}else{
					return Enums.WeekType.Odd;
				}
			}
		}

		public LessonListViewModel(){
			Title="课程表";
			Lessons=new ObservableCollection<Models.Lesson>();
			currentWeek_Number=Services.PreferenceService.DateTime_WeekNumber(DateTime.Now);
			LoadLessonsCommand=new Command(async ()=>await ExecuteLoadCommand());
		}

		async Task ExecuteLoadCommand(){
			if(IsBusy){
				return;
			}

			IsBusy=true;
			try{
				Lessons.Clear();
				var lessons=await Services.ScheduleService.GetCurrentLessons();
				if(lessons.Count>0){
					lessons.ForEach(o=>{
						Lessons.Add(o);
					});
				}else{
					//alert
				}
			}catch(Exception ex){
				Debug.WriteLine(ex);
			}finally{
				IsBusy=false;
			}
		}
	}
}