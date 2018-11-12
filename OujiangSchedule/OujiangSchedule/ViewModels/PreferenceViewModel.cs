using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class PreferenceViewModel:BaseViewModel{
		public PreferenceViewModel(Page Page){
			Title="参数管理";

			OpenTimeScheduleCommand=new Command(async ()=>{
				await Page.Navigation.PushAsync(new Views.PreferenceTimeSchedulePage());
			});
		}

		public ICommand OpenTimeScheduleCommand{get;set;}
	}
}