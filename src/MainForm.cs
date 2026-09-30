using Chemo.Controls;
using Chemo.Treatment;
using Chemo.Utilities;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Chemo
{
    internal sealed partial class MainForm : Form
    {
        private readonly Font categoryFont;
        private readonly string fullVersion;
        private readonly ToolStripRenderMode menuRenderMode;
        private readonly ToolStripRenderMode statusRenderMode;
        private ThemeChoice themeChoice = ThemeChoice.System;
        private bool dark;
        private bool cascadingChecks;

        public MainForm()
        {
            InitializeComponent();

            categoryFont = new(treatmentList.Font, FontStyle.Bold);

            // The full version ends with "+" and the commit it was built from, which is too long for the menu but
            // useful in bug reports, so the menu shows the version number and clicking it copies the rest.
            fullVersion = typeof(MainForm).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            versionMenuItem.Text = $"Version {fullVersion.Split('+')[0]}";
            versionMenuItem.ToolTipText = "Copy full version with build information";
            helpMenuItem.DropDown.ShowItemToolTips = true;
            helpMenuItem.DropDown.Opened += HelpMenu_Opened;

            systemThemeMenuItem.Tag = ThemeChoice.System;
            lightThemeMenuItem.Tag = ThemeChoice.Light;
            darkThemeMenuItem.Tag = ThemeChoice.Dark;

            // Light mode puts back the renderers the strips start with.
            menuRenderMode = menuStrip.RenderMode;
            statusRenderMode = statusStrip.RenderMode;

            // These are drawn by Windows, so each gets Windows' light or dark look whenever its window is created.
            detailsTextBox.HandleCreated += NativeControl_HandleCreated;
            analyzeButton.HandleCreated += NativeControl_HandleCreated;
            applyButton.HandleCreated += NativeControl_HandleCreated;
            progressBar.ProgressBar.HandleCreated += NativeControl_HandleCreated;

            ApplyTheme(ShouldBeDark());

            InitTreatments();
            ShowDetails();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Set before the window is first shown, so it never flashes a light title bar.
            DarkTheme.SetTitleBar(Handle, dark);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Windows sends this whenever a setting changes, like the theme in Settings or high contrast, and Chemo's
            // own dark mode treatment sends it too. The theme is checked after the message is handled, so whoever sent
            // it isn't kept waiting while Chemo repaints.
            if (m.Msg == UnsafeNativeMethods.WM_SETTINGCHANGE)
            {
                BeginInvoke(new Action(UpdateTheme));
            }
        }

        /// <summary>
        /// Works out whether Chemo should be dark from the theme picked in the View menu, the Windows setting and high
        /// contrast.
        /// </summary>
        private bool ShouldBeDark()
        {
            return DarkTheme.UseDark(themeChoice, DarkTheme.WindowsIsDark(), SystemInformation.HighContrast);
        }

        /// <summary>
        /// Switches between light and dark if the View menu, the Windows setting or high contrast now calls for it.
        /// </summary>
        private void UpdateTheme()
        {
            bool useDark = ShouldBeDark();
            if (useDark != dark)
            {
                ApplyTheme(useDark);
            }
        }

        private void ThemeMenuItem_Click(object sender, EventArgs e)
        {
            themeChoice = (ThemeChoice)((ToolStripMenuItem)sender).Tag;
            foreach (ToolStripMenuItem item in themeMenuItem.DropDownItems.OfType<ToolStripMenuItem>())
            {
                item.Checked = item == sender;
            }

            UpdateTheme();
        }

        /// <summary>
        /// Switches every control between light and dark.
        /// </summary>
        private void ApplyTheme(bool useDark)
        {
            dark = useDark;
            DarkTheme.SetMenuMode(useDark);

            if (useDark)
            {
                // A new renderer each time, because after going back to the manager's renderer, a strip ignores being
                // given the renderer it had before.
                DarkToolStripRenderer darkRenderer = new();

                BackColor = DarkTheme.Background;
                ForeColor = DarkTheme.Text;
                menuStrip.Renderer = darkRenderer;
                statusStrip.Renderer = darkRenderer;
                splitContainer.Panel2.BackColor = DarkTheme.Border;
                detailsTextBox.BackColor = DarkTheme.Surface;
                detailsTextBox.ForeColor = DarkTheme.Text;
                progressBar.ProgressBar.ForeColor = DarkTheme.ProgressBar;
                progressBar.ProgressBar.BackColor = DarkTheme.ProgressTrack;
            }
            else
            {
                ResetBackColor();
                ResetForeColor();
                menuStrip.RenderMode = menuRenderMode;
                statusStrip.RenderMode = statusRenderMode;
                splitContainer.Panel2.ResetBackColor();
                detailsTextBox.ResetBackColor();
                detailsTextBox.ResetForeColor();
                progressBar.ProgressBar.ResetForeColor();
                progressBar.ProgressBar.ResetBackColor();
            }

            // Windows' dark text box keeps the light look's white border, so in dark mode the details box has no
            // border of its own and its panel shows a line of color around it instead.
            detailsTextBox.BorderStyle = useDark ? BorderStyle.None : BorderStyle.Fixed3D;
            splitContainer.Panel2.Padding = new Padding(useDark ? 1 : 0);

            treatmentList.Dark = useDark;
            SetMenuIcon(versionMenuItem, FluentIcons.CopyGlyph);
            SetMenuIcon(githubMenuItem, FluentIcons.OpenInNewWindowGlyph);

            foreach (Control control in new Control[] { detailsTextBox, analyzeButton, applyButton, progressBar.ProgressBar })
            {
                if (control.IsHandleCreated)
                {
                    SetNativeTheme(control);
                }
            }

            if (IsHandleCreated)
            {
                DarkTheme.SetTitleBar(Handle, useDark);

                // Windows doesn't repaint borders and scrollbars when their theme changes.
                UnsafeNativeMethods.RedrawWindow(
                    Handle,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    UnsafeNativeMethods.RDW_FRAME | UnsafeNativeMethods.RDW_INVALIDATE | UnsafeNativeMethods.RDW_ERASE | UnsafeNativeMethods.RDW_ALLCHILDREN);
            }
        }

        private void NativeControl_HandleCreated(object sender, EventArgs e)
        {
            SetNativeTheme((Control)sender);
        }

        private void SetNativeTheme(Control control)
        {
            if (control is ProgressBar)
            {
                // Windows' dark progress bars aren't in every version of Windows 11, so dark mode turns the progress
                // bar's theme off, which makes it a flat bar in its ForeColor and BackColor.
                _ = UnsafeNativeMethods.SetWindowTheme(control.Handle, dark ? " " : null, dark ? " " : null);
            }
            else
            {
                DarkTheme.SetWindowTheme(control.Handle, dark);
            }
        }

        private void HelpMenu_Opened(object sender, EventArgs e)
        {
            // Windows Forms keeps a menu's tooltip to itself, so it's found through reflection. Windows Forms for .NET
            // Framework no longer changes, and if this ever fails the tooltip keeps the light look.
            ToolTip? toolTip = typeof(ToolStrip).GetProperty("ToolTip", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(helpMenuItem.DropDown) as ToolTip;
            if (typeof(ToolTip).GetProperty("Handle", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(toolTip) is IntPtr toolTipHandle)
            {
                DarkTheme.SetWindowTheme(toolTipHandle, dark);
            }
        }

        /// <summary>
        /// Gives a menu item an icon drawn for the display's scale and the current theme, so it isn't stretched.
        /// </summary>
        private void SetMenuIcon(ToolStripMenuItem item, string glyph)
        {
            Image? oldImage = item.Image;
            item.Image = FluentIcons.DrawGlyph(glyph, LogicalToDeviceUnits(16), dark ? DarkTheme.Text : SystemColors.MenuText);
            item.ImageScaling = ToolStripItemImageScaling.None;
            oldImage?.Dispose();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // While its handle is created, the list reports every checked item as newly checked, before all of the
            // items are in place. Only follow check changes once the list is ready.
            treatmentList.ItemChecked += TreatmentList_ItemChecked;
        }

        public void InitTreatments()
        {
            treatmentList.Items.Clear();

            foreach (Category category in TreatmentCatalog.Categories)
            {
                CategoryItem categoryItem = new(category, categoryFont);
                treatmentList.Items.Add(categoryItem);

                foreach (SettingsTreatment treatment in category.Treatments)
                {
                    TreatmentItem treatmentItem = new(treatment, categoryItem);
                    categoryItem.Treatments.Add(treatmentItem);
                    treatmentList.Items.Add(treatmentItem);
                }
            }
        }

        private async void AnalyzeButton_Click(object sender, EventArgs e)
        {
            List<TreatmentItem> treatments = CheckedTreatments();
            if (!BeginRun(treatments))
            {
                return;
            }

            Stopwatch overallTime = Stopwatch.StartNew();

            foreach (TreatmentItem item in treatments)
            {
                statusLabel.Text = $"Analyzing {item.Text}…";
                item.SetStatus(TreatmentStatus.NotStarted, "Analyzing…");

                (TreatmentStatus status, TimeSpan duration) = await RunStep(item.Treatment, AnalyzeTreatment);
                string text = status switch
                {
                    TreatmentStatus.Info => "Ready to apply",
                    TreatmentStatus.Restart => "Restart to finish",
                    TreatmentStatus.Error => "Couldn't analyze, see details",
                    _ => "Already applied",
                };
                item.SetStatus(status, text, duration);
                RefreshDetails(item);

                progressBar.Value += 1;
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

        private async void ApplyButton_Click(object sender, EventArgs e)
        {
            List<TreatmentItem> treatments = CheckedTreatments();
            if (!BeginRun(treatments))
            {
                return;
            }

            Stopwatch overallTime = Stopwatch.StartNew();
            TreatmentItem? previous = null;

            foreach (TreatmentItem item in treatments)
            {
                // Scroll just far enough to show the treatment being applied, so the list follows the run to the
                // bottom. If the previous treatment has been scrolled out of view to look at something else, leave the
                // list where it is.
                if (previous is null || treatmentList.IsInView(previous))
                {
                    item.EnsureVisible();
                }
                previous = item;

                statusLabel.Text = $"Applying {item.Text}…";
                item.SetStatus(TreatmentStatus.NotStarted, "Applying…");

                (TreatmentStatus status, TimeSpan duration) = await RunStep(item.Treatment, ApplyTreatment);
                string text = status switch
                {
                    TreatmentStatus.Restart => "Applied, restart to finish",
                    TreatmentStatus.Error => "Failed, see details",
                    _ => "Applied",
                };
                item.SetStatus(status, text, duration);
                RefreshDetails(item);

                progressBar.Value += 1;
            }

            int signOutCount = await RestartExplorerIfNeeded(treatments);
            int restartCount = CountStatus(treatments, TreatmentStatus.Restart);
            int failedCount = CountStatus(treatments, TreatmentStatus.Error);

            string summary = $"Applied {Count(treatments.Count, "treatment")} in {Durations.Humanize(overallTime.Elapsed)}.";
            if (restartCount > 0)
            {
                summary += $" Restart Windows to finish {Count(restartCount, "treatment")}.";
            }
            if (signOutCount > 0)
            {
                summary += $" File Explorer couldn't restart, so sign out to finish {Count(signOutCount, "treatment")}.";
            }
            if (failedCount > 0)
            {
                summary += $" {failedCount} failed; select a treatment to see what went wrong.";
            }
            EndRun(summary);
        }

        /// <summary>
        /// Restarts File Explorer once if any treatment it only picks up when it starts changed something, and logs how
        /// it went in each of those treatments.
        /// </summary>
        /// <returns>Returns how many treatments still need a sign-out because File Explorer couldn't restart.</returns>
        private async Task<int> RestartExplorerIfNeeded(List<TreatmentItem> treatments)
        {
            List<TreatmentItem> waiting = treatments.Where(item => item.Treatment.NeedsExplorerRestart && item.Treatment.Changed).ToList();
            if (waiting.Count == 0)
            {
                return 0;
            }

            statusLabel.Text = "Restarting File Explorer…";
            Exception? failure = null;
            try
            {
                await Task.Run(ExplorerShell.Restart);
            }
            catch (Exception ex)
            {
                failure = ex;
            }

            foreach (TreatmentItem item in waiting)
            {
                if (failure is null)
                {
                    item.Treatment.Logger.Log("Restarted File Explorer.");
                }
                else
                {
                    item.Treatment.Logger.Log("Could not restart File Explorer: {0}", failure.Message);
                }
                RefreshDetails(item);
            }

            return failure is null ? 0 : waiting.Count;
        }

        private static TreatmentStatus AnalyzeTreatment(SettingsTreatment treatment)
        {
            if (treatment.NeedsApplying())
            {
                return TreatmentStatus.Info;
            }

            return treatment.RestartPending() ? TreatmentStatus.Restart : TreatmentStatus.Ok;
        }

        private static TreatmentStatus ApplyTreatment(SettingsTreatment treatment)
        {
            if (!treatment.Apply())
            {
                return TreatmentStatus.Error;
            }

            return treatment.RestartPending() ? TreatmentStatus.Restart : TreatmentStatus.Ok;
        }

        /// <summary>
        /// Runs part of a treatment in the background. An exception is recorded in the treatment's log and
        /// reported as an error.
        /// </summary>
        private static async Task<(TreatmentStatus Status, TimeSpan Duration)> RunStep(SettingsTreatment treatment, Func<SettingsTreatment, TreatmentStatus> step)
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
            return treatmentList.Items.OfType<TreatmentItem>().Where(item => item.Checked).ToList();
        }

        /// <summary>
        /// Clears the previous run's results and, if any treatments are selected, disables the buttons until the run ends.
        /// </summary>
        /// <returns>Returns true if there is anything to run, false otherwise.</returns>
        private bool BeginRun(List<TreatmentItem> treatments)
        {
            foreach (TreatmentItem item in treatmentList.Items.OfType<TreatmentItem>())
            {
                item.SetStatus(TreatmentStatus.NotStarted, "");
                item.Treatment.Logger.Reset();
            }

            ShowDetails();
            progressBar.Value = 0;

            if (treatments.Count == 0)
            {
                statusLabel.Text = "Select at least one treatment.";
                return false;
            }

            progressBar.Maximum = treatments.Count;
            analyzeButton.Enabled = false;
            applyButton.Enabled = false;
            return true;
        }

        private void EndRun(string summary)
        {
            statusLabel.Text = summary;
            analyzeButton.Enabled = true;
            applyButton.Enabled = true;
        }

        private static string Count(int count, string noun)
        {
            return count == 1 ? $"1 {noun}" : $"{count} {noun}s";
        }

        private void TreatmentList_ItemChecked(object sender, ItemCheckedEventArgs e)
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
                        treatment.Category.Checked = treatment.Category.Treatments.TrueForAll(t => t.Checked);
                        break;
                }
            }
            finally
            {
                cascadingChecks = false;
            }
        }

        private void TreatmentList_SelectedIndexChanged(object sender, EventArgs e)
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
            ListViewItem? selected = treatmentList.SelectedItems.Count > 0 ? treatmentList.SelectedItems[0] : null;

            detailsTextBox.Text = selected switch
            {
                TreatmentItem item => TreatmentDetails(item.Treatment),
                CategoryItem category => category.Category.Description,
                _ => "Select a treatment to see what it does and what happened when it last ran.",
            };
        }

        private static string TreatmentDetails(SettingsTreatment treatment)
        {
            string log = treatment.Logger.ToString();
            return log.Length == 0 ? treatment.Description : $"{treatment.Description}\r\n\r\n{log}";
        }

        private void GithubMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/t-richards/chemo") { UseShellExecute = true });
        }

        private void VersionMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(fullVersion);
                statusLabel.Text = $"Copied {fullVersion} to the clipboard.";
            }
            catch (ExternalException)
            {
                // Another app has the clipboard open.
                statusLabel.Text = "Couldn't copy the version because another app is using the clipboard. Try again.";
            }
        }
    }
}
