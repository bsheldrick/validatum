using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Valdrick
{
    /// <summary>
    /// Extension methods for Expression functions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Gets the path of the given selector expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The source type.</typeparam>
        /// <typeparam name="P">The target type.</typeparam>
        public static string GetPropertyPath<T, P>(this Expression<Func<T, P>> expression)
        {
            MemberExpression memberExpression = null;

            // first try extract member expression from unary type
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                if (expression.Body is UnaryExpression unary)
                {
                    memberExpression = unary.Operand as MemberExpression;
                }
            }

            // try a straight cast of the body
            if (memberExpression is null)
            {
                memberExpression = expression.Body as MemberExpression;
            }

            // still null throw exception
            if (memberExpression is null)
            {
                throw new NotSupportedException("Expression should be in format x => x.Property.");
            }
            
            return GetMemberPath(memberExpression);
        }

        private static string GetMemberPath(MemberExpression expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var parts = new List<string>();

            while (expression != null)
            {
                parts.Add(expression.Member.Name);
                expression = expression.Expression as MemberExpression;
            }

            parts.Reverse();
            
            return string.Join(".", parts);
        }
    }
}