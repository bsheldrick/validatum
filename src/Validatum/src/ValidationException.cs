using System;
using System.Collections.Generic;

namespace Validatum
{
    /// <summary>
    /// Represents errors that occur during validation execution.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Creates a new instance of ValidationException containing broken rules.
        /// </summary>
        /// <param name="brokenRules">The broken rules.</param>
        public ValidationException(IEnumerable<BrokenRule> brokenRules)
        {
            BrokenRules = brokenRules;
        }

        /// <summary>
        /// Creates a new instance of ValidationException with an error message and 
        /// an inner exception that caused this exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Gets the broken rules.
        /// </summary>
        public IEnumerable<BrokenRule> BrokenRules { get; }
    }
}