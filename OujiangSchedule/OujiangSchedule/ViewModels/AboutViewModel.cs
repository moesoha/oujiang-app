using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class AboutViewModel:BaseViewModel{
		public INavigation Navigation{get;set;}

		public AboutViewModel(Page Page){
			Title="设置";

			RefreshScheduleCommand=new Command(async ()=>{
				this.btnRefreshScheduleIsEnabled=false;
				try{
					var result=await Services.ScheduleService.RefreshCurrentLessons();
					if(result!=null){
						await Page.DisplayAlert("哇！","课表已经更新当前学期最新版本。","好耶");
					}
				}catch(Exceptions.SessionTimeoutException){
					await Navigation.PushAsync(new Views.LoginPage());
				}catch(Exception e){
					await Page.DisplayAlert("遇到未知错误",e.Message,"好的");
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