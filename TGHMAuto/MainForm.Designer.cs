namespace TGHMAuto
{
    partial class MainForm
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
            this.txtPathFolder = new System.Windows.Forms.TextBox();
            this.btnSelectedFolder = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewAccount = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPathFolder
            // 
            this.txtPathFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPathFolder.Enabled = false;
            this.txtPathFolder.Location = new System.Drawing.Point(3, 5);
            this.txtPathFolder.Name = "txtPathFolder";
            this.txtPathFolder.Size = new System.Drawing.Size(178, 20);
            this.txtPathFolder.TabIndex = 1;
            // 
            // btnSelectedFolder
            // 
            this.btnSelectedFolder.Location = new System.Drawing.Point(187, 3);
            this.btnSelectedFolder.Name = "btnSelectedFolder";
            this.btnSelectedFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectedFolder.TabIndex = 2;
            this.btnSelectedFolder.Text = "Folder";
            this.btnSelectedFolder.UseVisualStyleBackColor = true;
            this.btnSelectedFolder.Click += new System.EventHandler(this.btnSelectedFolder_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(207, 426);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(126, 426);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update...";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtPathFolder);
            this.panel1.Controls.Add(this.btnSelectedFolder);
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 32);
            this.panel1.TabIndex = 6;
            // 
            // dataGridViewAccount
            // 
            this.dataGridViewAccount.AllowUserToAddRows = false;
            this.dataGridViewAccount.AllowUserToDeleteRows = false;
            this.dataGridViewAccount.AllowUserToResizeColumns = false;
            this.dataGridViewAccount.AllowUserToResizeRows = false;
            this.dataGridViewAccount.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccount.ColumnHeadersVisible = false;
            this.dataGridViewAccount.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dataGridViewAccount.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewAccount.MultiSelect = false;
            this.dataGridViewAccount.Name = "dataGridViewAccount";
            this.dataGridViewAccount.ReadOnly = true;
            this.dataGridViewAccount.RowHeadersVisible = false;
            this.dataGridViewAccount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewAccount.Size = new System.Drawing.Size(270, 370);
            this.dataGridViewAccount.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 461);
            this.Controls.Add(this.dataGridViewAccount);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TGHMAuto";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtPathFolder;
        private System.Windows.Forms.Button btnSelectedFolder;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridViewAccount;
    }
}

