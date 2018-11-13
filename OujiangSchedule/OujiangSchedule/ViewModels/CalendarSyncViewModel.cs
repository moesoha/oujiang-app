using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class CalendarSyncViewModel:BaseViewModel{
		public CalendarSyncViewModel(Page Page){
			Title="日程同步";
			
			SyncCommand=new Command(async ()=>{
				btnSyncIsEnabled=false;
				actidctIsRunning=true;

				TimePicker remindBefore=Page.FindByName<TimePicker>("remindBefore");
				Dictionary<int,List<TimeSpan>> timeSchedule=App.PreferenceDatabase.GetTimeSchedule();
				DateTime firstWeekSunday=Services.PreferenceService.GetDisplay_FirstWeek_Sunday();

				var addWeekNumber=await Page.DisplayAlert("那么……","你想不想顺便在日历中加上这个是学期第几周呢？","好啊","不要");

				try{
					var calender=await Services.CalendarService.ResetCalendar();
					int lastWeekNumberIs=1;

					(await Services.ScheduleService.GetCurrentLessons()).ForEach(async o=>{
						var reminder=Services.CalendarService.CreateEventReminders(new List<TimeSpan>{
							remindBefore.Time
						});

						for(int i=o.Week.Start;i<=o.Week.End;i++){
							if(i>lastWeekNumberIs){
								lastWeekNumberIs=i;
							}
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
					if(addWeekNumber){
						for(int i=1;i<=lastWeekNumberIs;i++){
							DateTime sunday=new DateTime(firstWeekSunday.Year,firstWeekSunday.Month,firstWeekSunday.Day,0,0,0).Add(new TimeSpan((i-1)*7,0,0,0));
							await Services.CalendarService.AddEvent(calender,String.Format("第 {0} 周",i),"","",sunday,sunday.Add(new TimeSpan(7,0,0,0)),Services.CalendarService.CreateEventReminders(new List<TimeSpan>{}),true);
						}
					}

					await Page.DisplayAlert("哇！","课表已经成功同步到了系统日历。","好耶");
					await Page.Navigation.PopAsync();
				}catch(Exception e){
					await Page.DisplayAlert("Awww",e.Message,"OK");
					throw e;
				}

				btnSyncIsEnabled=true;
				actidctIsRunning=false;
			});

			SwitchHintToOperationCommand=new Command(()=>{
				isOnHint=false;
				isOnOperation=true;
			});
		}
		
		public ICommand SyncCommand{get;}
		public ICommand SwitchHintToOperationCommand{get;}
		
		private bool _isOnHint=true;
		public bool isOnHint{
			get{
				return _isOnHint;
			}
			set{
				_isOnHint=value;
				OnPropertyChanged();
			}
		}
		private bool _isOnOperation=false;
		public bool isOnOperation{
			get{
				return _isOnOperation;
			}
			set{
				_isOnOperation=value;
				OnPropertyChanged();
			}
		}

		private bool _btnSyncIsEnabled=true;
		public bool btnSyncIsEnabled{
			get{
				return _btnSyncIsEnabled;
			}
			set{
				_btnSyncIsEnabled=value;
				OnPropertyChanged();
			}
		}
		
		private bool _actidctIsRunning=false;
		public bool actidctIsRunning{
			get{
				return _actidctIsRunning;
			}
			set{
				_actidctIsRunning=value;
				OnPropertyChanged();
			}
		}
	}
}