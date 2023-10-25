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
            buttonFormatA_D = new Button();
            buttonFormatE_H = new Button();
            SuspendLayout();
            // 
            // buttonFormatA_D
            // 
            buttonFormatA_D.Location = new Point(12, 56);
            buttonFormatA_D.Name = "buttonFormatA_D";
            buttonFormatA_D.Size = new Size(177, 23);
            buttonFormatA_D.TabIndex = 3;
            buttonFormatA_D.Text = "Fomart A_D Report";
            buttonFormatA_D.UseVisualStyleBackColor = true;
            buttonFormatA_D.Click += ButtonFormatA_D_Click;
            // 
            // buttonFormatE_H
            // 
            buttonFormatE_H.Location = new Point(12, 99);
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
            Controls.Add(buttonFormatA_D);
            Controls.Add(buttonFormatE_H);
            Name = "GLR00300DesignReport";
            Text = "DesignForm";
            Load += GLDesainReport_Load;
            ResumeLayout(false);
        }

        private Button buttonFormatA_D;
        private Button buttonFormatE_H;
    }
}