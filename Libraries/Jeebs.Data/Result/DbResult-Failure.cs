using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public static partial class DbResult
	{
		/// <summary>
		/// Simple structure for safely performing operations and returning a successful result, or a list of errors
		/// Shows that the operation resulted in one or more error(s)
		/// </summary>
		public class DbFailure<T> : Failure<T>, IDbResult<T>
		{
			/// <summary>
			/// Create from a predefined list
			/// </summary>
			/// <param name="errors">List of errors</param>
			internal DbFailure(in List<string> errors) : base(errors) { }

			/// <summary>
			/// Create from a parameterised array
			/// </summary>
			/// <param name="errors">Array of errors</param>
			internal DbFailure(params string[] errors) : base(errors) { }
		}

		/// <summary>
		/// Simple structure for safely performing operations and returning a successful result, or a list of errors
		/// Special case - to be used instead of a return type of 'bool' for success / failure
		/// </summary>
		public sealed class DbFailure : DbFailure<bool>, IDbResult
		{
			/// <summary>
			/// Create from a predefined list
			/// </summary>
			/// <param name="errors">List of errors</param>
			internal DbFailure(in List<string> errors) : base(errors) { }

			/// <summary>
			/// Create from a parameterised array
			/// </summary>
			/// <param name="errors">Array of errors</param>
			internal DbFailure(params string[] errors) : base(errors) { }
		}

		/// <summary>
		/// Simple structure for safely performing operations and returning a successful result, or a list of errors
		/// Special case - to be used instead of a return type of 'bool' for success / failure
		/// </summary>
		public sealed class DbFailureConcurrency<T> : DbFailure<bool>, IDbResult
		{
			/// <summary>
			/// Create from a predefined list
			/// </summary>
			/// <param name="errors">List of errors</param>
			internal DbFailureConcurrency(in List<string> errors) : base(errors) { }

			/// <summary>
			/// Create from a parameterised array
			/// </summary>
			/// <param name="errors">Array of errors</param>
			internal DbFailureConcurrency(params string[] errors) : base(errors) { }
		}
	}
}