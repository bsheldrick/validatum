using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Validatum
{
    public static partial class ValidatorBuilderExtensions
    {
        /// <summary>
        /// Adds a validator to ensure a collection has a specified item count.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="count">The item count.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<IEnumerable<T>> Count<T>(this IValidatorBuilder<IEnumerable<T>> builder, int count, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot be less than zero");
            }

            return builder
                .WhenNot(
                    ctx => ctx.Value.Count() == count,
                    ctx => ctx.AddBrokenRule(nameof(Count), key ?? $"IEnumerable<{typeof(T).Name}>", message ?? $"Collection count must equal {count}.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure a collection has a specified item count for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="count">The item count.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> Count<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, IEnumerable<P>>> selector,
            int count,
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

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot be less than zero");
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For<T, IEnumerable<P>>(selector, p => p.Count(count, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure a collection has a specified minimum item count.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="count">The minimum item count.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<IEnumerable<T>> MinCount<T>(this IValidatorBuilder<IEnumerable<T>> builder, int count, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot be less than zero");
            }

            return builder
                .When(
                    ctx => ctx.Value.Count() < count,
                    ctx => ctx.AddBrokenRule(nameof(MinCount), key ?? $"IEnumerable<{typeof(T).Name}>", message ?? $"Collection count must be at least {count}.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure a collection has a specified minimum item count for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="count">The minimum item count.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> MinCount<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, IEnumerable<P>>> selector,
            int count,
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

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot be less than zero");
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For<T, IEnumerable<P>>(selector, p => p.MinCount(count, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure a collection has a specified maximum item count.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="count">The maximum item count.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<IEnumerable<T>> MaxCount<T>(this IValidatorBuilder<IEnumerable<T>> builder, int count, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot be less than zero");
            }

            return builder
                .When(
                    ctx => ctx.Value.Count() > count,
                    ctx => ctx.AddBrokenRule(nameof(MaxCount), key ?? $"IEnumerable<{typeof(T).Name}>", message ?? $"Collection count cannot be greater than {count}.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure a collection has a specified maximum item count for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="count">The maximum item count.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> MaxCount<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, IEnumerable<P>>> selector,
            int count,
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

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot be less than zero");
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For<T, IEnumerable<P>>(selector, p => p.MaxCount(count, key, message));
        }

        /// <summary>
        /// Adds a validator to ensure a collection contains a specified item.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="item">The item.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<IEnumerable<T>> Contains<T>(this IValidatorBuilder<IEnumerable<T>> builder, T item, string key = null, string message = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return builder
                .WhenNot(
                    ctx => ctx.Value?.Contains(item) ?? false,
                    ctx => ctx.AddBrokenRule(nameof(Contains), key ?? $"IEnumerable<{typeof(T).Name}>", message ?? $"Collection must contain item '{item.ToString()}'.")
                );
        }

        /// <summary>
        /// Adds a validator to ensure a collection has a specified item for the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="item">The item.</param>
        /// <param name="key">The key to use in broken rule.</param>
        /// <param name="message">The message to use in broken rule.</param>
        public static IValidatorBuilder<T> Contains<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, IEnumerable<P>>> selector,
            P item,
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

            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            key = key ?? selector.GetPropertyPath();

            return builder.For<T, IEnumerable<P>>(selector, p => p.Contains(item, key, message));
        }
    }
}