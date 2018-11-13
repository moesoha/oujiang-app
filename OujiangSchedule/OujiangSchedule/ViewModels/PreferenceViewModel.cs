using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class PreferenceViewModel:BaseViewModel{
		public PreferenceViewModel(Page Page){
			Title="参数管理";

			OpenTimeScheduleCommand=new Command(async ()=>{
				btnOpenTimeScheduleIsEnabled=false;
				await Page.Navigation.PushAsync(new Views.PreferenceTimeSchedulePage());
				btnOpenTimeScheduleIsEnabled=true;
			});
		}

		public ICommand OpenTimeScheduleCommand{get;set;}
		private bool _btnOpenTimeScheduleIsEnabled=true;
		public bool btnOpenTimeScheduleIsEnabled{
			get{
				return _btnOpenTimeScheduleIsEnabled;
			}
			set{
				_btnOpenTimeScheduleIsEnabled=value;
				OnPropertyChanged();
			}
		}
	}
}