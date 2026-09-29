using Chemo.Treatment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chemo
{
    public partial class frmMain : Form
    {
        private bool cascadingChecks;

        public frmMain()
        {
            InitializeComponent();

            lstTreatments.SmallImageList = StatusIcons.Create(lstTreatments.LogicalToDeviceUnits(16));
            InitTreatments();
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
            Font categoryFont = new Font(lstTreatments.Font, FontStyle.Bold);

            foreach (Category category in TreatmentCatalog.Categories)
            {
                CategoryItem categoryItem = new CategoryItem(category, categoryFont);
                lstTreatments.Items.Add(categoryItem);

                foreach (BaseTreatment treatment in category.Treatments)
                {
                    TreatmentItem treatmentItem = new TreatmentItem(treatment, categoryItem);
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
                    TreatmentStatus.Error => "Couldn't analyze, right-click for details",
                    _ => "Already applied",
                };
                item.SetStatus(status, text, duration);

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
                    TreatmentStatus.Error => "Failed, right-click for details",
                    _ => "Applied",
                };
                item.SetStatus(status, text, duration);

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
                summary += $" {failedCount} failed; right-click for details.";
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
                treatment.Logger.Log("{0}", ex.Message);
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

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new frmAbout();
            aboutForm.ShowDialog();
            aboutForm.Dispose();
        }

        private void LstTreatments_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            lstTreatments.ContextMenuStrip?.Dispose();
            lstTreatments.ContextMenuStrip = null;

            if (lstTreatments.HitTest(e.Location).Item is not TreatmentItem item)
            {
                return;
            }

            ContextMenuStrip menu = new ContextMenuStrip
            {
                Tag = item.Treatment
            };
            menu.Items.Add($"Show Details for {item.Text}", null, LstTreatments_OnContextMenuClick);

            lstTreatments.ContextMenuStrip = menu;
        }

        private void LstTreatments_OnContextMenuClick(object sender, EventArgs e)
        {
            ToolStripItem senderItem = (ToolStripItem)sender;
            BaseTreatment treatment = (BaseTreatment)senderItem.Owner.Tag;
            string message = treatment.Logger.ToString();

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            MessageBox.Show(message, treatment.Name());
        }
    }
}
