using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tianhai.OujiangApp.Schedule.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarSyncPage:ContentPage{
		ViewModels.CalendarSyncViewModel viewModel;

		public CalendarSyncPage(){
			InitializeComponent();

			BindingContext=viewModel=new ViewModels.CalendarSyncViewModel(this);
		}
	}
}