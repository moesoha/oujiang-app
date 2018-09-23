using System;
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

		public LessonListViewModel(){
			Title="课程列表";
			Lessons=new ObservableCollection<Models.Lesson>();
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
				lessons.ForEach(o=>{
					Lessons.Add(o);
				});
			}catch(Exception ex){
				Debug.WriteLine(ex);
			}finally{
				IsBusy=false;
			}
		}
	}
}