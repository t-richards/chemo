using Chemo.Settings;

namespace Chemo.Treatment
{
    /// <summary>
    /// A treatment made up of settings that are each checked and applied on their own.
    /// </summary>
    internal abstract class SettingsTreatment : BaseTreatment
    {
        /// <summary>
        /// The settings this treatment puts in place, in the order they are applied.
        /// </summary>
        /// <returns>The treatment's settings.</returns>
        protected abstract IEnumerable<ISetting> Settings();

        /// <summary>
        /// Called after at least one setting was changed, for treatments that need to tell Windows or running
        /// programs about the change.
        /// </summary>
        protected virtual void OnSettingsChanged()
        {
        }

        public override bool ShouldPerformTreatment()
        {
            bool retval = false;

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
                        retval = true;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Could not check {0}: {1}", setting, ex.Message);
                    retval = true;
                }
            }

            return retval;
        }

        public override bool PerformTreatment()
        {
            bool retval = true;
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
                    retval = false;
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
                    retval = false;
                }
            }

            return retval;
        }
    }
}
