using System;
using System.Collections.Generic;
using System.Linq;

namespace Validatum
{
    /// <summary>
    /// Encapsulates validation specific data.
    /// </summary>
    /// <typeparam name="T">The type being validated.</typeparam>
    public sealed class ValidationContext<T>
    {
        private readonly List<BrokenRule> _brokenRules = new List<BrokenRule>();

        /// <summary>
        /// Creates a new instance of ValidationContext
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="label">The label.</param>
        /// <param name="options">The validation options.</param>   
        public ValidationContext(T value, string label = null, ValidationOptions options = null)
        {
            Value = value;
            Label = label;
            Options = options ?? new ValidationOptions();
            Options.Lock();
        }

        /// <summary>
        /// Gets the value being validated.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the validation options.
        /// </summary>
        public ValidationOptions Options { get; }

        /// <summary>
        /// Gets the broken rules collection.
        /// </summary>
        public IEnumerable<BrokenRule> BrokenRules => _brokenRules;

        /// <summary>
        /// Gets a value indicating whether the context is in a valid state.
        /// </summary>
        public bool IsValid => _brokenRules.Count == 0;

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Indicates whether the validation context can continue processing.
        /// </summary>
        public bool CanContinue => IsValid || (!IsValid && !Options.StopWhenInvalid);

        /// <summary>
        /// Merges a validation result into this context.
        /// </summary>
        /// <param name="result">The validation result.</param>
        public void Merge(ValidationResult result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            foreach (var rule in result.BrokenRules)
            {
                AddBrokenRule(rule.Rule, rule.Key, rule.Message);
            }
        }

        /// <summary>
        /// Adds a broken rule to the context.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="key">The key.</param>
        /// <param name="message">The message.</param>
        public void AddBrokenRule(string rule, string key, string message)
            => _brokenRules.Add(new BrokenRule(rule, key ?? Label, message));

        internal void AggregateBrokenRules(string message)
        {
            if (!IsValid)
            {
                var rules = string.Join(",", _brokenRules.Select(r => r.Rule));

                _brokenRules.Clear();

                AddBrokenRule(rules, Label, message);               
            }
        }
    }
}