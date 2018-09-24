using System;

namespace Tianhai.OujiangApp.Schedule.Models{
    public class LessonWeek{
        public int Start { get; set; }
        public int End { get; set; }
        public Enums.WeekType Type { get; set; }
    }
}