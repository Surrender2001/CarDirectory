
namespace CarDirectory
{
    partial class FindBrandAndModelForm
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FindButton = new System.Windows.Forms.Button();
            this.ModelTextBox = new System.Windows.Forms.MaskedTextBox();
            this.BrandTextBox = new System.Windows.Forms.MaskedTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(247)))));
            this.label2.Location = new System.Drawing.Point(22, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 33);
            this.label2.TabIndex = 18;
            this.label2.Text = "Модель";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(247)))));
            this.label1.Location = new System.Drawing.Point(22, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 33);
            this.label1.TabIndex = 17;
            this.label1.Text = "Марка";
            // 
            // FindButton
            // 
            this.FindButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(120)))));
            this.FindButton.FlatAppearance.BorderSize = 0;
            this.FindButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(205)))), ((int)(((byte)(207)))));
            this.FindButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindButton.Font = new System.Drawing.Font("Roboto", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FindButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(247)))));
            this.FindButton.Location = new System.Drawing.Point(133, 172);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(162, 44);
            this.FindButton.TabIndex = 16;
            this.FindButton.Text = "Найти";
            this.FindButton.UseVisualStyleBackColor = false;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // ModelTextBox
            // 
            this.ModelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(247)))));
            this.ModelTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ModelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ModelTextBox.Location = new System.Drawing.Point(160, 109);
            this.ModelTextBox.Mask = "AAAAAAAAAAAAAAAAAAAA";
            this.ModelTextBox.Name = "ModelTextBox";
            this.ModelTextBox.Size = new System.Drawing.Size(249, 31);
            this.ModelTextBox.TabIndex = 15;
            this.ModelTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModelTextBox_KeyDown);
            // 
            // BrandTextBox
            // 
            this.BrandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BrandTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(247)))));
            this.BrandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BrandTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BrandTextBox.Location = new System.Drawing.Point(160, 47);
            this.BrandTextBox.Mask = "LLLLLLLLLLLLLLLLLLLL";
            this.BrandTextBox.Name = "BrandTextBox";
            this.BrandTextBox.RejectInputOnFirstFailure = true;
            this.BrandTextBox.Size = new System.Drawing.Size(249, 31);
            this.BrandTextBox.TabIndex = 14;
            this.BrandTextBox.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.BrandTextBox_MaskInputRejected);
            this.BrandTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BrandTextBox_KeyDown);
            // 
            // FindBrandAndModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(53)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(438, 243);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.ModelTextBox);
            this.Controls.Add(this.BrandTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindBrandAndModelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Найти";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.MaskedTextBox ModelTextBox;
        private System.Windows.Forms.MaskedTextBox BrandTextBox;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}