namespace Chemo.Treatment
{
    public abstract class BaseTreatment
    {
        private readonly Logger logger = Logger.Instance;

        /// <summary>
        /// Determines whether this treatment should be applied. This operation should be idempotent.
        /// </summary>
        /// <returns>Returns true if the treatment should be applied, false otherwise.</returns>
        public abstract bool ShouldPerformTreatment();

        /// <summary>
        /// Perform the treatment. This operation can produce side effects or be destructive.
        /// </summary>
        /// <returns>Returns true if the treatment was successful, false otherwise.</returns>
        public abstract bool PerformTreatment();
    }
}
