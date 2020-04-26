using System;
using System.Linq.Expressions;

namespace Valdrick
{
    /// <summary>
    /// Extension methods for adding validation delegates.
    /// </summary>
    public static partial class ValidatorBuilderExtensions
    {
        /// <summary>
        /// Adds a validator to ensure the value is not null.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> NotNull<T>(this IValidatorBuilder<T> builder, string key = null, string message = null)
            where T : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => ctx.Value is null, 
                    ctx => ctx.AddBrokenRules(new BrokenRule(nameof(NotNull), key ?? ctx.Label, message ?? "Value cannot be null."))
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is not null for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        /// <typeparam name="T">The source type.</typeparam>
        /// <typeparam name="P">The target type.</typeparam>
        public static IValidatorBuilder<T> NotNullFor<T, P>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, P>> selector, 
            string key = null,
            string message = null)
            where T : class
            where P : class
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

            return builder.For(selector, p => p.NotNull(key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value is null.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> Null<T>(this IValidatorBuilder<T> builder, string key = null, string message = null)
            where T : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => ctx.Value != null, 
                    ctx => ctx.AddBrokenRules(new BrokenRule(nameof(Null), key ?? ctx.Label, message ?? "Value must be null."))
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is null for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        /// <typeparam name="T">The source type.</typeparam>
        /// <typeparam name="P">The target type.</typeparam>
        public static IValidatorBuilder<T> NullFor<T, P>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, P>> selector, 
            string key = null,
            string message = null)
            where T : class
            where P : class
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

            return builder.For(selector, p => p.Null(key, message));
        }
    }
}