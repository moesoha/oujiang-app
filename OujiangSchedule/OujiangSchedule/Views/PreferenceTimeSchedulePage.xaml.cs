using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Tianhai.OujiangApp.Schedule.ViewModels;

namespace Tianhai.OujiangApp.Schedule.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PreferenceTimeSchedulePage:ContentPage{
		PreferenceTimeScheduleViewModel viewModel;

		private Dictionary<int,List<TimePicker>> tableTimePickers;

		public PreferenceTimeSchedulePage(){
			InitializeComponent();

			BindingContext=viewModel=new PreferenceTimeScheduleViewModel();

			CreateTable();
		}
		
		private EventHandler TimePicker_End_Changed_Constructor(int i){
			return async (object sender,EventArgs e)=>{
				if(tableTimePickers[i][0].Time>((TimePicker)sender).Time){
					await DisplayAlert("数据错误","结束时间不能大于起始时间！","好的");
					((TimePicker)sender).Time=tableTimePickers[i][0].Time;
				}
			};
		}

		private async void Submit_Clicked(object sender,EventArgs e){
			bool test=await DataValidate();
			if(test){
				var newTable=new Dictionary<int,List<TimeSpan>>();
				for(int i=1;i<=12;i++){
					newTable.Add(i,new List<TimeSpan>{
						tableTimePickers[i][0].Time,
						tableTimePickers[i][1].Time
					});
				}
				App.PreferenceDatabase.SetTimeSchedule(newTable);
				await DisplayAlert("哇！","时间表修改成功！","好耶");
				await Navigation.PopAsync();
			}
		}

		private async Task<bool> DataValidate(){
			TimeSpan last=new TimeSpan(0,0,0);
			for(int i=1;i<=12;i++){
				if(tableTimePickers[i][0].Time<last){
					await DisplayAlert("数据错误",String.Format("第 {0} 节的开始时间不能小于上一节课的结束时间！",i),"好的");
					return false;
				}
				last=tableTimePickers[i][0].Time;
				if(tableTimePickers[i][1].Time<last){
					await DisplayAlert("数据错误",String.Format("第 {0} 节的结束时间不能小于这节课的开始时间！",i),"好的");
					return false;
				}
				last=tableTimePickers[i][1].Time;
			}
			return true;
		}

		private void CreateTable(){
			tableTimePickers=new Dictionary<int, List<TimePicker>>();
			Dictionary<int,List<TimeSpan>> previousSchedule=App.PreferenceDatabase.GetTimeSchedule();

			for(int i=1;i<=12;i++){
				StackLayout container=new StackLayout{
					Spacing=3
				};
				container.Children.Add(new Label{
					FontSize=Device.GetNamedSize(NamedSize.Small,typeof(Label)),
					Text=String.Format("第 {0} 节",i)
				});
				Grid grid=new Grid{
					ColumnDefinitions={
						new ColumnDefinition{
							Width=new GridLength(4,GridUnitType.Star)
						},
						new ColumnDefinition{
							Width=new GridLength(1,GridUnitType.Star)
						},
						new ColumnDefinition{
							Width=new GridLength(4,GridUnitType.Star)
						}
					}
				};
				List<TimePicker> timePickers=new List<TimePicker>{
					new TimePicker{
						Time=previousSchedule[i][0]
					},
					new TimePicker{
						Time=previousSchedule[i][1]
					}
				};
				//timePickers[0].BindingContextChanged+=TimePicker_Start_Changed_Constructor(i);
				//timePickers[1].BindingContextChanged+=TimePicker_End_Changed_Constructor(i);
				grid.Children.Add(timePickers[0],0,0);
				grid.Children.Add(timePickers[1],2,0);
				tableTimePickers.Add(i,timePickers);
				grid.Children.Add(new Label{
					FontSize=Device.GetNamedSize(NamedSize.Large,typeof(Label)),
					FontAttributes=FontAttributes.Bold,
					HorizontalTextAlignment=TextAlignment.Center,
					VerticalTextAlignment=TextAlignment.Center,
					Text="→"
				},1,0);
				container.Children.Add(grid);
				TimeTableContainer.Children.Add(container);
			}
		}
	}
}