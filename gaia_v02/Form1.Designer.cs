namespace gaia_v02
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            grpParams         = new GroupBox();
            btnRunExperiment01 = new Button();
            btnResetDefaults   = new Button();
            lblStatus          = new Label();
            lblSummaryTitle    = new Label();
            txtSummary         = new TextBox();
            lblCsvTitle        = new Label();
            txtCsv             = new TextBox();

            // Parameter labels (left column)
            lblStarCount      = new Label(); lblScaleHeightZ    = new Label();
            lblR0             = new Label(); lblExpectedRatio   = new Label();
            lblSigmaR         = new Label(); lblRatioTolerance  = new Label();
            lblSigmaZ         = new Label(); lblCellDeltaR      = new Label();
            lblSigmaPhi       = new Label(); lblCellDeltaZ      = new Label();
            lblVLSR           = new Label(); lblMinStars        = new Label();
            lblScaleHeightR   = new Label(); lblTimeoutMinutes  = new Label();

            // Parameter inputs
            nudStarCount      = new NumericUpDown();
            nudR0             = new NumericUpDown();
            nudSigmaR         = new NumericUpDown();
            nudSigmaZ         = new NumericUpDown();
            nudSigmaPhi       = new NumericUpDown();
            nudVLSR           = new NumericUpDown();
            nudScaleHeightR   = new NumericUpDown();
            nudScaleHeightZ   = new NumericUpDown();
            nudExpectedRatio  = new NumericUpDown();
            nudRatioTolerance = new NumericUpDown();
            nudCellDeltaR     = new NumericUpDown();
            nudCellDeltaZ     = new NumericUpDown();
            nudMinStars       = new NumericUpDown();
            nudTimeoutMinutes = new NumericUpDown();

            SuspendLayout();

            // ----------------------------------------------------------------
            // grpParams  (full width, top of form)
            // ----------------------------------------------------------------
            grpParams.Text     = "Experiment Parameters";
            grpParams.Location = new Point(12, 12);
            grpParams.Size     = new Size(1126, 228);
            grpParams.Font     = new Font("Segoe UI", 9F);

            // Left column: x=8 for label, x=205 for input, label w=192, input w=130
            // Right column: x=578 for label, x=775 for input
            // Row y inside groupbox: 26, 54, 82, 110, 138, 166, 194
            const int Lx1 = 8,   Nx1 = 205, Lw = 192, Nw = 130;
            const int Lx2 = 578, Nx2 = 775;
            int[] ry = [26, 54, 82, 110, 138, 166, 194];

            void SetLbl(Label l, string text, int x, int y)
            {
                l.Text = text; l.Location = new Point(x, y + 3);
                l.Size = new Size(Lw, 20); l.TextAlign = ContentAlignment.MiddleRight;
                grpParams.Controls.Add(l);
            }
            void SetNud(NumericUpDown n, int x, int y)
            {
                n.Location = new Point(x, y); n.Size = new Size(Nw, 24);
                grpParams.Controls.Add(n);
            }

            // Row 0 — Star Count  |  Expected σR/σZ Ratio
            SetLbl(lblStarCount,     "Star Count:",             Lx1, ry[0]);
            nudStarCount.Minimum = 100; nudStarCount.Maximum = 10_000_000;
            nudStarCount.Increment = 1000; nudStarCount.DecimalPlaces = 0;
            nudStarCount.Value = 50_000; nudStarCount.ThousandsSeparator = true;
            SetNud(nudStarCount, Nx1, ry[0]);

            SetLbl(lblExpectedRatio, "Expected σR/σZ Ratio:",   Lx2, ry[0]);
            nudExpectedRatio.Minimum = 0.1m; nudExpectedRatio.Maximum = 10m;
            nudExpectedRatio.Increment = 0.01m; nudExpectedRatio.DecimalPlaces = 3;
            nudExpectedRatio.Value = 1.930m;
            SetNud(nudExpectedRatio, Nx2, ry[0]);

            // Row 1 — R₀  |  Ratio Tolerance
            SetLbl(lblR0,            "R₀ – Solar Radius [kpc]:", Lx1, ry[1]);
            nudR0.Minimum = 0.1m; nudR0.Maximum = 50m;
            nudR0.Increment = 0.1m; nudR0.DecimalPlaces = 2;
            nudR0.Value = 8.00m;
            SetNud(nudR0, Nx1, ry[1]);

            SetLbl(lblRatioTolerance,"Ratio Tolerance (±):",     Lx2, ry[1]);
            nudRatioTolerance.Minimum = 0.01m; nudRatioTolerance.Maximum = 2m;
            nudRatioTolerance.Increment = 0.01m; nudRatioTolerance.DecimalPlaces = 3;
            nudRatioTolerance.Value = 0.150m;
            SetNud(nudRatioTolerance, Nx2, ry[1]);

            // Row 2 — σR  |  Cell ΔR
            SetLbl(lblSigmaR,        "σR – Radial Disp. [km/s]:",   Lx1, ry[2]);
            nudSigmaR.Minimum = 0.1m; nudSigmaR.Maximum = 500m;
            nudSigmaR.Increment = 0.5m; nudSigmaR.DecimalPlaces = 1;
            nudSigmaR.Value = 38.0m;
            SetNud(nudSigmaR, Nx1, ry[2]);

            SetLbl(lblCellDeltaR,    "Cell ΔR [kpc]:",              Lx2, ry[2]);
            nudCellDeltaR.Minimum = 0.01m; nudCellDeltaR.Maximum = 5m;
            nudCellDeltaR.Increment = 0.05m; nudCellDeltaR.DecimalPlaces = 3;
            nudCellDeltaR.Value = 0.500m;
            SetNud(nudCellDeltaR, Nx2, ry[2]);

            // Row 3 — σZ  |  Cell ΔZ
            SetLbl(lblSigmaZ,        "σZ – Vertical Disp. [km/s]:", Lx1, ry[3]);
            nudSigmaZ.Minimum = 0.1m; nudSigmaZ.Maximum = 500m;
            nudSigmaZ.Increment = 0.5m; nudSigmaZ.DecimalPlaces = 1;
            nudSigmaZ.Value = 20.0m;
            SetNud(nudSigmaZ, Nx1, ry[3]);

            SetLbl(lblCellDeltaZ,    "Cell ΔZ [kpc]:",              Lx2, ry[3]);
            nudCellDeltaZ.Minimum = 0.01m; nudCellDeltaZ.Maximum = 2m;
            nudCellDeltaZ.Increment = 0.01m; nudCellDeltaZ.DecimalPlaces = 3;
            nudCellDeltaZ.Value = 0.150m;
            SetNud(nudCellDeltaZ, Nx2, ry[3]);

            // Row 4 — σΦ  |  Min Stars/Cell
            SetLbl(lblSigmaPhi,      "σΦ – Azimuth. Disp. [km/s]:", Lx1, ry[4]);
            nudSigmaPhi.Minimum = 0.1m; nudSigmaPhi.Maximum = 500m;
            nudSigmaPhi.Increment = 0.5m; nudSigmaPhi.DecimalPlaces = 1;
            nudSigmaPhi.Value = 28.0m;
            SetNud(nudSigmaPhi, Nx1, ry[4]);

            SetLbl(lblMinStars,      "Min Stars / Cell:",            Lx2, ry[4]);
            nudMinStars.Minimum = 1; nudMinStars.Maximum = 1000;
            nudMinStars.Increment = 1; nudMinStars.DecimalPlaces = 0;
            nudMinStars.Value = 20;
            SetNud(nudMinStars, Nx2, ry[4]);

            // Row 5 — V_LSR  |  Timeout
            SetLbl(lblVLSR,          "V_LSR [km/s]:",               Lx1, ry[5]);
            nudVLSR.Minimum = 0m; nudVLSR.Maximum = 500m;
            nudVLSR.Increment = 1m; nudVLSR.DecimalPlaces = 1;
            nudVLSR.Value = 220.0m;
            SetNud(nudVLSR, Nx1, ry[5]);

            SetLbl(lblTimeoutMinutes,"Timeout [minutes]:",           Lx2, ry[5]);
            nudTimeoutMinutes.Minimum = 1; nudTimeoutMinutes.Maximum = 60;
            nudTimeoutMinutes.Increment = 1; nudTimeoutMinutes.DecimalPlaces = 0;
            nudTimeoutMinutes.Value = 5;
            SetNud(nudTimeoutMinutes, Nx2, ry[5]);

            // Row 6 — Scale Height R  |  Scale Height Z
            SetLbl(lblScaleHeightR,  "Scale Height R [kpc]:",       Lx1, ry[6]);
            nudScaleHeightR.Minimum = 0.01m; nudScaleHeightR.Maximum = 20m;
            nudScaleHeightR.Increment = 0.1m; nudScaleHeightR.DecimalPlaces = 2;
            nudScaleHeightR.Value = 2.50m;
            SetNud(nudScaleHeightR, Nx1, ry[6]);

            SetLbl(lblScaleHeightZ,  "Scale Height Z [kpc]:",       Lx2, ry[6]);
            nudScaleHeightZ.Minimum = 0.01m; nudScaleHeightZ.Maximum = 5m;
            nudScaleHeightZ.Increment = 0.01m; nudScaleHeightZ.DecimalPlaces = 3;
            nudScaleHeightZ.Value = 0.300m;
            SetNud(nudScaleHeightZ, Nx2, ry[6]);

            // ----------------------------------------------------------------
            // Action buttons + status label (y = 12 + 228 + 10 = 250)
            // ----------------------------------------------------------------
            btnRunExperiment01.Location = new Point(12, 252);
            btnRunExperiment01.Size     = new Size(200, 34);
            btnRunExperiment01.Text     = "▶  Run Experiment 01";
            btnRunExperiment01.UseVisualStyleBackColor = true;
            btnRunExperiment01.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRunExperiment01.Click   += BtnRunExperiment01_Click;

            btnResetDefaults.Location = new Point(222, 252);
            btnResetDefaults.Size     = new Size(160, 34);
            btnResetDefaults.Text     = "↺  Reset Defaults";
            btnResetDefaults.UseVisualStyleBackColor = true;
            btnResetDefaults.Click   += BtnResetDefaults_Click;

            lblStatus.Location  = new Point(394, 260);
            lblStatus.Size      = new Size(744, 20);
            lblStatus.Text      = "Ready. Set parameters above, then click Run.";
            lblStatus.ForeColor = SystemColors.GrayText;

            // ----------------------------------------------------------------
            // txtSummary – experiment details + insights  (y = 252+34+8 = 294)
            // ----------------------------------------------------------------
            lblSummaryTitle.Location = new Point(12, 296);
            lblSummaryTitle.Size     = new Size(500, 18);
            lblSummaryTitle.Text     = "Experiment Summary & Insights:";
            lblSummaryTitle.Font     = new Font("Segoe UI", 9F, FontStyle.Bold);

            txtSummary.Location    = new Point(12, 316);
            txtSummary.Size        = new Size(1126, 190);
            txtSummary.Multiline   = true;
            txtSummary.ScrollBars  = ScrollBars.Vertical;
            txtSummary.ReadOnly    = true;
            txtSummary.Font        = new Font("Consolas", 8.75F);
            txtSummary.BackColor   = Color.FromArgb(245, 245, 250);

            // ----------------------------------------------------------------
            // txtCsv – raw CSV output  (y = 316+190+8 = 514)
            // ----------------------------------------------------------------
            lblCsvTitle.Location = new Point(12, 516);
            lblCsvTitle.Size     = new Size(500, 18);
            lblCsvTitle.Text     = "CSV Output (also saved to file):";
            lblCsvTitle.Font     = new Font("Segoe UI", 9F, FontStyle.Bold);

            txtCsv.Location   = new Point(12, 536);
            txtCsv.Size       = new Size(1126, 362);
            txtCsv.Multiline  = true;
            txtCsv.ScrollBars = ScrollBars.Both;
            txtCsv.ReadOnly   = true;
            txtCsv.Font       = new Font("Consolas", 8.75F);
            txtCsv.WordWrap   = false;

            // ----------------------------------------------------------------
            // Form
            // ----------------------------------------------------------------
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize    = new Size(1150, 912);
            MinimumSize   = new Size(900, 700);
            Text          = "Gaia SR – Experiment Runner";
            Font          = new Font("Segoe UI", 9F);

            Controls.Add(grpParams);
            Controls.Add(btnRunExperiment01);
            Controls.Add(btnResetDefaults);
            Controls.Add(lblStatus);
            Controls.Add(lblSummaryTitle);
            Controls.Add(txtSummary);
            Controls.Add(lblCsvTitle);
            Controls.Add(txtCsv);

            ResumeLayout(false);
        }

        // Parameter group
        private GroupBox grpParams;

        // Parameter labels
        private Label lblStarCount, lblR0, lblSigmaR, lblSigmaZ, lblSigmaPhi;
        private Label lblVLSR, lblScaleHeightR, lblScaleHeightZ;
        private Label lblExpectedRatio, lblRatioTolerance;
        private Label lblCellDeltaR, lblCellDeltaZ, lblMinStars, lblTimeoutMinutes;

        // Parameter inputs
        private NumericUpDown nudStarCount, nudR0, nudSigmaR, nudSigmaZ, nudSigmaPhi;
        private NumericUpDown nudVLSR, nudScaleHeightR, nudScaleHeightZ;
        private NumericUpDown nudExpectedRatio, nudRatioTolerance;
        private NumericUpDown nudCellDeltaR, nudCellDeltaZ, nudMinStars, nudTimeoutMinutes;

        // Action controls
        private Button btnRunExperiment01;
        private Button btnResetDefaults;
        private Label  lblStatus;

        // Output controls
        private Label   lblSummaryTitle;
        private TextBox txtSummary;
        private Label   lblCsvTitle;
        private TextBox txtCsv;

        #endregion
    }
}

