namespace AVL_Launcher
{
    partial class BrowseText
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseText));
            this.btnEnableVanguard = new System.Windows.Forms.Button();
            this.btnDisableVanguard = new System.Windows.Forms.Button();
            this.btnManualReboot = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripWatermark = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.btnLaunchLol = new System.Windows.Forms.Button();
            this.btnBrowseRiotFolder = new System.Windows.Forms.Button();
            this.lblCustomDir = new System.Windows.Forms.Label();
            this.rtbVanguardStatus = new System.Windows.Forms.RichTextBox();
            this.btnToggleTheme = new System.Windows.Forms.Button();
            this.btnOpenGitHub = new System.Windows.Forms.Button();
            this.btnToggleLanguage = new System.Windows.Forms.Button();
            this.chkMinimize = new System.Windows.Forms.CheckBox();
            this.chkAutoReboot = new System.Windows.Forms.CheckBox();
            this.chkAutoLaunch = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnableVanguard
            // 
            this.btnEnableVanguard.Location = new System.Drawing.Point(12, 81);
            this.btnEnableVanguard.Name = "btnEnableVanguard";
            this.btnEnableVanguard.Size = new System.Drawing.Size(151, 39);
            this.btnEnableVanguard.TabIndex = 4;
            this.btnEnableVanguard.Text = "Activer Vanguard";
            this.btnEnableVanguard.UseVisualStyleBackColor = true;
            // 
            // btnDisableVanguard
            // 
            this.btnDisableVanguard.Enabled = false;
            this.btnDisableVanguard.Location = new System.Drawing.Point(12, 126);
            this.btnDisableVanguard.Name = "btnDisableVanguard";
            this.btnDisableVanguard.Size = new System.Drawing.Size(151, 39);
            this.btnDisableVanguard.TabIndex = 5;
            this.btnDisableVanguard.Text = "Désactiver Vanguard";
            this.btnDisableVanguard.UseVisualStyleBackColor = true;
            // 
            // btnManualReboot
            // 
            this.btnManualReboot.Enabled = false;
            this.btnManualReboot.Location = new System.Drawing.Point(12, 171);
            this.btnManualReboot.Name = "btnManualReboot";
            this.btnManualReboot.Size = new System.Drawing.Size(151, 39);
            this.btnManualReboot.TabIndex = 6;
            this.btnManualReboot.Text = "Redémarrer le système";
            this.btnManualReboot.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.toolStripWatermark});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(364, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.StatusStrip1_ItemClicked);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(309, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "Prêt";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripWatermark
            // 
            this.toolStripWatermark.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripWatermark.Name = "toolStripWatermark";
            this.toolStripWatermark.Size = new System.Drawing.Size(40, 17);
            this.toolStripWatermark.Text = "Djexxy";
            // 
            // timerBlink
            // 
            this.timerBlink.Interval = 500;
            // 
            // btnLaunchLol
            // 
            this.btnLaunchLol.Enabled = false;
            this.btnLaunchLol.Location = new System.Drawing.Point(12, 216);
            this.btnLaunchLol.Name = "btnLaunchLol";
            this.btnLaunchLol.Size = new System.Drawing.Size(151, 39);
            this.btnLaunchLol.TabIndex = 8;
            this.btnLaunchLol.Text = "Lancer League of Legends";
            this.btnLaunchLol.UseVisualStyleBackColor = true;
            // 
            // btnBrowseRiotFolder
            // 
            this.btnBrowseRiotFolder.Location = new System.Drawing.Point(12, 394);
            this.btnBrowseRiotFolder.Name = "btnBrowseRiotFolder";
            this.btnBrowseRiotFolder.Size = new System.Drawing.Size(66, 23);
            this.btnBrowseRiotFolder.TabIndex = 9;
            this.btnBrowseRiotFolder.Text = "Parcourir";
            this.btnBrowseRiotFolder.UseVisualStyleBackColor = true;
            // 
            // lblCustomDir
            // 
            this.lblCustomDir.AutoSize = true;
            this.lblCustomDir.Location = new System.Drawing.Point(9, 378);
            this.lblCustomDir.Name = "lblCustomDir";
            this.lblCustomDir.Size = new System.Drawing.Size(340, 13);
            this.lblCustomDir.TabIndex = 10;
            this.lblCustomDir.Text = "Dossier d\'installation Riot Games personnalisé ? Cliquez sur \"Parcourir\"";
            // 
            // rtbVanguardStatus
            // 
            this.rtbVanguardStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbVanguardStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbVanguardStatus.Location = new System.Drawing.Point(12, 12);
            this.rtbVanguardStatus.Name = "rtbVanguardStatus";
            this.rtbVanguardStatus.ReadOnly = true;
            this.rtbVanguardStatus.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbVanguardStatus.Size = new System.Drawing.Size(209, 48);
            this.rtbVanguardStatus.TabIndex = 11;
            this.rtbVanguardStatus.TabStop = false;
            this.rtbVanguardStatus.Text = "";
            // 
            // btnToggleTheme
            // 
            this.btnToggleTheme.Location = new System.Drawing.Point(240, 11);
            this.btnToggleTheme.Name = "btnToggleTheme";
            this.btnToggleTheme.Size = new System.Drawing.Size(113, 23);
            this.btnToggleTheme.TabIndex = 12;
            this.btnToggleTheme.Text = "Thème Clair/Sombre";
            this.btnToggleTheme.UseVisualStyleBackColor = true;
            this.btnToggleTheme.Click += new System.EventHandler(this.BtnToggleTheme_Click);
            // 
            // btnOpenGitHub
            // 
            this.btnOpenGitHub.Location = new System.Drawing.Point(278, 393);
            this.btnOpenGitHub.Name = "btnOpenGitHub";
            this.btnOpenGitHub.Size = new System.Drawing.Size(75, 23);
            this.btnOpenGitHub.TabIndex = 13;
            this.btnOpenGitHub.Text = "GitHub";
            this.btnOpenGitHub.UseVisualStyleBackColor = true;
            this.btnOpenGitHub.Click += new System.EventHandler(this.BtnOpenGitHub_Click);
            // 
            // btnToggleLanguage
            // 
            this.btnToggleLanguage.Location = new System.Drawing.Point(240, 40);
            this.btnToggleLanguage.Name = "btnToggleLanguage";
            this.btnToggleLanguage.Size = new System.Drawing.Size(113, 23);
            this.btnToggleLanguage.TabIndex = 14;
            this.btnToggleLanguage.Text = "English";
            this.btnToggleLanguage.UseVisualStyleBackColor = true;
            this.btnToggleLanguage.Click += new System.EventHandler(this.BtnToggleLanguage_Click);
            // 
            // chkMinimize
            // 
            this.chkMinimize.AutoSize = true;
            this.chkMinimize.Checked = global::AVL_Launcher.Properties.Settings.Default.AutoMinimize;
            this.chkMinimize.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::AVL_Launcher.Properties.Settings.Default, "AutoMinimize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkMinimize.Location = new System.Drawing.Point(12, 327);
            this.chkMinimize.Name = "chkMinimize";
            this.chkMinimize.Size = new System.Drawing.Size(139, 17);
            this.chkMinimize.TabIndex = 3;
            this.chkMinimize.Text = "Minimiser pendant le jeu";
            this.chkMinimize.UseVisualStyleBackColor = true;
            this.chkMinimize.CheckedChanged += new System.EventHandler(this.ChkMinimize_CheckedChanged);
            // 
            // chkAutoReboot
            // 
            this.chkAutoReboot.AutoSize = true;
            this.chkAutoReboot.Checked = global::AVL_Launcher.Properties.Settings.Default.AutoReboot;
            this.chkAutoReboot.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::AVL_Launcher.Properties.Settings.Default, "AutoReboot", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkAutoReboot.Location = new System.Drawing.Point(12, 304);
            this.chkAutoReboot.Name = "chkAutoReboot";
            this.chkAutoReboot.Size = new System.Drawing.Size(297, 17);
            this.chkAutoReboot.TabIndex = 2;
            this.chkAutoReboot.Text = "Redémarrage automatique après activation/désactivation";
            this.chkAutoReboot.UseVisualStyleBackColor = true;
            this.chkAutoReboot.CheckedChanged += new System.EventHandler(this.ChkAutoReboot_CheckedChanged);
            // 
            // chkAutoLaunch
            // 
            this.chkAutoLaunch.AutoSize = true;
            this.chkAutoLaunch.Checked = global::AVL_Launcher.Properties.Settings.Default.AutoLaunch;
            this.chkAutoLaunch.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::AVL_Launcher.Properties.Settings.Default, "AutoLaunch", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkAutoLaunch.Location = new System.Drawing.Point(12, 281);
            this.chkAutoLaunch.Name = "chkAutoLaunch";
            this.chkAutoLaunch.Size = new System.Drawing.Size(258, 17);
            this.chkAutoLaunch.TabIndex = 1;
            this.chkAutoLaunch.Text = "Lancer automatiquement League of Legends (5s)";
            this.chkAutoLaunch.UseVisualStyleBackColor = true;
            this.chkAutoLaunch.CheckedChanged += new System.EventHandler(this.ChkAutoLaunch_CheckedChanged);
            // 
            // BrowseText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 441);
            this.Controls.Add(this.btnToggleLanguage);
            this.Controls.Add(this.btnOpenGitHub);
            this.Controls.Add(this.btnToggleTheme);
            this.Controls.Add(this.rtbVanguardStatus);
            this.Controls.Add(this.lblCustomDir);
            this.Controls.Add(this.btnBrowseRiotFolder);
            this.Controls.Add(this.btnLaunchLol);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnManualReboot);
            this.Controls.Add(this.btnDisableVanguard);
            this.Controls.Add(this.btnEnableVanguard);
            this.Controls.Add(this.chkMinimize);
            this.Controls.Add(this.chkAutoReboot);
            this.Controls.Add(this.chkAutoLaunch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(380, 480);
            this.MinimumSize = new System.Drawing.Size(380, 480);
            this.Name = "BrowseText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AVL Launcher v0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkAutoLaunch;
        private System.Windows.Forms.CheckBox chkAutoReboot;
        private System.Windows.Forms.CheckBox chkMinimize;
        private System.Windows.Forms.Button btnEnableVanguard;
        private System.Windows.Forms.Button btnDisableVanguard;
        private System.Windows.Forms.Button btnManualReboot;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Timer timerBlink;
        private System.Windows.Forms.Button btnLaunchLol;
        private System.Windows.Forms.Button btnBrowseRiotFolder;
        private System.Windows.Forms.Label lblCustomDir;
        private System.Windows.Forms.RichTextBox rtbVanguardStatus;
        private System.Windows.Forms.Button btnToggleTheme;
        private System.Windows.Forms.Button btnOpenGitHub;
        private System.Windows.Forms.Button btnToggleLanguage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripWatermark;
    }
}

