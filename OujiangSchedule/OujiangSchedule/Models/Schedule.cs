﻿using System;
using System.Collections.Generic;

namespace Tianhai.OujiangApp.Schedule.Models{
    public class Schedule{
        public string Year { get; set; }
        public string Semester { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}