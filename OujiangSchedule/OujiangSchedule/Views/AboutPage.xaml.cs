using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tianhai.OujiangApp.Schedule.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage:ContentPage{
		ViewModels.AboutViewModel viewModel;

		public AboutPage(){
			InitializeComponent();

			viewModel=new ViewModels.AboutViewModel();
			viewModel.Navigation=Navigation;

			BindingContext=viewModel;
		}
	}
}