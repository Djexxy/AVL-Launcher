using System;
using System.Drawing;
using System.Diagnostics;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Threading;
using System.Linq;

namespace AVL_Launcher
{
    public partial class BrowseText : Form
    {
        private string currentVanguardStatus = "Inconnu";
        private Thread lolMonitorThread;
        private string folderDialogDescription;
        private string folderSetMessage;
        private string vanguardStatusPrefix = "État de Vanguard : ";
        private string statusActiveText = "Actif";
        private string statusInactiveText = "Inactif";
        private string statusUnknownText = "Inconnu";

        public BrowseText()
        {
            InitializeComponent();

            timerBlink.Tick += TimerBlink_Tick;
            timerBlink.Interval = 500;

            btnEnableVanguard.Click += BtnEnableVanguard_Click;
            btnDisableVanguard.Click += BtnDisableVanguard_Click;
            btnManualReboot.Click += BtnManualReboot_Click;
            btnLaunchLol.Click += BtnLaunchLol_Click;

            this.FormClosing += BrowseText_FormClosing;
        }

        private void BrowseText_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentVanguardStatus == statusActiveText)
            {
                var result = MessageBox.Show(
                    isFrench
                        ? "Vanguard est encore actif. Voulez-vous le désactiver avant de quitter ?"
                        : "Vanguard is still active. Do you want to disable it before exiting?",
                    isFrench ? "Attention" : "Warning",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Désactive Vanguard
                    BtnDisableVanguard_Click(null, null);
                    // Laisse la fermeture continuer
                }
                else if (result == DialogResult.No)
                {
                    // Laisse la fermeture continuer sans désactiver Vanguard
                }
                else
                {
                    // Annule la fermeture
                    e.Cancel = true;
                }
            }
        }

        private string riotGamesFolderPath = @"C:\Riot Games";

        private void ApplyTheme()
        {
            bool isDark = Properties.Settings.Default.IsDarkTheme;
            statusStrip1.RenderMode = ToolStripRenderMode.ManagerRenderMode;

            Color bgColor = isDark ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            Color fgColor = isDark ? Color.White : Color.Black;

            this.BackColor = bgColor;

            rtbVanguardStatus.BackColor = bgColor;
            rtbVanguardStatus.ForeColor = fgColor;

            if (isDark)
            {
                statusStrip1.BackColor = Color.FromArgb(30, 30, 30);
                statusStrip1.ForeColor = Color.White;
            }
            else
            {
                statusStrip1.BackColor = SystemColors.Control;
                statusStrip1.ForeColor = Color.Black;
            }

            foreach (Control c in this.Controls)
            {
                if (c is Label || c is CheckBox || c is Button)
                {
                    c.ForeColor = fgColor;
                    c.BackColor = bgColor;

                    // Ajout pour les boutons : changer bordure et style
                    if (c is Button btn)
                    {
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderColor = isDark
                            ? Color.FromArgb(255, 165, 79)  // orange doux en sombre
                            : Color.FromArgb(200, 200, 200); // gris clair en clair

                        if (!btn.Enabled && isDark)
                        {
                            btn.BackColor = Color.FromArgb(70, 70, 70); // Gris foncé lisible
                            btn.ForeColor = Color.Gray; // Texte plus doux
                        }
                    }
                }
            }

            // Mets à jour le texte du bouton thème
            btnToggleTheme.Text = isFrench
                ? (isDark ? "Thème Clair" : "Thème Sombre")
                : (isDark ? "Light Theme" : "Dark Theme");
            UpdateVanguardStatus();
            RefreshVanguardStatus();
        }

        private void BtnToggleTheme_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsDarkTheme = !Properties.Settings.Default.IsDarkTheme;
            Properties.Settings.Default.Save();
            ApplyTheme();
        }


        public BrowseText(string riotGamesFolderPath)
        {
            this.riotGamesFolderPath = riotGamesFolderPath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshVanguardStatus();
            RefreshUIControls();

            ApplyTheme();
            riotGamesFolderPath = Properties.Settings.Default.RiotGamesFolderPath;
            chkAutoLaunch.Checked = Properties.Settings.Default.AutoLaunch;
            chkMinimize.Checked = Properties.Settings.Default.AutoMinimize;
            chkAutoReboot.Checked = Properties.Settings.Default.AutoReboot;
            btnBrowseRiotFolder.Click += BtnBrowseRiotFolder_Click;
            isFrench = Properties.Settings.Default.IsFrench;
            ApplyLanguage();

            if (chkAutoLaunch.Checked && currentVanguardStatus == statusActiveText)
            {
                LaunchLol();
                StartMonitoringLol();
            }
        }

        private void ChkAutoLaunch_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoLaunch = chkAutoLaunch.Checked;
            Properties.Settings.Default.Save();
        }

        private void ChkMinimize_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoMinimize = chkMinimize.Checked;
            Properties.Settings.Default.Save();
        }

        private void ChkAutoReboot_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoReboot = chkAutoReboot.Checked;
            Properties.Settings.Default.Save();
        }

        private void BtnBrowseRiotFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = folderDialogDescription;
                fbd.SelectedPath = riotGamesFolderPath;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    riotGamesFolderPath = fbd.SelectedPath;
                    statusLabel.Text = string.Format(folderSetMessage, riotGamesFolderPath);
                    Properties.Settings.Default.RiotGamesFolderPath = riotGamesFolderPath;
                    Properties.Settings.Default.Save();
                }
            }

            ApplyTheme();
        }

        private void UpdateVanguardStatus(string status)
        {
            currentVanguardStatus = status;
            timerBlink.Stop();
            rtbVanguardStatus.Visible = true;

            string prefix = vanguardStatusPrefix;
            string fullText = prefix + status;

            rtbVanguardStatus.Clear();
            rtbVanguardStatus.Text = fullText;

            // Appliquer la couleur uniquement au mot "Actif", "Inactif" ou "Inconnu"
            rtbVanguardStatus.Select(prefix.Length, status.Length);

            if (status == statusActiveText)
            {
                rtbVanguardStatus.SelectionColor = Color.Green;
                btnLaunchLol.Enabled = true;
            }
            else if (status == statusInactiveText)
            {
                rtbVanguardStatus.SelectionColor = Color.Red;
                btnLaunchLol.Enabled = false;
            }
            else
            {
                rtbVanguardStatus.SelectionColor = Color.Red;
                timerBlink.Start();
                btnLaunchLol.Enabled = false;
            }

            // Remettre la sélection à la fin
            rtbVanguardStatus.SelectionLength = 0;
            rtbVanguardStatus.SelectionStart = rtbVanguardStatus.Text.Length;
        }

        private void UpdateVanguardStatus()
        {
            UpdateVanguardStatus(currentVanguardStatus);
        }

        private void TimerBlink_Tick(object sender, EventArgs e)
        {
            string prefix = vanguardStatusPrefix;
            string status = statusUnknownText;

            int start = prefix.Length;
            int length = status.Length;

            rtbVanguardStatus.Select(start, length);
            rtbVanguardStatus.SelectionColor = rtbVanguardStatus.SelectionColor == Color.Red ? Color.Transparent : Color.Red;
        }

        private string GetVanguardStartType()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "sc.exe",
                    Arguments = "qc vgk",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                using (var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    foreach (var line in output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (line.Trim().StartsWith("START_TYPE"))
                        {
                            var parts = line.Split(new[] { ':' }, 2);
                            if (parts.Length == 2)
                            {
                                var rest = parts[1].Trim();
                                var codeStr = rest.Split(' ')[0];
                                if (int.TryParse(codeStr, out int code))
                                {
                                    return code.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return "Unknown";
        }

        private void RefreshVanguardStatus()
        {
            string startType = GetVanguardStartType();
            switch (startType)
            {
                case "1":
                    UpdateVanguardStatus(statusActiveText);
                    break;
                case "4":
                    UpdateVanguardStatus(statusInactiveText);
                    break;
                default:
                    UpdateVanguardStatus(statusUnknownText);
                    break;
            }
        }

        private void RefreshUIControls()
        {
            string startType = GetVanguardStartType();
            bool isActive = (startType == "1");
            btnEnableVanguard.Enabled = !isActive;
            btnDisableVanguard.Enabled = isActive;
            btnManualReboot.Enabled = true;
            btnLaunchLol.Enabled = isActive;
        }

        private bool RunScConfig(string serviceName, string startType)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "sc.exe",
                    Arguments = $"config {serviceName} start={startType}",
                    UseShellExecute = true,
                    Verb = "runas",
                    CreateNoWindow = true,
                };

                using (var proc = Process.Start(psi))
                {
                    proc.WaitForExit();
                    return proc.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    isFrench
                        ? $"Erreur lors de la modification du service {serviceName} :\n{ex.Message}"
                        : $"Error modifying service {serviceName}:\n{ex.Message}",
                    isFrench ? "Erreur" : "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        private void BtnEnableVanguard_Click(object sender, EventArgs e)
        {
            bool vgkOk = RunScConfig("vgk", "system");
            bool vgcOk = RunScConfig("vgc", "auto");

            if (vgkOk && vgcOk)
            {
                MessageBox.Show(
                    isFrench
                        ? "Configuration Vanguard modifiée. Un redémarrage est nécessaire pour appliquer les changements."
                        : "Vanguard configuration changed. A restart is required to apply changes.",
                    isFrench ? "Info" : "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                if (chkAutoReboot.Checked)
                {
                    var res = MessageBox.Show(
                        isFrench ? "Voulez-vous redémarrer maintenant ?" : "Do you want to restart now?",
                        isFrench ? "Redémarrage" : "Restart",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );
                    if (res == DialogResult.Yes)
                        RestartComputer();
                }

                RefreshVanguardStatus();
                RefreshUIControls();
                statusLabel.Text = isFrench
                    ? "Vanguard activé (après reboot)."
                    : "Vanguard enabled (after reboot).";
            }
            else
            {
                statusLabel.Text = isFrench
                    ? "Erreur lors de l'activation."
                    : "Error during activation.";
            }

            ApplyTheme();
        }

        private void BtnDisableVanguard_Click(object sender, EventArgs e)
        {
            bool vgkOk = RunScConfig("vgk", "disabled");
            bool vgcOk = RunScConfig("vgc", "disabled");

            if (vgkOk && vgcOk)
            {
                MessageBox.Show(
                    isFrench
                        ? "Configuration Vanguard modifiée. Un redémarrage est nécessaire pour appliquer les changements."
                        : "Vanguard configuration changed. A restart is required to apply changes.",
                    isFrench ? "Info" : "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                if (chkAutoReboot.Checked)
                {
                    var res = MessageBox.Show(
                        isFrench ? "Voulez-vous redémarrer maintenant ?" : "Do you want to restart now?",
                        isFrench ? "Redémarrage" : "Restart",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );
                    if (res == DialogResult.Yes)
                        RestartComputer();
                }

                RefreshVanguardStatus();
                RefreshUIControls();
                statusLabel.Text = isFrench
                    ? "Vanguard désactivé (après reboot)."
                    : "Vanguard disabled (after reboot).";
            }
            else
            {
                statusLabel.Text = isFrench
                    ? "Erreur lors de la désactivation."
                    : "Error during deactivation.";
            }

            ApplyTheme();
        }

        private void BtnManualReboot_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show(
                isFrench ? "Voulez-vous vraiment redémarrer maintenant ?" : "Do you really want to restart now?",
                isFrench ? "Confirmation" : "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (res == DialogResult.Yes)
            {
                RestartComputer();
                ApplyTheme();
            }
        }

        private void RestartComputer()
        {
            try
            {
                Process.Start(new ProcessStartInfo("shutdown", "/r /t 3")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                statusLabel.Text = isFrench ? "Redémarrage en cours..." : "Restarting...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                isFrench ? "Erreur lors du redémarrage :\n" + ex.Message : "Error during restart:\n" + ex.Message,
                isFrench ? "Erreur" : "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            }
        }

        private void BtnLaunchLol_Click(object sender, EventArgs e)
        {
            LaunchLol();
            StartMonitoringLol();
            ApplyTheme();
        }

        private string currentStatusKey = "";

        private string GetTranslatedStatus(string key)
        {
            bool isFrench = Properties.Settings.Default.IsFrench;

            switch (key)
            {
                case "launching_lol":
                    return isFrench ? "Lancement de League of Legends.." : "Launching League of Legends...";
                case "vanguard_enabled":
                    return isFrench ? "Vanguard activé." : "Vanguard enabled.";
                case "vanguard_disabled":
                    return isFrench ? "Vanguard désactivé." : "Vanguard disabled.";
                default:
                    return "";
            }
        }

        private void LaunchLol()
        {
            string lolExe = System.IO.Path.Combine(riotGamesFolderPath, @"Riot Client\RiotClientServices.exe");
            string lolArgs = "--launch-product=league_of_legends --launch-patchline=live";

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = lolExe,
                    Arguments = lolArgs,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                Process.Start(psi);
                currentStatusKey = "launching_lol";
                statusLabel.Text = GetTranslatedStatus(currentStatusKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                isFrench ? "Erreur lors du lancement de League of Legends :\n" + ex.Message : "Error launching League of Legends:\n" + ex.Message,
                isFrench ? "Erreur" : "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            }
        }

        private void BtnOpenGitHub_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/Djexxy"; // Ouvre GitHub

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                isFrench ? $"Impossible d'ouvrir le lien : {ex.Message}" : $"Cannot open link: {ex.Message}",
                isFrench ? "Erreur" : "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            }
        }

        private bool isFrench = true;

        private void ApplyLanguage()
        {
            if (isFrench)
            {
                rtbVanguardStatus.Text = "État de Vanguard : ...";
                btnEnableVanguard.Text = "Activer Vanguard";
                btnDisableVanguard.Text = "Désactiver Vanguard";
                btnManualReboot.Text = "Redémarrer le système";
                btnLaunchLol.Text = "Lancer League of Legends";
                btnBrowseRiotFolder.Text = "Parcourir";
                btnToggleLanguage.Text = "English";
                chkAutoLaunch.Text = "Lancer automatiquement League of Legends (5s)";
                chkAutoReboot.Text = "Redémarrer automatiquement après activation/désactivation";
                chkMinimize.Text = "Minimiser pendant le jeu";
                lblCustomDir.Text = "Dossier d'installation personnalisé de Riot Games ? Cliquez sur \"Parcourir\"";
                folderDialogDescription = "Sélectionnez le dossier d'installation de Riot Games";
                folderSetMessage = "Dossier Riot Games défini sur : {0}";
                vanguardStatusPrefix = "État de Vanguard : ";
                statusActiveText = "Actif";
                statusInactiveText = "Inactif";
                statusUnknownText = "Inconnu";

                ApplyTheme();
            }
            else
            {
                rtbVanguardStatus.Text = "Vanguard status: ...";
                btnEnableVanguard.Text = "Enable Vanguard";
                btnDisableVanguard.Text = "Disable Vanguard";
                btnManualReboot.Text = "Restart the system";
                btnLaunchLol.Text = "Launch League of Legends";
                btnBrowseRiotFolder.Text = "Browse";
                btnToggleLanguage.Text = "Français";
                chkAutoLaunch.Text = "Automatically launch League of Legends (5s)";
                chkAutoReboot.Text = "Automatically reboot after Vanguard activation/deactivation";
                chkMinimize.Text = "Minimize during the game";
                lblCustomDir.Text = "Custom Riot Games installation folder? Click on \"Browse\"";
                folderDialogDescription = "Select the Riot Games installation folder";
                folderSetMessage = "Riot Games folder set to: {0}";
                vanguardStatusPrefix = "Vanguard status: ";
                statusActiveText = "Active";
                statusInactiveText = "Inactive";
                statusUnknownText = "Unknown";

                ApplyTheme();
            }

            statusLabel.Text = GetTranslatedStatus(currentStatusKey);
        }

        private void BtnToggleLanguage_Click(object sender, EventArgs e)
        {
            isFrench = !isFrench;
            Properties.Settings.Default.IsFrench = isFrench;
            Properties.Settings.Default.Save(); // Sauvegarde le changement
            ApplyLanguage();
        }

        private void StartMonitoringLol()
        {
            if (this.lolMonitorThread != null && this.lolMonitorThread.IsAlive)
                return;

            Thread lolMonitorThread = new Thread(() =>
            {
                int elapsed = 0;
                while (elapsed < 30)
                {
                    if (Process.GetProcessesByName("LeagueClient").Any())
                    {
                        if (chkMinimize.Checked)
                        {
                            this.Invoke((MethodInvoker)(() => this.WindowState = FormWindowState.Minimized));
                        }

                        Process lolProcess = Process.GetProcessesByName("LeagueClient").FirstOrDefault();
                        if (lolProcess != null)
                        {
                            lolProcess.WaitForExit();
                            this.Invoke((MethodInvoker)(() => this.WindowState = FormWindowState.Normal));
                        }
                        return;
                    }

                    Thread.Sleep(1000);
                    elapsed++;
                }
            });
            this.lolMonitorThread = lolMonitorThread;

            this.lolMonitorThread.IsBackground = true;
            this.lolMonitorThread.Start();
        }

        private void StatusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void WaterMark_Click(object sender, EventArgs e)
        {

        }
    }
}