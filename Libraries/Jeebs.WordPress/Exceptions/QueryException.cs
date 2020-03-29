using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.WordPress
{
    /// <summary>
    /// Exception thrown by queries
    /// </summary>

    [Serializable]
    public class QueryException : Exception
    {
        /// <summary>
        /// Create object
        /// </summary>
        public QueryException() { }

        /// <summary>
        /// Create object
        /// </summary>
        /// <param name="message">Message</param>
        public QueryException(string message) : base(message) { }

        /// <summary>
        /// Create object
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="inner">Inner Exception</param>
        public QueryException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Create object
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        protected QueryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
