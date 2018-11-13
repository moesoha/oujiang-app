using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class CalendarSyncViewModel:BaseViewModel{
		public CalendarSyncViewModel(){
			Title="日程同步";
			
			SyncCommand=new Command(async ()=>{});
		}
		
		public ICommand SyncCommand{get;}
		
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