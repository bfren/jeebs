// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;

namespace F
{
	/// <summary>
	/// Create <see cref="Option{T}"/> types and begin chains
	/// </summary>
	public static partial class OptionF
	{
		/// <summary>
		/// Exception handler delegate - takes exception and must return a message of type <see cref="IExceptionMsg"/>
		/// </summary>
		/// <param name="e">Exception</param>
		public delegate IExceptionMsg Handler(Exception e);

		/// <summary>
		/// Default exception handler,
		/// it returns <see cref="Msg.UnhandledExceptionMsg"/>
		/// </summary>
		public static Handler DefaultHandler =>
			e => new Msg.UnhandledExceptionMsg(e);

		/// <summary>
		/// Set to log audit exceptions - otherwise they are sent to the Console
		/// </summary>
		public static Action<Exception>? LogAuditExceptions { get; set; }

		internal static void HandleAuditException(Exception e) =>
			HandleAuditException(e, LogAuditExceptions, Console.Out);

		internal static void HandleAuditException(Exception e, Action<Exception>? log, TextWriter writer)
		{
			if (log is not null)
			{
				log(e);
			}
			else
			{
				writer.WriteLine("Audit Error: {0}", e);
			}
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Exception while creating a new object</summary>
			/// <typeparam name="T">The type of the object being created</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record class CreateNewExceptionMsg<T>(Exception Exception) : IExceptionMsg { }

			/// <summary>Unhandled exception</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record class UnhandledExceptionMsg(Exception Exception) : IExceptionMsg { }
		}
	}
}
