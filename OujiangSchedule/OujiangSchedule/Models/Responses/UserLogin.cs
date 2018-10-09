using System;
using System.Collections.Generic;
using System.Text;

namespace Tianhai.OujiangApp.Schedule.Models.Responses{
    public class UserLoginStart{
		public string Token{get;set;}
		public string Captcha{get;set;}
    }
	public class UserLoginSubmit{
		public bool Success{get;set;}
	}
}
