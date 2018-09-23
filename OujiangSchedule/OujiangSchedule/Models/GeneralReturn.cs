using System;

namespace Tianhai.OujiangApp.Schedule.Models{
	public class GeneralReturn<T>{
		public int Status { get; set; }
		public string Return { get; set; }
		public T Data { get; set; }
	}
}
