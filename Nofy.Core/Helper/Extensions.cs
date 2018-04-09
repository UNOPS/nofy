namespace Nofy.Core.Helper
{
    /// <summary>
    /// Extensions functions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Will truncate the string if it has more character than the limit
        /// </summary>
        /// <param name="text"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string EnsureLength(this string text, int limit)
        {
            return text.Length > limit ? text.Substring(0, limit) : text;
        }
    }
}