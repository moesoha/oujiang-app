using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tianhai.OujiangApp.Schedule.Views;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace Tianhai.OujiangApp.Schedule{
	public partial class App:Application{
		static Data.CurrentInfoDatabase currentInfoDatabase;

		public App(){
			InitializeComponent();
			
			MainPage=new MainPage();
		}

		public static Data.CurrentInfoDatabase CurrentInfoDatabase{
			get{
				if(currentInfoDatabase==null){
					currentInfoDatabase=new Data.CurrentInfoDatabase(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"CurrentInfo.db"));
				}
				return currentInfoDatabase;
			}
		}

		protected override void OnStart(){
			// Handle when your app starts
		}

		protected override void OnSleep(){
			// Handle when your app sleeps
		}

		protected override void OnResume(){
			// Handle when your app resumes
		}
	}
}
