using System;
using System.Collections.Generic;

namespace Validatum
{
    /// <summary>
    /// Default implementation for <see cref="IValidatorBuilder{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type being validated.</typeparam>
    public sealed class ValidatorBuilder<T> : IValidatorBuilder<T>
    {
        private readonly Stack<Func<ValidatorDelegate<T>, ValidatorDelegate<T>>> _delegates 
            = new Stack<Func<ValidatorDelegate<T>, ValidatorDelegate<T>>>();

        private Validator<T> _validator;

        /// <inheritdoc/>
        public Validator<T> Build(string label = null)
        {
            if (_validator is null)
            {
                ValidatorDelegate<T> validator = ctx => {};

                while (_delegates.Count > 0)
                {
                    var next = _delegates.Pop();
                    validator = next(validator);
                }

                _validator = new Validator<T>(validator, label ?? typeof(T).Name);
            }

            return _validator;
        }

        /// <inheritdoc/>
        public IValidatorBuilder<T> With(Action<ValidationContext<T>, ValidatorDelegate<T>> func)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            _delegates.Push(next => ctx => 
            {
                if (ctx.CanContinue)
                {
                    func(ctx, next);
                }
            });

            return this;
        }
    }
}