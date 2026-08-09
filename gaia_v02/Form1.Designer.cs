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

            btnRunExperiment01 = new Button();
            txtOutput = new TextBox();
            lblStatus = new Label();

            SuspendLayout();

            // btnRunExperiment01
            btnRunExperiment01.Location = new Point(12, 12);
            btnRunExperiment01.Size = new Size(200, 36);
            btnRunExperiment01.Text = "Run Experiment 01";
            btnRunExperiment01.UseVisualStyleBackColor = true;
            btnRunExperiment01.Click += BtnRunExperiment01_Click;

            // lblStatus
            lblStatus.Location = new Point(224, 18);
            lblStatus.Size = new Size(550, 22);
            lblStatus.Text = "Ready.";

            // txtOutput
            txtOutput.Location = new Point(12, 60);
            txtOutput.Size = new Size(960, 500);
            txtOutput.Multiline = true;
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.ReadOnly = true;
            txtOutput.Font = new Font("Consolas", 9F);

            // Form1
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(990, 580);
            Controls.Add(btnRunExperiment01);
            Controls.Add(lblStatus);
            Controls.Add(txtOutput);
            Text = "Gaia SR – Experiment Runner";

            ResumeLayout(false);
        }

        private Button btnRunExperiment01;
        private TextBox txtOutput;
        private Label lblStatus;

        #endregion
    }
}
