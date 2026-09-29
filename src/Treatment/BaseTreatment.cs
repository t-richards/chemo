using Chemo.Utilities;
namespace Chemo.Treatment
{
    internal abstract class BaseTreatment
    {
        public MemoryLogger Logger { get; } = new();

        /// <summary>
        /// The treatment's name in the list.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// What the treatment does, shown as its tooltip and in the details pane.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Determines whether the treatment should be applied. This operation should be idempotent.
        /// </summary>
        /// <returns>Returns true if the treatment should be applied, false otherwise.</returns>
        public abstract bool ShouldPerformTreatment();

        /// <summary>
        /// Determines whether the treatment's changes are waiting on a restart to finish. Treatments that
        /// report this should not also report that they need to be applied for the same reason.
        /// </summary>
        /// <returns>Returns true if a restart is needed to finish the treatment, false otherwise.</returns>
        public virtual bool RestartPending()
        {
            return false;
        }

        /// <summary>
        /// Perform the treatment. This operation can produce side effects or be destructive.
        /// </summary>
        /// <returns>Returns true if the treatment was successful, false otherwise.</returns>
        public abstract bool PerformTreatment();
    }
}
