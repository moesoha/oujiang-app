using System;
using System.Collections.Generic;
using System.Text;

namespace Tianhai.OujiangApp.Schedule.Services{
	public class DataService{
		public static string urlBase="https://oa.ojc.lohu.info";
		public static string urlGetScheduleCurrent="{0}/schedule/oa/get/{1}/current";
		public static string urlUserLoginStart="{0}/user/oa/start";
		public static string urlUserLoginSubmit="{0}/user/oa/login/{1}";
	}
}
