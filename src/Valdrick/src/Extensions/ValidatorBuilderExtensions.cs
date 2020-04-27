using System;
using System.Linq;
using System.Linq.Expressions;

namespace Valdrick
{
    /// <summary>
    /// Extension methods for adding validation delegates.
    /// </summary>
    public static partial class ValidatorBuilderExtensions
    {
        /// <summary>
        /// Adds a delegate to the validator.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="func">The delegate function.</param>
        public static IValidatorBuilder<T> With<T>(this IValidatorBuilder<T> builder, ValidatorDelegate<T> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return builder
                .With((ctx, next) => 
                {
                    func(ctx);

                    if (ctx.CanContinue)
                    {
                        next(ctx);
                    }
                });
        }

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

                        ctx.AddBrokenRules(result.BrokenRules.ToArray());
                    }
                    catch (NullReferenceException ex) when (ctx.Options.AddBrokenRuleForException)
                    {
                        ctx.AddBrokenRule(nameof(NullReferenceException), label, ex.Message);
                    }
                    catch {}
                });
        }

        /// <summary>
        /// Adds a validator function that will execute when the predicate resolves to true.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="func">The function to execute.</param>
        public static IValidatorBuilder<T> When<T>(this IValidatorBuilder<T> builder, Func<ValidationContext<T>, bool> predicate, ValidatorDelegate<T> func)
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
        /// Executes the delegate function when <see cref="ValidationContext{T}.IsValid"/> is true.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="func">The function.</param>
        public static IValidatorBuilder<T> WhenValid<T>(this IValidatorBuilder<T> builder, ValidatorDelegate<T> func)
            => When(builder, ctx => ctx.IsValid, func);

        /// <summary>
        /// Executes the delegate function when <see cref="ValidationContext{T}.IsValid"/> is false.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="func">The function.</param>
        public static IValidatorBuilder<T> WhenInvalid<T>(this IValidatorBuilder<T> builder, ValidatorDelegate<T> func)
            => When(builder, ctx => !ctx.IsValid, func);

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

                    ctx.AddBrokenRules(result.BrokenRules.ToArray());
                });
        }

        /// <summary>
        /// Adds an external validator to the target of the selected expression.
        /// </summary>
        /// <param name="builder">The validator builder.</param>
        /// <param name="selector">The selector expression.</param>
        /// <param name="validator">The external validator.</param>
        public static IValidatorBuilder<T> ValidatorFor<T, P>(this IValidatorBuilder<T> builder, 
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
    }
}