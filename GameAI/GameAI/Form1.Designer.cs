namespace GameAI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxWorld = new System.Windows.Forms.PictureBox();
            this.buttonAddExplorer = new System.Windows.Forms.Button();
            this.buttonAddMiner = new System.Windows.Forms.Button();
            this.buttonAddTransporter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWorld)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxWorld
            // 
            this.pictureBoxWorld.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxWorld.Name = "pictureBoxWorld";
            this.pictureBoxWorld.Size = new System.Drawing.Size(706, 463);
            this.pictureBoxWorld.TabIndex = 0;
            this.pictureBoxWorld.TabStop = false;
            // 
            // buttonAddExplorer
            // 
            this.buttonAddExplorer.Location = new System.Drawing.Point(726, 13);
            this.buttonAddExplorer.Name = "buttonAddExplorer";
            this.buttonAddExplorer.Size = new System.Drawing.Size(91, 23);
            this.buttonAddExplorer.TabIndex = 1;
            this.buttonAddExplorer.Text = "Add Explorer";
            this.buttonAddExplorer.UseVisualStyleBackColor = true;
            this.buttonAddExplorer.Click += new System.EventHandler(this.buttonAddExplorer_Click);
            // 
            // buttonAddMiner
            // 
            this.buttonAddMiner.Location = new System.Drawing.Point(725, 42);
            this.buttonAddMiner.Name = "buttonAddMiner";
            this.buttonAddMiner.Size = new System.Drawing.Size(92, 23);
            this.buttonAddMiner.TabIndex = 2;
            this.buttonAddMiner.Text = "Add Miner";
            this.buttonAddMiner.UseVisualStyleBackColor = true;
            this.buttonAddMiner.Click += new System.EventHandler(this.buttonAddMiner_Click);
            // 
            // buttonAddTransporter
            // 
            this.buttonAddTransporter.Location = new System.Drawing.Point(725, 71);
            this.buttonAddTransporter.Name = "buttonAddTransporter";
            this.buttonAddTransporter.Size = new System.Drawing.Size(92, 23);
            this.buttonAddTransporter.TabIndex = 3;
            this.buttonAddTransporter.Text = "Add Transporter";
            this.buttonAddTransporter.UseVisualStyleBackColor = true;
            this.buttonAddTransporter.Click += new System.EventHandler(this.buttonAddTransporter_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 491);
            this.Controls.Add(this.buttonAddTransporter);
            this.Controls.Add(this.buttonAddMiner);
            this.Controls.Add(this.buttonAddExplorer);
            this.Controls.Add(this.pictureBoxWorld);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWorld)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxWorld;
        private System.Windows.Forms.Button buttonAddExplorer;
        private System.Windows.Forms.Button buttonAddMiner;
        private System.Windows.Forms.Button buttonAddTransporter;
    }
}

