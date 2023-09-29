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
            buttonFormatE_H = new Button();
            SuspendLayout();
            // 
            // buttonFormatA
            // 
            buttonFormatA.Location = new Point(12, 25);
            buttonFormatA.Name = "buttonFormatA";
            buttonFormatA.Size = new Size(177, 23);
            buttonFormatA.TabIndex = 3;
            buttonFormatA.Text = "Fomart A_D Report";
            buttonFormatA.UseVisualStyleBackColor = true;
            buttonFormatA.Click += ButtonFormatAClick;
            // 
            // buttonFormatE_H
            // 
            buttonFormatE_H.Location = new Point(12, 63);
            buttonFormatE_H.Name = "buttonFormatE_H";
            buttonFormatE_H.Size = new Size(177, 23);
            buttonFormatE_H.TabIndex = 3;
            buttonFormatE_H.Text = "Fomart E_H Report";
            buttonFormatE_H.UseVisualStyleBackColor = true;
            buttonFormatE_H.Click += ButtonFormatE_H_Click;
            // 
            // GLR00300DesignReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonFormatA);
            Controls.Add(buttonFormatE_H);
            Name = "GLR00300DesignReport";
            Text = "DesignForm";
            Load += GLDesainReport_Load;
            ResumeLayout(false);
        }

        private Button buttonFormatA;
        private Button buttonFormatE_H;
    }
}