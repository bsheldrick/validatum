using System;

namespace Validatum
{
    /// <summary>
    /// A builder for constructing validation delegate chains.
    /// </summary>
    /// <typeparam name="T">The type being validated.</typeparam>
    public interface IValidatorBuilder<T>
    {
        /// <summary>
        /// Builds the <see cref="Validator{T}"/>.
        /// </summary>
        /// <param name="label">A label to attach to the validator.</param>
        Validator<T> Build(string label = null);

        /// <summary>
        /// Adds a function to the validator.
        /// </summary>
        /// <param name="func">The function.</param>
        IValidatorBuilder<T> With(Action<ValidationContext<T>, ValidatorDelegate<T>> func);
    }
}