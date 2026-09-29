namespace Chemo.Settings
{
    /// <summary>
    /// A single change made by a treatment, which is either in place or not.
    /// </summary>
    internal interface ISetting
    {
        /// <summary>
        /// Determines whether the setting is already in place. This operation should be idempotent.
        /// </summary>
        /// <returns>Returns true if nothing needs to change, false otherwise.</returns>
        bool IsApplied();

        /// <summary>
        /// Puts the setting in place. Throws if the setting could not be applied.
        /// </summary>
        void Apply();
    }
}
