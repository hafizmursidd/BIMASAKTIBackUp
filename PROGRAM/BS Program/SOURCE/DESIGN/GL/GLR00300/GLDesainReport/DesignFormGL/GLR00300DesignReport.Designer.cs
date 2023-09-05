using FastReport;
using System.Collections;

namespace DesignFormGL
{
    partial class GLR00300DesignReport : Form
    {
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

        private void InitializeComponent()
        {
            buttonFormatA = new Button();
            SuspendLayout();
            // 
            // buttonFormatA
            // 
            buttonFormatA.Location = new Point(12, 12);
            buttonFormatA.Name = "buttonFormatA";
            buttonFormatA.Size = new Size(177, 23);
            buttonFormatA.TabIndex = 3;
            buttonFormatA.Text = "Fomart A Report";
            buttonFormatA.UseVisualStyleBackColor = true;
            buttonFormatA.Click += ButtonFormatAClick;
            // 
            // GLR00300DesignReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonFormatA);
            Name = "GLR00300DesignReport";
            Text = "DesignForm";
            Load += GLDesainReport_Load;
            ResumeLayout(false);
        }

        private Button buttonFormatA;
    }
}