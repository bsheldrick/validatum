using System;
using System.Linq.Expressions;

namespace Validatum
{
    public static partial class ValidatorBuilderExtensions
    {
        /// <summary>
        /// Adds a validator to ensure the value is true.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<bool> True(this IValidatorBuilder<bool> builder, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .WhenNot(
                    ctx => ctx.Value,
                    ctx => ctx.AddBrokenRule(nameof(True), key, message ?? "Value must be true.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is true for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        /// <typeparam name="T">The source type.</typeparam>
        public static IValidatorBuilder<T> True<T>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, bool>> selector, 
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

            return builder.For(selector, p => p.True(key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value is false.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<bool> False(this IValidatorBuilder<bool> builder, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => ctx.Value,
                    ctx => ctx.AddBrokenRule(nameof(False), key, message ?? "Value must be false.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is false for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        /// <typeparam name="T">The source type.</typeparam>
        public static IValidatorBuilder<T> False<T>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, bool>> selector, 
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

            return builder.For(selector, p => p.False(key, message));
        }
    }
}