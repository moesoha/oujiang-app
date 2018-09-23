using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;
using SQLite;

namespace Tianhai.OujiangApp.Schedule.Models{
    public class Lesson{
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Place { get; set; }
		public DayOfWeek Day { get; set; }
		[OneToMany(CascadeOperations=CascadeOperation.All)]
		public List<int> Session { get; set; }
		[OneToOne]
		public LessonWeek Week { get; set; }
    }
}