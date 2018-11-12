using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tianhai.OujiangApp.Schedule.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage:ContentPage{
		ViewModels.LoginViewModel viewModel;

		public LoginPage(){
			InitializeComponent();

			BindingContext=viewModel=new ViewModels.LoginViewModel(Content,Navigation,this);
		}

		protected override async void OnAppearing(){
			base.OnAppearing();

			viewModel.LoadSavedCredential(Content);
			await viewModel.LoadCaptcha();
		}
	}
}