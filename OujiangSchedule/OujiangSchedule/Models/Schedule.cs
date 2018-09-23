using System;

namespace Tianhai.OujiangApp.Schedule.Models{
    public class Schedule{
        public string Year { get; set; }
        public string Semester { get; set; }
        public Lesson[] Lessons { get; set; }
    }
}