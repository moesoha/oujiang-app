using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class AboutViewModel:BaseViewModel{
		public AboutViewModel(){
			Title="设置";

			RefreshScheduleCommand=new Command(async ()=>{
				
				await Tianhai.OujiangApp.Schedule.Services.ScheduleService.GetCurrentLessons();
				return;
			});
		}

		public ICommand RefreshScheduleCommand{get;}
	}
}