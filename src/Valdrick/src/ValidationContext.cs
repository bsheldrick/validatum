using System.Collections.Generic;

namespace Valdrick
{
    /// <summary>
    /// Encapsulates validation specific data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ValidationContext<T>
    {
        private readonly List<BrokenRule> _brokenRules = new List<BrokenRule>();

        /// <summary>
        /// Creates a new instance of ValidationContext
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="options">The validation options.</param>   
        public ValidationContext(T value, ValidationOptions options = null)
        {
            Value = value;
            Options = options ?? ValidationOptions.Default;
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
        public string Label { get; internal set; }

        /// <summary>
        /// Indicates whether the validation context can continue processing.
        /// </summary>
        public bool CanContinue => IsValid || (!IsValid && !Options.StopWhenInvalid);

        /// <summary>
        /// Adds a collection of broken rules to the context.
        /// </summary>
        /// <param name="rules">The broken rules to add.</param>
        public void AddBrokenRules(params BrokenRule[] rules)
        {
            _brokenRules.AddRange(rules);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="key"></param>
        /// <param name="error"></param>
        public void AddBrokenRule(string rule, string key, string error)
            => AddBrokenRules(new BrokenRule(rule, key, error));
    }
}