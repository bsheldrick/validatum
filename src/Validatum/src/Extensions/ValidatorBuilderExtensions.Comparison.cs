using System;
using System.Linq.Expressions;

namespace Validatum
{
    /// <summary>
    /// Extension methods for adding validation delegates.
    /// </summary>
    public static partial class ValidatorBuilderExtensions
    {        
        /// <summary>
        /// Adds a validator to ensure the value is greater than a specified value.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="other">The value to test if greater than.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> GreaterThan<T>(this IValidatorBuilder<T> builder, T other, string key = null, string message = null)
            where T : IComparable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => !(ctx.Value?.CompareTo(other) > 0),
                    ctx => ctx.AddBrokenRule(nameof(GreaterThan), key, message ?? $"Value must be greater than '{other?.ToString() ?? "null"}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is greater than a specified value for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="other">The value to test if greater than.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> GreaterThanFor<T, P>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, P>> selector,
            P other, 
            string key = null, 
            string message = null)
            where P : IComparable
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
            
            return builder.For(selector, p => p.GreaterThan(other, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value is greater than or equal to a specified value.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="other">The value to test if greater than or equal.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> GreaterThanOrEqual<T>(this IValidatorBuilder<T> builder, T other, string key = null, string message = null)
            where T : IComparable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => !(ctx.Value?.CompareTo(other) >= 0),
                    ctx => ctx.AddBrokenRule(nameof(GreaterThanOrEqual), key, message ?? $"Value must be greater than or equal to '{other?.ToString() ?? "null"}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is greater than or equal to a specified value 
        /// for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="other">The value to test if greater than.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> GreaterThanOrEqualFor<T, P>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, P>> selector,
            P other, 
            string key = null, 
            string message = null)
            where P : IComparable
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
            
            return builder.For(selector, p => p.GreaterThanOrEqual(other, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value is less than a specified value.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="other">The value to test if less than.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> LessThan<T>(this IValidatorBuilder<T> builder, T other, string key = null, string message = null)
            where T : IComparable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => !(ctx.Value?.CompareTo(other) < 0),
                    ctx => ctx.AddBrokenRule(nameof(LessThan), key, message ?? $"Value must be less than '{other?.ToString() ?? "null"}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is less than a specified value for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="other">The value to test if less than.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> LessThanFor<T, P>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, P>> selector,
            P other, 
            string key = null, 
            string message = null)
            where P : IComparable
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
            
            return builder.For(selector, p => p.LessThan(other, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value is less than or equal to a specified value.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="other">The value to test if less than or equal to.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> LessThanOrEqual<T>(this IValidatorBuilder<T> builder, T other, string key = null, string message = null)
            where T : IComparable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .When(
                    ctx => !(ctx.Value?.CompareTo(other) <= 0),
                    ctx => ctx.AddBrokenRule(nameof(LessThanOrEqual), key, message ?? $"Value must be less than or equal to '{other?.ToString() ?? "null"}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is less than or equal to a specified value for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="other">The value to test if less than or equal to.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> LessThanOrEqualFor<T, P>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, P>> selector,
            P other, 
            string key = null, 
            string message = null)
            where P : IComparable
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
            
            return builder.For(selector, p => p.LessThanOrEqual(other, key, message));
        }
    }
}