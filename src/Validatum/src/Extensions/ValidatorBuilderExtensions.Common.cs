using System;
using System.Linq.Expressions;

namespace Validatum
{
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
                    ctx => ctx.AddBrokenRule(nameof(NotNull), key, message ?? "Value cannot be null.")
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
        public static IValidatorBuilder<T> NotNull<T, P>(this IValidatorBuilder<T> builder, 
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
                .WhenNot(
                    ctx => ctx.Value is null,
                    ctx => ctx.AddBrokenRule(nameof(Null), key, message ?? "Value must be null.")
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
        public static IValidatorBuilder<T> Null<T, P>(this IValidatorBuilder<T> builder, 
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

        /// <summary>
        /// Adds a validator to ensure the value is equal to a specified value.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="other">The value to test equality.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> Equal<T>(this IValidatorBuilder<T> builder, T other, string key = null, string message = null)
            where T : IEquatable<T>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .WhenNot(
                    ctx => (ctx.Value is null && other is null) || (ctx.Value?.Equals(other) ?? false),
                    ctx => ctx.AddBrokenRule(nameof(Equal), key, message ?? $"Value must equal '{other?.ToString() ?? "null"}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is equal to a specified value for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="other">The value to test equality.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        /// <typeparam name="T">The source type.</typeparam>
        /// <typeparam name="P">The target type.</typeparam>
        public static IValidatorBuilder<T> Equal<T, P>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, P>> selector,
            P other,
            string key = null,
            string message = null)
            where P : IEquatable<P>
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

            return builder.For(selector, p => p.Equal(other, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value is not equal to a specified value.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="other">The value to test equality.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> NotEqual<T>(this IValidatorBuilder<T> builder, T other, string key = null, string message = null)
            where T : IEquatable<T>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => (ctx.Value is null && other is null) || (ctx.Value?.Equals(other) ?? false),
                    ctx => ctx.AddBrokenRule(nameof(NotEqual), key, message ?? $"Value must not equal '{other?.ToString() ?? "null"}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is not equal to a specified value for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="other">The value to test equality.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        /// <typeparam name="T">The source type.</typeparam>
        /// <typeparam name="P">The target type.</typeparam>
        public static IValidatorBuilder<T> NotEqual<T, P>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, P>> selector,
            P other,
            string key = null,
            string message = null)
            where P : IEquatable<P>
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

            return builder.For(selector, p => p.NotEqual(other, key, message));
        }
    }
}