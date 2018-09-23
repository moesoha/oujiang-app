using System;
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

		public LessonListPage(){
			InitializeComponent();

			BindingContext=viewModel=new LessonListViewModel();
		}

		protected override void OnAppearing(){
			base.OnAppearing();

			viewModel.LoadLessonsCommand.Execute(null);
		}
	}
}