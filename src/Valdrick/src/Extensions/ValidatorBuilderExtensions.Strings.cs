using System;
using System.Linq.Expressions;

namespace Valdrick
{
    /// <summary>
    /// Extension methods for adding string validation delegates.
    /// </summary>
    public static partial class ValidatorBuilderExtensions
    {        
        /// <summary>
        /// Adds a validator to ensure the value is not an empty string.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> NotEmpty(this IValidatorBuilder<string> builder, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => string.IsNullOrEmpty(ctx.Value),
                    ctx => ctx.AddBrokenRule(nameof(NotEmpty), key ?? ctx.Label, message ?? "Value cannot be empty.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is not an empty string for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> NotEmptyFor<T>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, string>> selector,
            string key = null,
            string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For<T, string>(selector, p => p.NotEmpty(key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value is an empty string.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> Empty(this IValidatorBuilder<string> builder, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => ctx.Value != string.Empty,
                    ctx => ctx.AddBrokenRule(nameof(Empty), key ?? ctx.Label, message ?? "Value must be empty.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is an empty string for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> EmptyFor<T>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, string>> selector,
            string key = null,
            string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For<T, string>(selector, p => p.Empty(key, message));
        }
    }
}