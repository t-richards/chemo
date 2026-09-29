using Chemo.Settings;
using Chemo.Utilities;

namespace Chemo.Treatment
{
    /// <summary>
    /// A treatment, made up of settings that are each checked and applied on their own.
    /// </summary>
    internal abstract class SettingsTreatment
    {
        /// <summary>
        /// What happened the last time the treatment was checked or applied, shown in the details pane.
        /// </summary>
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
        /// The settings this treatment puts in place, in the order they are applied.
        /// </summary>
        protected abstract IEnumerable<ISetting> Settings();

        /// <summary>
        /// Called after at least one setting was changed, for treatments that need to tell Windows or running
        /// programs about the change.
        /// </summary>
        protected virtual void OnSettingsChanged()
        {
        }

        /// <summary>
        /// Checks each setting, logging which are already in place and which would change. Changes nothing.
        /// </summary>
        /// <returns>True if any setting needs to change, or couldn't be checked.</returns>
        public bool NeedsApplying()
        {
            bool needsApplying = false;

            foreach (ISetting setting in Settings())
            {
                try
                {
                    if (setting.IsApplied())
                    {
                        Logger.Log("Already set: {0}", setting);
                    }
                    else
                    {
                        Logger.Log("Would set: {0}", setting);
                        needsApplying = true;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Could not check {0}: {1}", setting, ex.Message);
                    needsApplying = true;
                }
            }

            return needsApplying;
        }

        /// <summary>
        /// Puts every setting that isn't already in place in place. A setting that fails is logged, and the rest are
        /// still applied.
        /// </summary>
        /// <returns>True if every setting is now in place.</returns>
        public bool Apply()
        {
            bool succeeded = true;
            bool changed = false;

            foreach (ISetting setting in Settings())
            {
                try
                {
                    if (setting.IsApplied())
                    {
                        Logger.Log("Already set: {0}", setting);
                        continue;
                    }

                    setting.Apply();
                    Logger.Log("Set: {0}", setting);
                    changed = true;
                }
                catch (Exception ex)
                {
                    Logger.Log("Could not set {0}: {1}", setting, ex.Message);
                    succeeded = false;
                }
            }

            if (changed)
            {
                try
                {
                    OnSettingsChanged();
                }
                catch (Exception ex)
                {
                    Logger.Log("Could not finish applying the changes: {0}", ex.Message);
                    succeeded = false;
                }
            }

            return succeeded;
        }

        /// <summary>
        /// Determines whether the treatment's changes are waiting on a restart to finish. Treatments that report this
        /// shouldn't also report that they need applying for the same reason.
        /// </summary>
        public virtual bool RestartPending()
        {
            return false;
        }
    }
}
