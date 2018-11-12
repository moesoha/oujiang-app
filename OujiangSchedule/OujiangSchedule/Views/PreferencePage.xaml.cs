using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Tianhai.OujiangApp.Schedule.ViewModels;

namespace Tianhai.OujiangApp.Schedule.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PreferencePage:ContentPage{
		PreferenceViewModel viewModel;

		public PreferencePage(){
			InitializeComponent();

			BindingContext=viewModel=new PreferenceViewModel(this);
		}

		protected override void OnAppearing(){
			base.OnAppearing();

			currentWeekNumber.Text=Services.PreferenceService.DateTime_WeekNumber(DateTime.Now).ToString();
		}

		private void currentWeekNumber_Completed(object sender,EventArgs e){
			int cwn=Convert.ToInt32(((Entry)sender).Text);
			Services.PreferenceService.SetDisplay_FirstWeek_Sunday(Services.PreferenceService.WeekNumber_DateTime(cwn));
		}

		private void currentWeekNumber_TextChanged(object sender,TextChangedEventArgs e){
			if(e.NewTextValue.Length>0 && e.NewTextValue.Length>((Entry)sender).MaxLength){
				((Entry)sender).Text=e.NewTextValue.Substring(0,((Entry)sender).MaxLength);
			}
		}
	}
}