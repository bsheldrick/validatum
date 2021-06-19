using System;
using System.Linq.Expressions;

namespace Validatum
{
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
                .WhenNot(
                    ctx => ctx.Value?.CompareTo(other) > 0,
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
        public static IValidatorBuilder<T> GreaterThan<T, P>(this IValidatorBuilder<T> builder,
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
                .WhenNot(
                    ctx => ctx.Value?.CompareTo(other) >= 0,
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
        public static IValidatorBuilder<T> GreaterThanOrEqual<T, P>(this IValidatorBuilder<T> builder,
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
                .WhenNot(
                    ctx => ctx.Value?.CompareTo(other) < 0,
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
        public static IValidatorBuilder<T> LessThan<T, P>(this IValidatorBuilder<T> builder,
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
                .WhenNot(
                    ctx => ctx.Value?.CompareTo(other) <= 0,
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
        public static IValidatorBuilder<T> LessThanOrEqual<T, P>(this IValidatorBuilder<T> builder,
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

        /// <summary>
        /// Adds a validator to ensure the value is within a specified value range.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="lower">The lower bound range value.</param>
        /// <param name="upper">The upper bound range value.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> Range<T>(this IValidatorBuilder<T> builder,
            T lower,
            T upper,
            string key = null,
            string message = null)
            where T : IComparable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (lower is null)
            {
                throw new ArgumentNullException(nameof(lower));
            }

            if (upper is null)
            {
                throw new ArgumentNullException(nameof(upper));
            }

            return builder
                .WhenNot(
                    ctx => ctx.Value != null && (lower.CompareTo(ctx.Value) <= 0 && upper.CompareTo(ctx.Value) >= 0),
                    ctx => ctx.AddBrokenRule(nameof(Range), key, message ?? $"Value must be in range '{lower.ToString() ?? "null"}' to '{upper.ToString() ?? "null"}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value is within a specified value range for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="lower">The lower bound range value.</param>
        /// <param name="upper">The upper bound range value.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> Range<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, P>> selector,
            P lower,
            P upper,
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

            if (lower is null)
            {
                throw new ArgumentNullException(nameof(lower));
            }

            if (upper is null)
            {
                throw new ArgumentNullException(nameof(upper));
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For(selector, p => p.Range(lower, upper, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure two values from the targets of selector expressions are equal.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="leftSelector">The left selector expression.</param>
        /// <param name="rightSelector">The right selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> Compare<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, P>> leftSelector,
            Expression<Func<T, P>> rightSelector,
            string key = null,
            string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (leftSelector is null)
            {
                throw new ArgumentNullException(nameof(leftSelector));
            }

            if (rightSelector is null)
            {
                throw new ArgumentNullException(nameof(rightSelector));
            }

            var leftFunc = leftSelector.Compile();
            var rightFunc = rightSelector.Compile();

            var leftKey = leftSelector.GetPropertyPath();
            var rightKey = rightSelector.GetPropertyPath();

            return builder.
                WhenNot(
                    ctx => leftFunc(ctx.Value)?.Equals(rightFunc(ctx.Value)) ?? false,
                    ctx => ctx.AddBrokenRule(nameof(Compare), key ?? leftKey, message ?? $"Value must be equal to value of {rightKey}")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value of the target of the first selector expression 
        /// is greater than the value of the target of the second selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="leftSelector">The left selector expression.</param>
        /// <param name="rightSelector">The right selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> CompareGreaterThan<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, P>> leftSelector,
            Expression<Func<T, P>> rightSelector,
            string key = null,
            string message = null)
            where P : IComparable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (leftSelector is null)
            {
                throw new ArgumentNullException(nameof(leftSelector));
            }

            if (rightSelector is null)
            {
                throw new ArgumentNullException(nameof(rightSelector));
            }

            var leftFunc = leftSelector.Compile();
            var rightFunc = rightSelector.Compile();

            var leftKey = leftSelector.GetPropertyPath();
            var rightKey = rightSelector.GetPropertyPath();

            return builder.
                WhenNot(
                    ctx => leftFunc(ctx.Value)?.CompareTo(rightFunc(ctx.Value)) > 0,
                    ctx => ctx.AddBrokenRule(nameof(Compare), key ?? leftKey, message ?? $"Value must be greater than value of {rightKey}")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value of the target of the first selector expression 
        /// is greater than or equal to the value of the target of the second selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="leftSelector">The left selector expression.</param>
        /// <param name="rightSelector">The right selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> CompareGreaterThanOrEqual<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, P>> leftSelector,
            Expression<Func<T, P>> rightSelector,
            string key = null,
            string message = null)
            where P : IComparable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (leftSelector is null)
            {
                throw new ArgumentNullException(nameof(leftSelector));
            }

            if (rightSelector is null)
            {
                throw new ArgumentNullException(nameof(rightSelector));
            }

            var leftFunc = leftSelector.Compile();
            var rightFunc = rightSelector.Compile();

            var leftKey = leftSelector.GetPropertyPath();
            var rightKey = rightSelector.GetPropertyPath();

            return builder.
                WhenNot(
                    ctx => leftFunc(ctx.Value)?.CompareTo(rightFunc(ctx.Value)) >= 0,
                    ctx => ctx.AddBrokenRule(nameof(Compare), key ?? leftKey, message ?? $"Value must be greater than or equal to value of {rightKey}")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value of the target of the first selector expression 
        /// is less than the value of the target of the second selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="leftSelector">The left selector expression.</param>
        /// <param name="rightSelector">The right selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> CompareLessThan<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, P>> leftSelector,
            Expression<Func<T, P>> rightSelector,
            string key = null,
            string message = null)
            where P : IComparable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (leftSelector is null)
            {
                throw new ArgumentNullException(nameof(leftSelector));
            }

            if (rightSelector is null)
            {
                throw new ArgumentNullException(nameof(rightSelector));
            }

            var leftFunc = leftSelector.Compile();
            var rightFunc = rightSelector.Compile();

            var leftKey = leftSelector.GetPropertyPath();
            var rightKey = rightSelector.GetPropertyPath();

            return builder.
                WhenNot(
                    ctx => leftFunc(ctx.Value)?.CompareTo(rightFunc(ctx.Value)) < 0,
                    ctx => ctx.AddBrokenRule(nameof(Compare), key ?? leftKey, message ?? $"Value must be less than value of {rightKey}")
                );
        }

        /// <summary>
        /// Adds a validator to ensure the value of the target of the first selector expression 
        /// is less than or equal to the value of the target of the second selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="leftSelector">The left selector expression.</param>
        /// <param name="rightSelector">The right selector expression.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> CompareLessThanOrEqual<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, P>> leftSelector,
            Expression<Func<T, P>> rightSelector,
            string key = null,
            string message = null)
            where P : IComparable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (leftSelector is null)
            {
                throw new ArgumentNullException(nameof(leftSelector));
            }

            if (rightSelector is null)
            {
                throw new ArgumentNullException(nameof(rightSelector));
            }

            var leftFunc = leftSelector.Compile();
            var rightFunc = rightSelector.Compile();

            var leftKey = leftSelector.GetPropertyPath();
            var rightKey = rightSelector.GetPropertyPath();

            return builder.
                WhenNot(
                    ctx => leftFunc(ctx.Value)?.CompareTo(rightFunc(ctx.Value)) <= 0,
                    ctx => ctx.AddBrokenRule(nameof(Compare), key ?? leftKey, message ?? $"Value must be less than or equal to value of {rightKey}")
                );
        }
    }
}