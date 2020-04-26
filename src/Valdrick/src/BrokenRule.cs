using System;

namespace Valdrick
{
    /// <summary>
    /// Represents a broken validation rule.
    /// </summary>
    public class BrokenRule
    {
        /// <summary>
        /// Creates a new instance of BrokenRule.
        /// </summary>
        /// <param name="rule">The name of the broken rule.</param>
        /// <param name="key">The key.</param>
        /// <param name="message">The validation message.</param>
        public BrokenRule(string rule, string key, string message)
        {
            if (string.IsNullOrWhiteSpace(rule))
            {
                throw new ArgumentException("Cannot be null or empty or whitespace.", nameof(rule));
            }

            Rule = rule;
            Key = key;
            Message = message;
        }

        /// <summary>
        /// Gets the broken rule name.
        /// </summary>
        public string Rule { get; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; }
    }
}