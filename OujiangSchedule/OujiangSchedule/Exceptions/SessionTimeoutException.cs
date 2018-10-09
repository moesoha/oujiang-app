using System;
using System.Collections.Generic;
using System.Text;

namespace Tianhai.OujiangApp.Schedule.Exceptions{
	[Serializable]
	public class SessionTimeoutException:Exception{
		public SessionTimeoutException() { }
		public SessionTimeoutException(string message) : base(message) { }
		public SessionTimeoutException(string message,Exception inner) : base(message,inner) { }
		protected SessionTimeoutException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context
		) : base(info,context) { }
	}
}
