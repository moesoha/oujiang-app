﻿using System;

namespace Tianhai.OujiangApp.Schedule.Models{
    public class Lesson{
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Place { get; set; }
		public DayOfWeek Day { get; set; }
		public int[] Session { get; set; }
		public LessonWeek Week { get; set; }
    }
}