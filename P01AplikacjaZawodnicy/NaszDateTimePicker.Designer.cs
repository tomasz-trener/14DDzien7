namespace P01AplikacjaZawodnicy
{
    partial class NaszDateTimePicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.cbAktrywny = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dtpData
            // 
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpData.Location = new System.Drawing.Point(3, 3);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(200, 20);
            this.dtpData.TabIndex = 0;
            // 
            // cbAktrywny
            // 
            this.cbAktrywny.AutoSize = true;
            this.cbAktrywny.Checked = true;
            this.cbAktrywny.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAktrywny.Location = new System.Drawing.Point(210, 5);
            this.cbAktrywny.Name = "cbAktrywny";
            this.cbAktrywny.Size = new System.Drawing.Size(15, 14);
            this.cbAktrywny.TabIndex = 1;
            this.cbAktrywny.UseVisualStyleBackColor = true;
            this.cbAktrywny.CheckedChanged += new System.EventHandler(this.cbAktrywny_CheckedChanged);
            // 
            // NaszDateTimePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbAktrywny);
            this.Controls.Add(this.dtpData);
            this.Name = "NaszDateTimePicker";
            this.Size = new System.Drawing.Size(237, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.CheckBox cbAktrywny;
    }
}
