using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Validatum
{
    /// <summary>
    /// Encapsulates data collected during validation.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Creates a new instance of ValidationResult.
        /// </summary>
        /// <param name="brokenRules">The broken rules.</param>
        public ValidationResult(IEnumerable<BrokenRule> brokenRules)
        {
            BrokenRules = new ReadOnlyCollection<BrokenRule>((brokenRules ?? Enumerable.Empty<BrokenRule>()).ToList());
        }

        /// <summary>
        /// Gets the collection of broken rules.
        /// </summary>
        public IReadOnlyCollection<BrokenRule> BrokenRules { get; }

        /// <summary>
        /// Gets a value indicating if the result is valid.
        /// </summary>
        public bool IsValid => BrokenRules.Count() == 0;
    }
}