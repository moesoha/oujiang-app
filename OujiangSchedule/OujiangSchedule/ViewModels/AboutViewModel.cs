using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class AboutViewModel:BaseViewModel{
		public INavigation Navigation{get;set;}

		public AboutViewModel(){
			Title="设置";

			RefreshScheduleCommand=new Command(async ()=>{
				this.btnRefreshScheduleIsEnabled=false;
				Models.Preferences.Token token=await App.PreferenceDatabase.GetToken();
				if(token==null){
					await Navigation.PushAsync(new Views.LoginPage());
				}else{
					await Services.ScheduleService.RefreshCurrentLessons();
				}
				this.btnRefreshScheduleIsEnabled=true;
				return;
			});
		}

		public ICommand RefreshScheduleCommand{get;}

		private bool _btnRefreshScheduleIsEnabled=true;
		public bool btnRefreshScheduleIsEnabled{
			get{
				return _btnRefreshScheduleIsEnabled;
			}
			set{
				_btnRefreshScheduleIsEnabled=value;
				OnPropertyChanged();
			}
		}
	}
}