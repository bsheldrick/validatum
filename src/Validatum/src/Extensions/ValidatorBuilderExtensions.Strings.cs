using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Validatum
{
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
                    ctx => ctx.Value == string.Empty,
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
        public static IValidatorBuilder<T> NotEmpty<T>(this IValidatorBuilder<T> builder, 
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
                .WhenNot(
                    ctx => ctx.Value == string.Empty,
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
        public static IValidatorBuilder<T> Empty<T>(this IValidatorBuilder<T> builder, 
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
                .WhenNot(
                    ctx => System.Text.RegularExpressions.Regex.IsMatch(ctx.Value ?? string.Empty, pattern),
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
                .WhenNot(
                    ctx => System.Text.RegularExpressions.Regex.IsMatch(ctx.Value ?? string.Empty, pattern, options),
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
        public static IValidatorBuilder<T> Regex<T>(this IValidatorBuilder<T> builder,
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
        public static IValidatorBuilder<T> Regex<T>(this IValidatorBuilder<T> builder,
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
                .WhenNot(
                    ctx => ctx.Value?.StartsWith(value) ?? false,
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
        public static IValidatorBuilder<T> StartsWith<T>(this IValidatorBuilder<T> builder, 
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
                .WhenNot(
                    ctx => ctx.Value?.EndsWith(value) ?? false,
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
        public static IValidatorBuilder<T> EndsWith<T>(this IValidatorBuilder<T> builder, 
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

        /// <summary>
        /// Adds a validator to ensure the value contains the specified value.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="value">The value the string contains.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> Contains(this IValidatorBuilder<string> builder, string value, string key = null, string message = null)
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
                .WhenNot(
                    ctx => ctx.Value?.Contains(value) ?? false,
                    ctx => ctx.AddBrokenRule(nameof(Contains), key, message ?? $"Value must contain '{value}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value contains the specified for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="value">The value the string contains.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> Contains<T>(this IValidatorBuilder<T> builder, 
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

            return builder.For<T, string>(selector, p => p.Contains(value, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value length is within a specified range.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="min">The minimum length.</param>
        /// <param name="max">The maximum length.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> Length(this IValidatorBuilder<string> builder, int min, int max, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, "Cannot be less than zero.");
            }

            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, $"Cannot be greater than max parameter (max '{max}').");
            }

            return builder
                .When(
                    ctx => ctx.Value is null || ctx.Value?.Length < min || ctx.Value?.Length > max,
                    ctx => ctx.AddBrokenRule(nameof(Length), key, message ?? $"Value must be {min} to {max} characters in length.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value length is within a specified range.
        /// for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="min">The minimum length.</param>
        /// <param name="max">The maximum length.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> Length<T>(this IValidatorBuilder<T> builder,
            Expression<Func<T, string>> selector,
            int min,
            int max,
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

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, "Cannot be less than zero.");
            }

            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, $"Cannot be greater than max parameter (max '{max}').");
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For<T, string>(selector, p => p.Length(min, max, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value is of a minimum length.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="min">The minimum length.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> MinLength(this IValidatorBuilder<string> builder, int min, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, "Cannot be less than zero.");
            }

            return builder
                .When(
                    ctx => ctx.Value is null || ctx.Value?.Length < min,
                    ctx => ctx.AddBrokenRule(nameof(MinLength), key, message ?? $"Value must have minimum length of {min}.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is of a minimum length for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="min">The minimum length.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> MinLength<T>(this IValidatorBuilder<T> builder,
            Expression<Func<T, string>> selector,
            int min,
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

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, "Cannot be less than zero.");
            }

            return builder.For<T, string>(selector, p => p.MinLength(min, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure the value is not longer than maximum length.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="max">The maximum length.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<string> MaxLength(this IValidatorBuilder<string> builder, int max, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, "Cannot be less than zero.");
            }

            return builder
                .When(
                    ctx => ctx.Value is null || ctx.Value?.Length > max,
                    ctx => ctx.AddBrokenRule(nameof(MaxLength), key, message ?? $"Value must have maximum length of {max}.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is of a maximum length for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validation builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="max">The maximum length.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> MaxLength<T>(this IValidatorBuilder<T> builder,
            Expression<Func<T, string>> selector,
            int max,
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

            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, "Cannot be less than zero.");
            }

            return builder.For<T, string>(selector, p => p.MaxLength(max, key, message));
        }
    }
}