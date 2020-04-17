using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jx
{
    /// <summary>
    /// Result Exception
    /// </summary>
    public class ResultException : Exception
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public ResultException() { }

        /// <summary>
        /// Construct exception
        /// </summary>
        /// <param name="message">Message</param>
        public ResultException(string message) : base(message) { }

        /// <summary>
        /// Construct exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="errors">Errors</param>
        public ResultException(string message, IErrorList errors) : base(message, new Exception(errors.ToString())) { }

        /// <summary>
        /// Construct exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="inner">Inner exception</param>
        public ResultException(string message, Exception inner) : base(message, inner) { }
    }
}
