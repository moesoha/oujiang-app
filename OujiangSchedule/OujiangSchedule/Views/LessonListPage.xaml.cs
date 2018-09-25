﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Tianhai.OujiangApp.Schedule.Models;
using Tianhai.OujiangApp.Schedule.Views;
using Tianhai.OujiangApp.Schedule.ViewModels;

namespace Tianhai.OujiangApp.Schedule.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LessonListPage:ContentPage{
		LessonListViewModel viewModel;
		Grid gridNow;

		public LessonListPage(){
			InitializeComponent();

			BindingContext=viewModel=new LessonListViewModel();
			viewModel.Lessons.CollectionChanged+=this.Lessons_CollectionChanged;
		}

		private void Lessons_CollectionChanged(object sender,System.Collections.Specialized.NotifyCollectionChangedEventArgs e){
			switch(e.Action){
				case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
					gridNow=CreateWeekGrid(viewModel.currentWeek_Sunday);
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					foreach(Models.Lesson o in e.NewItems){
						if(o.Session==null){
							Console.WriteLine("NULL! {0}",o.Name);
							continue;
						}
						if(o.Week.Type!=Enums.WeekType.Undefined && o.Week.Type!=viewModel.currentWeek_Type){
							continue;
						} // 不是全周/当前的单双周
						if(o.Week.Start>viewModel.currentWeek_Number || viewModel.currentWeek_Number>o.Week.End){
							continue;
						} // 不在上课周范围内
						int sessionSpan=Math.Abs(o.Session.Max()-o.Session.Min())+1;
						int sessionStart=o.Session.Min();

						var thisButton=new Button{
							Text=String.Format("{0}\n{1}",o.Name,o.Place),
							Padding=new Thickness(6),
							FontSize=11
						};
						gridNow.Children.Add(thisButton,(int)o.Day,sessionStart+1);
						Grid.SetRowSpan(thisButton,sessionSpan);
					}
					break;
			}
			this.Content=gridNow;
		}

		protected override void OnAppearing(){
			base.OnAppearing();
			
			this.Content=gridNow=CreateWeekGrid(viewModel.currentWeek_Sunday);
			viewModel.LoadLessonsCommand.Execute(null);
		}

		async void PrevWeek_Clicked(object sender, EventArgs e){
			//await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
		}

		async void NextWeek_Clicked(object sender, EventArgs e){
			//await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
		}

		private Grid CreateWeekGrid(DateTime sunday){
			var listGrid=new Grid{
				RowSpacing=1,
				ColumnSpacing=1
			};
			
			listGrid.RowDefinitions.Add(new RowDefinition {
				Height=new GridLength(20)
			}); // display dates
			listGrid.RowDefinitions.Add(new RowDefinition {
				Height=new GridLength(20)
			}); // display days of week
			for(int i=0;i<12;i++){
				listGrid.RowDefinitions.Add(new RowDefinition {
					Height=new GridLength(1,GridUnitType.Star)
				});
			} // only 12 classes set now
			for(int i=0;i<7;i++){
				listGrid.ColumnDefinitions.Add(new ColumnDefinition {
					Width=new GridLength(1,GridUnitType.Star)
				});
			} // 7 days in a week

			string[] dowNames=new string[]{"周日","周一","周二","周三","周四","周五","周六"};
			var nowDay=sunday.AddDays(-1);
			for(int i=0;i<7;i++){
				nowDay=nowDay.AddDays(1);
				listGrid.Children.Add(new Label{
					Text=dowNames[i],
					HorizontalTextAlignment=TextAlignment.Center,
					VerticalTextAlignment=TextAlignment.Center
				},i,1);
				listGrid.Children.Add(new Label{
					Text=String.Format("{0:D2}-{1:D2}",nowDay.Month,nowDay.Day),
					HorizontalTextAlignment=TextAlignment.Center,
					VerticalTextAlignment=TextAlignment.Center
				},i,0);
			}

			return listGrid;
		}
	}
}