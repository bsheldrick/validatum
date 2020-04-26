namespace Valdrick
{
    /// <summary>
    /// A function that processes validation logic.
    /// </summary>
    /// <param name="context">The validation context.</param>
    /// <typeparam name="T">The type being validated.</typeparam>
    public delegate void ValidatorDelegate<T>(ValidationContext<T> context);
}