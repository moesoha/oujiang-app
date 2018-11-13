using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class AboutViewModel:BaseViewModel{
		public INavigation Navigation{get;set;}

		public AboutViewModel(Page Page){
			Title="更多";
			
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

			CalendarSyncCommand=new Command(async ()=>{
				btnCalendarSyncIsEnabled=false;
				try{
					var accountName=Services.CalendarService.GetAccountName();

					var status=await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Calendar);
					if(status!=PermissionStatus.Granted){
						if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Calendar)){
							await Page.DisplayAlert("权限申请","我们接下来将向系统申请读写日历的权限。","好啊");
						}
						var results=await CrossPermissions.Current.RequestPermissionsAsync(Permission.Calendar);
						if(results.ContainsKey(Permission.Calendar)){
							status=results[Permission.Calendar];
						}
					}
					if(status==PermissionStatus.Granted){
						await Page.Navigation.PushAsync(new Views.CalendarSyncPage());
					}else if(status!=PermissionStatus.Unknown){
						await Page.DisplayAlert("没有权限","权限申请失败或您拒绝了权限申请，不能继续。","好吧");
					}
				}catch(Exceptions.NotLoggedInException){
					await Page.DisplayAlert("压根儿没登入","你还一次都没有登入过，不能同步。请先使用“更新课表”。","好的");
				}catch(Exception e){
					await Page.DisplayAlert("未知错误",e.Message,"好的");
				}
				btnCalendarSyncIsEnabled=true;
			});
		}
		
		public ICommand RefreshScheduleCommand{get;}
		public ICommand CalendarSyncCommand{get;}

		private bool _btnCalendarSyncIsEnabled=true;
		public bool btnCalendarSyncIsEnabled{
			get{
				return _btnCalendarSyncIsEnabled;
			}
			set{
				_btnCalendarSyncIsEnabled=value;
				OnPropertyChanged();
			}
		}

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