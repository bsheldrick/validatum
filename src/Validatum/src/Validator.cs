using System;

namespace Validatum
{
    /// <summary>
    /// A class that can validate values of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of data being validated.</typeparam>
    public sealed class Validator<T>
    {
        private readonly ValidatorDelegate<T> _function;

        /// <summary>
        /// Creates a new instance of Validator.
        /// </summary>
        /// <param name="function">The root validator function.</param>
        /// <param name="label">The label to attach to the validator (optional).</param>
        internal Validator(ValidatorDelegate<T> function, string label = null)
        {
            _function = function ?? throw new ArgumentNullException(nameof(function));
            Label = label;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Validates a value against the validator function.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="options">The validation options.</param>
        public ValidationResult Validate(T value, ValidationOptions options = null)
        {
            var context = new ValidationContext<T>(value, Label, options);

            _function(context);

            return new ValidationResult(context.BrokenRules);
        }
    }
}