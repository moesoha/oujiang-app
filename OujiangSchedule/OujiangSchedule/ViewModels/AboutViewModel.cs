using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class AboutViewModel:BaseViewModel{
		public AboutViewModel(){
			Title="About";

			OpenWebCommand=new Command(async ()=>{
				Console.WriteLine("Hello!");
				await Tianhai.OujiangApp.Schedule.Services.ScheduleService.fetchCurrent("demo");
				return;
			});
		}

		public ICommand OpenWebCommand{get;}
	}
}