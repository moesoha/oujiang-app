using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tianhai.OujiangApp.Schedule.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage:ContentPage{
		ViewModels.LoginViewModel viewModel;

		public LoginPage(){
			InitializeComponent();

			BindingContext=viewModel=new ViewModels.LoginViewModel();
		}
		
		private bool _btnLoginIsEnabled=true;
		public bool btnLoginIsEnabled{
			get{
				return _btnLoginIsEnabled;
			}
			set{
				_btnLoginIsEnabled=value;
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