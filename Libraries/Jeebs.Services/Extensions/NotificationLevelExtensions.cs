// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
