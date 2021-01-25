using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Jeebs.Services
{
	/// <summary>
	/// LogLevel Extensions - ToMessageLevel
	/// </summary>
	public static class NotificationLevelExtensions
	{
		/// <summary>
		/// Convert a <see cref="LogLevel"/> to a <see cref="NotificationLevel"/>
		/// </summary>
		/// <param name="this"></param>
		public static NotificationLevel ToNotificationLevel(this LogLevel @this) =>
			@this switch
			{
				LogLevel.Warning =>
					NotificationLevel.Warning,

				LogLevel.Error =>
					NotificationLevel.Error,

				LogLevel.Critical =>
					NotificationLevel.Error,

				_ =>
					NotificationLevel.Information
			};
	}
}
