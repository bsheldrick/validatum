using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Validatum
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
                    ctx => ctx.AddBrokenRule(nameof(NotEmpty), key, message ?? "Value cannot be empty.")
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
                    ctx => ctx.AddBrokenRule(nameof(Empty), key, message ?? "Value must be empty.")
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

        /// <summary>
        /// Adds a validator to ensure the value matches a regular expression pattern.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> Regex(this IValidatorBuilder<string> builder,
            string pattern,
            string key = null,
            string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentException("Cannot be null or empty or whitespace.", nameof(pattern));
            }

            return builder
                .When(
                    ctx => ctx.Value is null || !System.Text.RegularExpressions.Regex.IsMatch(ctx.Value, pattern),
                    ctx => ctx.AddBrokenRule(nameof(Regex), key, message ?? "Value must match pattern.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value matches a regular expression pattern using specified regex options.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The regex options.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> Regex(this IValidatorBuilder<string> builder,
            string pattern,
            RegexOptions options,
            string key = null,
            string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentException("Cannot be null or empty or whitespace.", nameof(pattern));
            }

            return builder
                .When(
                    ctx => ctx.Value is null || !System.Text.RegularExpressions.Regex.IsMatch(ctx.Value, pattern, options),
                    ctx => ctx.AddBrokenRule(nameof(Regex), key, message ?? "Value must match pattern.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value matches a regular expression pattern for the
        /// target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> RegexFor<T>(this IValidatorBuilder<T> builder,
            Expression<Func<T, string>> selector,
            string pattern,
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

            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentException("Cannot be null or empty or whitespace.", nameof(pattern));
            }

            return builder.For(selector, p => p.Regex(pattern, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value matches a regular expression pattern for the
        /// target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="options">The regex options.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> RegexFor<T>(this IValidatorBuilder<T> builder,
            Expression<Func<T, string>> selector,
            string pattern,
            RegexOptions options,
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

            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentException("Cannot be null or empty or whitespace.", nameof(pattern));
            }

            return builder.For(selector, p => p.Regex(pattern, options, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value starts with the specified value.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="value">The value the string starts with.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> StartsWith(this IValidatorBuilder<string> builder, string value, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Cannot be null or empty.", nameof(value));
            }

            return builder
                .When(
                    ctx => !ctx.Value?.StartsWith(value) ?? true,
                    ctx => ctx.AddBrokenRule(nameof(StartsWith), key, message ?? $"Value must start with '{value}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value starts with the specified for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="value">The value the string starts with.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> StartsWithFor<T>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, string>> selector,
            string value,
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

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Cannot be null or empty.", nameof(value));
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For<T, string>(selector, p => p.StartsWith(value, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value ends with the specified value.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="value">The value the string ends with.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> EndsWith(this IValidatorBuilder<string> builder, string value, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Cannot be null or empty.", nameof(value));
            }

            return builder
                .When(
                    ctx => !ctx.Value?.EndsWith(value) ?? true,
                    ctx => ctx.AddBrokenRule(nameof(EndsWith), key, message ?? $"Value must end with '{value}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value ends with the specified for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="value">The value the string ends with.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> EndsWithFor<T>(this IValidatorBuilder<T> builder, 
            Expression<Func<T, string>> selector,
            string value,
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

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Cannot be null or empty.", nameof(value));
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For<T, string>(selector, p => p.EndsWith(value, key, message));
        }
    }
}