namespace Chemo.Treatment
{
    public interface ITreatment
    {
        /// <summary>
        /// Determines whether this treatment should be applied. This operation should be idempotent.
        /// </summary>
        /// <returns>Returns true if the treatment should be applied, false otherwise.</returns>
        bool ShouldPerformTreatment();

        /// <summary>
        /// Perform the treatment. This operation can produce side effects or be destructive.
        /// </summary>
        /// <returns>Returns true if the treatment was successful, false otherwise.</returns>
        bool PerformTreatment();
    }
}
