using Chemo.Controls;
using Chemo.Treatment;
using Chemo.Utilities;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Chemo
{
    internal partial class MainForm : Form
    {
        private readonly Font categoryFont;
        private readonly string fullVersion;
        private bool cascadingChecks;

        public MainForm()
        {
            InitializeComponent();

            categoryFont = new(lstTreatments.Font, FontStyle.Bold);
            lstTreatments.SmallImageList = StatusIcons.Create(lstTreatments.LogicalToDeviceUnits(16), dark: false);

            // The full version ends with "+" and the commit it was built from, which is too long for the menu but
            // useful in bug reports, so the menu shows the version number and clicking it copies the rest.
            fullVersion = typeof(MainForm).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            versionToolStripMenuItem.Text = $"Version {fullVersion.Split('+')[0]}";
            versionToolStripMenuItem.ToolTipText = $"Copy {fullVersion}";
            helpToolStripMenuItem.DropDown.ShowItemToolTips = true;

            SetMenuIcon(versionToolStripMenuItem, StatusIcons.CopyGlyph);
            SetMenuIcon(githubToolStripMenuItem, StatusIcons.OpenInNewWindowGlyph);

            InitTreatments();
            ShowDetails();
        }

        /// <summary>
        /// Gives a menu item an icon drawn for the display's scale, so it isn't stretched.
        /// </summary>
        private void SetMenuIcon(ToolStripMenuItem item, string glyph)
        {
            item.Image = StatusIcons.DrawGlyph(glyph, LogicalToDeviceUnits(16), SystemColors.MenuText);
            item.ImageScaling = ToolStripItemImageScaling.None;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // While its handle is created, the list reports every checked item as newly checked, before all of the
            // items are in place. Only follow check changes once the list is ready.
            lstTreatments.ItemChecked += LstTreatments_ItemChecked;
        }

        public void InitTreatments()
        {
            lstTreatments.Items.Clear();

            foreach (Category category in TreatmentCatalog.Categories)
            {
                CategoryItem categoryItem = new(category, categoryFont);
                lstTreatments.Items.Add(categoryItem);

                foreach (BaseTreatment treatment in category.Treatments)
                {
                    TreatmentItem treatmentItem = new(treatment, categoryItem);
                    categoryItem.Treatments.Add(treatmentItem);
                    lstTreatments.Items.Add(treatmentItem);
                }
            }
        }

        private async void BtnAnalyze_Click(object sender, EventArgs e)
        {
            List<TreatmentItem> treatments = CheckedTreatments();
            if (!BeginRun(treatments))
            {
                return;
            }

            Stopwatch overallTime = Stopwatch.StartNew();

            foreach (TreatmentItem item in treatments)
            {
                lblStatus.Text = $"Analyzing {item.Text}…";
                item.SetStatus(TreatmentStatus.NotStarted, "Analyzing…");

                (TreatmentStatus status, TimeSpan duration) = await RunStep(item.Treatment, Analyze);
                string text = status switch
                {
                    TreatmentStatus.Info => "Ready to apply",
                    TreatmentStatus.Restart => "Restart to finish",
                    TreatmentStatus.Error => "Couldn't analyze, see details",
                    _ => "Already applied",
                };
                item.SetStatus(status, text, duration);
                RefreshDetails(item);

                prgTreatmentApplication.Value += 1;
            }

            int readyCount = CountStatus(treatments, TreatmentStatus.Info);
            int restartCount = CountStatus(treatments, TreatmentStatus.Restart);
            int failedCount = CountStatus(treatments, TreatmentStatus.Error);

            string summary = $"Analyzed {Count(treatments.Count, "treatment")} in {Durations.Humanize(overallTime.Elapsed)}. ";
            summary += readyCount == 0 ? "Nothing needs to be applied." : $"{readyCount} ready to apply.";
            if (restartCount > 0)
            {
                summary += $" {restartCount} waiting for a restart.";
            }
            if (failedCount > 0)
            {
                summary += $" {failedCount} couldn't be analyzed.";
            }
            EndRun(summary);
        }

        private async void BtnInitiateTreatment_Click(object sender, EventArgs e)
        {
            List<TreatmentItem> treatments = CheckedTreatments();
            if (!BeginRun(treatments))
            {
                return;
            }

            Stopwatch overallTime = Stopwatch.StartNew();

            foreach (TreatmentItem item in treatments)
            {
                lblStatus.Text = $"Applying {item.Text}…";
                item.SetStatus(TreatmentStatus.NotStarted, "Applying…");

                (TreatmentStatus status, TimeSpan duration) = await RunStep(item.Treatment, Apply);
                string text = status switch
                {
                    TreatmentStatus.Restart => "Applied, restart to finish",
                    TreatmentStatus.Error => "Failed, see details",
                    _ => "Applied",
                };
                item.SetStatus(status, text, duration);
                RefreshDetails(item);

                prgTreatmentApplication.Value += 1;
            }

            int restartCount = CountStatus(treatments, TreatmentStatus.Restart);
            int failedCount = CountStatus(treatments, TreatmentStatus.Error);

            string summary = $"Applied {Count(treatments.Count, "treatment")} in {Durations.Humanize(overallTime.Elapsed)}.";
            if (restartCount > 0)
            {
                summary += $" Restart Windows to finish {Count(restartCount, "treatment")}.";
            }
            if (failedCount > 0)
            {
                summary += $" {failedCount} failed; select a treatment to see what went wrong.";
            }
            EndRun(summary);
        }

        private static TreatmentStatus Analyze(BaseTreatment treatment)
        {
            if (treatment.ShouldPerformTreatment())
            {
                return TreatmentStatus.Info;
            }

            return treatment.RestartPending() ? TreatmentStatus.Restart : TreatmentStatus.Ok;
        }

        private static TreatmentStatus Apply(BaseTreatment treatment)
        {
            if (!treatment.PerformTreatment())
            {
                return TreatmentStatus.Error;
            }

            return treatment.RestartPending() ? TreatmentStatus.Restart : TreatmentStatus.Ok;
        }

        /// <summary>
        /// Runs part of a treatment in the background. An exception is recorded in the treatment's log and
        /// reported as an error.
        /// </summary>
        private static async Task<(TreatmentStatus Status, TimeSpan Duration)> RunStep(BaseTreatment treatment, Func<BaseTreatment, TreatmentStatus> step)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                TreatmentStatus status = await Task.Run(() => step(treatment));
                return (status, stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                treatment.Logger.Log("{0}: {1}", ex.GetType().Name, ex.Message.Trim());
                return (TreatmentStatus.Error, stopwatch.Elapsed);
            }
        }

        private static int CountStatus(List<TreatmentItem> treatments, TreatmentStatus status)
        {
            return treatments.Count(item => item.Status == status);
        }

        private List<TreatmentItem> CheckedTreatments()
        {
            return lstTreatments.Items.OfType<TreatmentItem>().Where(item => item.Checked).ToList();
        }

        /// <summary>
        /// Clears the previous run's results and, if any treatments are selected, disables the buttons until the run ends.
        /// </summary>
        /// <returns>Returns true if there is anything to run, false otherwise.</returns>
        private bool BeginRun(List<TreatmentItem> treatments)
        {
            foreach (TreatmentItem item in lstTreatments.Items.OfType<TreatmentItem>())
            {
                item.SetStatus(TreatmentStatus.NotStarted, "");
                item.Treatment.Logger.Reset();
            }

            ShowDetails();
            prgTreatmentApplication.Value = 0;

            if (treatments.Count == 0)
            {
                lblStatus.Text = "Select at least one treatment.";
                return false;
            }

            prgTreatmentApplication.Maximum = treatments.Count;
            btnAnalyze.Enabled = false;
            btnInitiateTreatment.Enabled = false;
            return true;
        }

        private void EndRun(string summary)
        {
            lblStatus.Text = summary;
            btnAnalyze.Enabled = true;
            btnInitiateTreatment.Enabled = true;
        }

        private static string Count(int count, string noun)
        {
            return count == 1 ? $"1 {noun}" : $"{count} {noun}s";
        }

        private void LstTreatments_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // Checking a category checks its treatments, and a category stays checked only while all of its treatments are.
            if (cascadingChecks)
            {
                return;
            }

            cascadingChecks = true;
            try
            {
                switch (e.Item)
                {
                    case CategoryItem category:
                        foreach (TreatmentItem treatment in category.Treatments)
                        {
                            treatment.Checked = category.Checked;
                        }
                        break;

                    case TreatmentItem treatment:
                        treatment.Category.Checked = treatment.Category.Treatments.All(t => t.Checked);
                        break;
                }
            }
            finally
            {
                cascadingChecks = false;
            }
        }

        private void LstTreatments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDetails();
        }

        /// <summary>
        /// Updates the details pane if it's showing this treatment.
        /// </summary>
        private void RefreshDetails(TreatmentItem item)
        {
            if (item.Selected)
            {
                ShowDetails();
            }
        }

        /// <summary>
        /// Shows what the selected treatment does and its log from the last run, or the selected category's description.
        /// </summary>
        private void ShowDetails()
        {
            ListViewItem? selected = lstTreatments.SelectedItems.Count > 0 ? lstTreatments.SelectedItems[0] : null;

            txtDetails.Text = selected switch
            {
                TreatmentItem item => TreatmentDetails(item.Treatment),
                CategoryItem category => category.Category.Description,
                _ => "Select a treatment to see what it does and what happened when it last ran.",
            };
        }

        private static string TreatmentDetails(BaseTreatment treatment)
        {
            string log = treatment.Logger.ToString();
            return log.Length == 0 ? treatment.Tooltip() : $"{treatment.Tooltip()}\r\n\r\n{log}";
        }

        private void GithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/t-richards/chemo") { UseShellExecute = true });
        }

        private void VersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(fullVersion);
                lblStatus.Text = $"Copied version {fullVersion} to the clipboard.";
            }
            catch (ExternalException)
            {
                // Another app has the clipboard open.
                lblStatus.Text = "Couldn't copy the version because another app is using the clipboard. Try again.";
            }
        }
    }
}
