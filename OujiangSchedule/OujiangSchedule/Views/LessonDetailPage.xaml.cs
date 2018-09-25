using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Tianhai.OujiangApp.Schedule.Models;
using Tianhai.OujiangApp.Schedule.ViewModels;

namespace Tianhai.OujiangApp.Schedule.Views{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LessonDetailPage:ContentPage{
		LessonDetailViewModel viewModel;

		public LessonDetailPage(LessonDetailViewModel viewModel){
			InitializeComponent();

			BindingContext=this.viewModel=viewModel;
		}
	}
}