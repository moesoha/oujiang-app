using System;
using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class LoginViewModel:BaseViewModel{
		public ICommand LoginCommand{get;}

		public LoginViewModel(){
			Title="登入";

			LoginCommand=new Command(async ()=>{
				return;
			});
		}

		public async Task LoadToken(){
		}

		private ImageSource _imgCaptcha;
		public ImageSource imgCaptcha{
			get{
				return _imgCaptcha;
			}
			set{
				_imgCaptcha=value;
				OnPropertyChanged();
			}
		}
	}
}