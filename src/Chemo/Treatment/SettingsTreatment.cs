using Chemo.Settings;
using System;
using System.Collections.Generic;

namespace Chemo.Treatment
{
    /// <summary>
    /// A treatment made up of settings that are each checked and applied on their own.
    /// </summary>
    public abstract class SettingsTreatment : BaseTreatment
    {
        /// <summary>
        /// The settings this treatment puts in place, in the order they are applied.
        /// </summary>
        /// <returns>The treatment's settings.</returns>
        protected abstract IEnumerable<ISetting> Settings();

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
                }
                catch (Exception ex)
                {
                    Logger.Log("Could not set {0}: {1}", setting, ex.Message);
                    retval = false;
                }
            }

            return retval;
        }
    }
}
