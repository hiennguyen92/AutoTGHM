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
            this.lvAccount = new System.Windows.Forms.ListView();
            this.Column1Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column2Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column3Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPathFolder
            // 
            this.txtPathFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPathFolder.Enabled = false;
            this.txtPathFolder.Location = new System.Drawing.Point(13, 35);
            this.txtPathFolder.Name = "txtPathFolder";
            this.txtPathFolder.Size = new System.Drawing.Size(298, 20);
            this.txtPathFolder.TabIndex = 1;
            // 
            // btnSelectedFolder
            // 
            this.btnSelectedFolder.Location = new System.Drawing.Point(317, 35);
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
            this.btnAdd.Location = new System.Drawing.Point(317, 297);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lvAccount
            // 
            this.lvAccount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column1Name,
            this.Column2Name,
            this.Column3Name});
            this.lvAccount.Enabled = false;
            this.lvAccount.HideSelection = false;
            this.lvAccount.Location = new System.Drawing.Point(12, 61);
            this.lvAccount.Name = "lvAccount";
            this.lvAccount.Size = new System.Drawing.Size(380, 230);
            this.lvAccount.TabIndex = 4;
            this.lvAccount.UseCompatibleStateImageBehavior = false;
            this.lvAccount.View = System.Windows.Forms.View.List;
            // 
            // Column1Name
            // 
            this.Column1Name.Width = 176;
            // 
            // Column2Name
            // 
            this.Column2Name.Width = 100;
            // 
            // Column3Name
            // 
            this.Column3Name.Width = 100;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(236, 297);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update...";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 331);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lvAccount);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSelectedFolder);
            this.Controls.Add(this.txtPathFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "TGHMAuto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPathFolder;
        private System.Windows.Forms.Button btnSelectedFolder;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListView lvAccount;
        private System.Windows.Forms.ColumnHeader Column1Name;
        private System.Windows.Forms.ColumnHeader Column2Name;
        private System.Windows.Forms.ColumnHeader Column3Name;
        private System.Windows.Forms.Button btnUpdate;
    }
}

