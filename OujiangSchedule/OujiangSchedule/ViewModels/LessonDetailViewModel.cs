using System;
using System.Linq;

using Xamarin.Forms;

using Tianhai.OujiangApp.Schedule.Models;

namespace Tianhai.OujiangApp.Schedule.ViewModels{
	public class LessonDetailViewModel:BaseViewModel{
		public Models.Lesson Lesson{get;set;}
		public int SessionMinValue{get;set;}
		public int SessionMaxValue{get;set;}
		public FormattedString LessonTimeLabel{get;set;}
		public string[] DayOfWeekChinese=new string[]{"周日","周一","周二","周三","周四","周五","周六"};
		
		public LessonDetailViewModel(Lesson lesson=null){
			Title=lesson?.Name;
			Lesson=lesson;
			SessionMaxValue=Lesson.Session.Max();
			SessionMinValue=Lesson.Session.Min();

			LessonTimeLabel=new FormattedString();
			LessonTimeLabel.Spans.Add(new Span{Text="从"});
			LessonTimeLabel.Spans.Add(new Span{Text=String.Format("第{0}周",Lesson.Week.Start),FontAttributes=FontAttributes.Bold});
			LessonTimeLabel.Spans.Add(new Span{Text="至"});
			LessonTimeLabel.Spans.Add(new Span{Text=String.Format("第{0}周",Lesson.Week.End),FontAttributes=FontAttributes.Bold});
			if(Lesson.Week.Type!=Enums.WeekType.Undefined){
				LessonTimeLabel.Spans.Add(new Span{Text="的每逢"});
				switch(Lesson.Week.Type){
					case Enums.WeekType.Odd:
						LessonTimeLabel.Spans.Add(new Span{Text="单周",FontAttributes=FontAttributes.Bold});
						break;
					case Enums.WeekType.Even:
						LessonTimeLabel.Spans.Add(new Span{Text="双周",FontAttributes=FontAttributes.Bold});
						break;
				}
			}
			LessonTimeLabel.Spans.Add(new Span{Text="的"});
			LessonTimeLabel.Spans.Add(new Span{Text=String.Format("每{0}",DayOfWeekChinese[(int)Lesson.Day]),FontAttributes=FontAttributes.Bold});
			LessonTimeLabel.Spans.Add(new Span{Text="的"});
			LessonTimeLabel.Spans.Add(new Span{Text=String.Format("第{0}-{1}节",SessionMinValue,SessionMaxValue),FontAttributes=FontAttributes.Bold});
		}
	}
}
