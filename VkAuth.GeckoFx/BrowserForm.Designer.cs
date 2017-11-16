using Gecko;
using System.Windows.Forms;

namespace VkAuth.GeckoFx
{
    partial class BrowserForm
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

        #region Windows BrowserForm Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browser = new Gecko.GeckoWebBrowser() { Dock = DockStyle.Fill };
            this.SuspendLayout();
            // 
            // browser
            // 
            this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browser.FrameEventsPropagateToMainWindow = false;
            this.browser.Location = new System.Drawing.Point(12, 12);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(260, 237);
            this.browser.TabIndex = 0;
            this.browser.UseHttpActivityObserver = false;
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.browser);
            this.Name = "BrowserForm";
            this.Text = "BrowserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GeckoWebBrowser browser;
    }
}

