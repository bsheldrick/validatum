namespace Valdrick
{
    /// <summary>
    /// Encapsulates options used for validation.
    /// </summary>
    public class ValidationOptions
    {
        /// <summary>
        /// Creates a new instance of ValidationOptions.
        /// </summary>
        /// <param name="stopWhenInvalid">The option to stop when invalid.</param>
        /// <param name="addBrokenRuleForException">The option to add a broken rule an exception occurs.</param>
        public ValidationOptions(bool stopWhenInvalid = false, bool addBrokenRuleForException = true)
        {
            StopWhenInvalid = stopWhenInvalid;
            AddBrokenRuleForException = addBrokenRuleForException;
        }

        /// <summary>
        /// A default instance of ValidationOptions.
        /// </summary>
        public static readonly ValidationOptions Default = new ValidationOptions();

        /// <summary>
        /// Indicates to stop validation when the first invalid rule occurs.
        /// </summary>
        public bool StopWhenInvalid { get; }

        /// <summary>
        /// Indicates to add exceptions as broken rules when they occur.
        /// </summary>
        public bool AddBrokenRuleForException { get; }
    }
}