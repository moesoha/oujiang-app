using System;
using System.Linq;
using System.Windows.Input;

using Xamarin.Forms;

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

			TestCommand=new Command(async ()=>{
				try{
					var timeSchedule=App.PreferenceDatabase.GetTimeSchedule();
					var firstWeekSunday=Services.PreferenceService.GetDisplay_FirstWeek_Sunday();
					var calender=await Services.CalendarService.ResetCalendar();
					(await Services.ScheduleService.GetCurrentLessons()).ForEach(async o=>{
						var reminder=Services.CalendarService.CreateEventReminders(new System.Collections.Generic.List<TimeSpan>{new TimeSpan(0,15,0)});
						for(int i=o.Week.Start;i<=o.Week.End;i++){
							var weekType=i%2==0?Enums.WeekType.Even:Enums.WeekType.Odd;
							if(o.Week.Type!=Enums.WeekType.Undefined && o.Week.Type!=weekType){
								continue;
							} // 不是全周/当前的单双周
							TimeSpan startSpan=timeSchedule[o.Session.Min()][0];
							TimeSpan endSpan=timeSchedule[o.Session.Max()][1];
							DateTime today=new DateTime(firstWeekSunday.Year,firstWeekSunday.Month,firstWeekSunday.Day,0,0,0).Add(new TimeSpan((i-1)*7+(int)o.Day,0,0,0));
							await Services.CalendarService.AddEvent(calender,String.Format("{0} {1}",o.Name,o.Teacher),o.Place,"",today.Add(startSpan),today.Add(endSpan),reminder);
						}
					});
					await Page.DisplayAlert("Calendar Added","done","OK");
				}catch(Exception e){
					await Page.DisplayAlert("Awww",e.Message,"OK");
					throw e;
				}
			});
		}
		
		public ICommand RefreshScheduleCommand{get;}
		public ICommand TestCommand{get;}

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