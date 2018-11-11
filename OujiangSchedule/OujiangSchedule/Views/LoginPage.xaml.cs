using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tianhai.OujiangApp.Schedule.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage:ContentPage{
		ViewModels.LoginViewModel viewModel;

		public LoginPage(){
			InitializeComponent();

			BindingContext=viewModel=new ViewModels.LoginViewModel(Content,Navigation);
		}

		protected override async void OnAppearing(){
			base.OnAppearing();

			await viewModel.LoadCaptcha();
		}
	}
}