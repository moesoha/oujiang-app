using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;
using SQLite;

namespace Tianhai.OujiangApp.Schedule.Models{
    public class Lesson{
		[PrimaryKey,AutoIncrement]
		public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Place { get; set; }
		public DayOfWeek Day { get; set; }
		[TextBlob("TextBlobSession")]
		public List<int> Session { get; set; }
		[TextBlob("TextBlobWeek")]
		public LessonWeek Week { get; set; }

		// for TextBlob
		public string TextBlobSession { get; set; }
		public string TextBlobWeek { get; set; }
    }
}