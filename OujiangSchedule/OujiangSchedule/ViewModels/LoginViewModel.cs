using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

using Xamarin.Forms;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class LoginViewModel:BaseViewModel{
		public LoginViewModel(View PageContent,INavigation Navigation,Page Page){
			Title="登入";

			LoginCommand=new Command(async ()=>{
				string username=PageContent.FindByName<Entry>("username").Text;
				string password=PageContent.FindByName<Entry>("password").Text;
				string captcha=PageContent.FindByName<Entry>("captcha").Text;

				bool r=await Login(username,password,captcha);
				if(r){
					SaveCredential(PageContent);
					await Page.DisplayAlert("登入成功","你现在可以去更新课表了。","好的");
					await Navigation.PopAsync();
				}else{
					await LoadCaptcha();
				}
				return;
			});
			ReloadCaptchaCommand=new Command(async ()=>await LoadCaptcha());
		}
		
		public void LoadSavedCredential(View PageContent){
			var savedCredential=App.PreferenceDatabase.GetOACredential();
			if(savedCredential==null){
				return;
			}
			PageContent.FindByName<Entry>("username").Text=savedCredential.Username;
			PageContent.FindByName<Entry>("password").Text=savedCredential.Password;
		}
		
		public void SaveCredential(View PageContent){
			var savedCredential=new Models.Preferences.OACredential{
				Username=PageContent.FindByName<Entry>("username").Text,
				Password=PageContent.FindByName<Entry>("password").Text
			};
			App.PreferenceDatabase.SetOACredential(savedCredential);
		}

		public async Task LoadCaptcha(){
			lblHintVisible=false;
			btnReloadCaptchaIsEnabled=false;
			string temp=btnReloadCaptchaText;
			btnReloadCaptchaText="正在加载验证码";
			
			try{
				string captchaDataurl=await Services.UserService.loginStartWithCaptcha();
				var captchaBase64=Regex.Match(captchaDataurl, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
				var captchaBinary=Convert.FromBase64String(captchaBase64);
				imgCaptcha=ImageSource.FromStream(()=>new MemoryStream(captchaBinary));
			}catch(Exception e){
				lblHintText=String.Format("验证码加载失败，可能是网络状况不佳。{0}",e.Message);
				lblHintVisible=true;
			}

			btnReloadCaptchaIsEnabled=true;
			btnReloadCaptchaText=temp;
		}

		public async Task<bool> Login(string username,string password,string captcha){
			btnLoginIsEnabled=false;
			lblHintVisible=false;
			actidctIsRunning=true;
			bool result=false;

			try{
				result=await Services.UserService.loginSubmit(username,password,captcha);
				if(!result){
					lblHintText="用户名/密码或验证码出错，请检查。";
					lblHintVisible=true;
				}else{
					lblHintText="登入成功";
					//lblHintVisible=true;
				}
			}catch(Exceptions.SessionTimeoutException){
				lblHintText="验证码过期，请刷新。";
				lblHintVisible=true;
			}catch(Exception e){
				lblHintText=String.Format("遇到未知错误，可能是网络状况不佳。{0}",e.Message);
				lblHintVisible=true;
			}

			actidctIsRunning=false;
			btnLoginIsEnabled=true;

			return result;
		}

		public ICommand LoginCommand{get;}
		public ICommand ReloadCaptchaCommand{get;}
		
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

		private bool _btnReloadCaptchaIsEnabled=true;
		public bool btnReloadCaptchaIsEnabled{
			get{
				return _btnReloadCaptchaIsEnabled;
			}
			set{
				_btnReloadCaptchaIsEnabled=value;
				OnPropertyChanged();
			}
		}
		private string _btnReloadCaptchaText="刷新验证码";
		public string btnReloadCaptchaText{
			get{
				return _btnReloadCaptchaText;
			}
			set{
				_btnReloadCaptchaText=value;
				OnPropertyChanged();
			}
		}

		private bool _lblHintVisible=false;
		private string _lblHintText="";
		public bool lblHintVisible{
			get{
				return _lblHintVisible;
			}
			set{
				_lblHintVisible=value;
				OnPropertyChanged();
			}
		}
		public string lblHintText{
			get{
				return _lblHintText;
			}
			set{
				_lblHintText=value;
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