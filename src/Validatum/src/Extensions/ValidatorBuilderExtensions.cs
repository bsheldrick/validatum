using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Validatum
{
    /// <summary>
    /// Extension methods for adding validator delegates.
    /// </summary>
    public static partial class ValidatorBuilderExtensions
    {
        /// <summary>
        /// Adds a validator function targeting the type <see paramref="P"/> from the source object <see paramref="T"/>.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="func">The validator builder function.</param>
        /// <typeparam name="T">The source type.</typeparam>
        /// <typeparam name="P">The target type.</typeparam>
        public static IValidatorBuilder<T> For<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, P>> selector,
            Action<IValidatorBuilder<P>> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            var label = selector.GetPropertyPath();
            var selectorFunc = selector.Compile();
            var propBuilder = new ValidatorBuilder<P>();
            func(propBuilder);
            var propValidator = propBuilder.Build(label);

            return builder
                .With(ctx =>
                {
                    try
                    {
                        var value = selectorFunc(ctx.Value);
                        var result = propValidator.Validate(value, ctx.Options);

                        ctx.Merge(result);
                    }
                    catch (Exception ex) when (!(ex is ValidationException))
                    {
                        if (ctx.Options.AddBrokenRuleForException)
                        {
                            ctx.AddBrokenRule(ex.GetType().Name, label, ex.Message);
                        }
                        else
                        {
                            throw new ValidationException(ex.Message, ex);
                        }
                    }
                });
        }

        /// <summary>
        /// Adds a validator to validate all items in a collection from the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="func">The validator builder function.</param>
        public static IValidatorBuilder<T> ForEach<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, IEnumerable<P>>> selector,
            Action<IValidatorBuilder<P>> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            var label = selector.GetPropertyPath();
            var selectorFunc = selector.Compile();
            var itemValidatorBuilder = new ValidatorBuilder<P>();
            func(itemValidatorBuilder);
            var itemValidator = itemValidatorBuilder.Build(label);

            return builder
                .With(ctx =>
                {
                    try
                    {
                        var items = selectorFunc(ctx.Value);

                        foreach (var item in items.Select((x, i) => new { value = x, index = i }))
                        {
                            var result = itemValidator.Validate(item.value, ctx.Options);

                            foreach (var brokenRule in result.BrokenRules)
                            {
                                ctx.AddBrokenRule(brokenRule.Rule, $"{brokenRule.Key}[{item.index}]", brokenRule.Message);
                            }
                        }
                    }
                    catch (Exception ex) when (!(ex is ValidationException))
                    {
                        if (ctx.Options.AddBrokenRuleForException)
                        {
                            ctx.AddBrokenRule(ex.GetType().Name, label, ex.Message);
                        }
                        else
                        {
                            throw new ValidationException(ex.Message, ex);
                        }
                    }
                });
        }

        /// <summary>
        /// Adds a validator function that will execute when the predicate resolves to true.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="func">The function to execute.</param>
        public static IValidatorBuilder<T> When<T>(this IValidatorBuilder<T> builder,
            Func<ValidationContext<T>, bool> predicate,
            ValidatorDelegate<T> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return builder
                .With(ctx =>
                {
                    if (predicate(ctx))
                    {
                        func(ctx);
                    }
                });
        }

        /// <summary>
        /// Adds a validator function that will execute when the predicate resolves to false.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="func">The function to execute.</param>
        public static IValidatorBuilder<T> WhenNot<T>(this IValidatorBuilder<T> builder,
            Func<ValidationContext<T>, bool> predicate,
            ValidatorDelegate<T> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return builder
                .With(ctx =>
                {
                    if (!predicate(ctx))
                    {
                        func(ctx);
                    }
                });
        }

        /// <summary>
        /// Executes validation if the predicate function is true.
        /// </summary>
        /// <param name="builder">The valiator builder.</param>
        /// <param name="predicate">The predicate function.</param>
        /// <param name="func">The if builder function.</param>
        public static IValidatorBuilder<T> If<T>(this IValidatorBuilder<T> builder,
            Func<ValidationContext<T>, bool> predicate,
            Action<IValidatorBuilder<T>> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            var ifBuilder = new ValidatorBuilder<T>();
            func(ifBuilder);
            var ifValidator = ifBuilder.Build();

            return builder
                .When(
                    ctx => predicate(ctx),
                    ctx =>
                    {
                        var result = ifValidator.Validate(ctx.Value, ctx.Options);

                        ctx.Merge(result);
                    });
        }

        /// <summary>
        /// Continue executing validation if the current validation context is valid.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="func">The continue builder function.</param>
        public static IValidatorBuilder<T> Continue<T>(this IValidatorBuilder<T> builder, Action<IValidatorBuilder<T>> func)
            => If(builder, ctx => ctx.IsValid, func);

        /// <summary>
        /// Adds an external validator to the validator.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="validator">The external validator.</param>
        public static IValidatorBuilder<T> Validator<T>(this IValidatorBuilder<T> builder, Validator<T> validator)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            return builder
                .With(ctx =>
                {
                    var result = validator.Validate(ctx.Value, ctx.Options);

                    ctx.Merge(result);
                });
        }

        /// <summary>
        /// Adds an external validator to the target of the selector expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="validator">The external validator.</param>
        public static IValidatorBuilder<T> Validator<T, P>(this IValidatorBuilder<T> builder,
            Expression<Func<T, P>> selector,
            Validator<P> validator)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            return builder.For(selector, p => p.Validator(validator));
        }

        /// <summary>
        /// Aggregate broken rules into a single broken rule with the specified message.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="message">The message.</param>
        public static IValidatorBuilder<T> Message<T>(this IValidatorBuilder<T> builder, string message)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message", nameof(message));
            }

            return builder.WhenNot(ctx => ctx.IsValid, ctx => ctx.AggregateBrokenRules(message));
        }

        /// <summary>
        /// Aggregate broken rules into a single broken rule using a function to build and return the message.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="func">The function to build and return a message.</param>
        public static IValidatorBuilder<T> Message<T>(this IValidatorBuilder<T> builder, Func<ValidationContext<T>, string> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return builder.WhenNot(ctx => ctx.IsValid, ctx => ctx.AggregateBrokenRules(func(ctx)));
        }
    }
}