namespace TGHMAuto
{
    partial class PopedContainer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.cbJumpFollowing = new System.Windows.Forms.CheckBox();
            this.cbFollowingKey = new System.Windows.Forms.CheckBox();
            this.ccbKeyMP = new System.Windows.Forms.ComboBox();
            this.ccbKeyHP = new System.Windows.Forms.ComboBox();
            this.ccbPercentMP = new System.Windows.Forms.ComboBox();
            this.ccbPercentHP = new System.Windows.Forms.ComboBox();
            this.cbMP = new System.Windows.Forms.CheckBox();
            this.cbHP = new System.Windows.Forms.CheckBox();
            this.lblAuto = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.cbJumpFollowing);
            this.panel1.Controls.Add(this.cbFollowingKey);
            this.panel1.Controls.Add(this.ccbKeyMP);
            this.panel1.Controls.Add(this.ccbKeyHP);
            this.panel1.Controls.Add(this.ccbPercentMP);
            this.panel1.Controls.Add(this.ccbPercentHP);
            this.panel1.Controls.Add(this.cbMP);
            this.panel1.Controls.Add(this.cbHP);
            this.panel1.Location = new System.Drawing.Point(4, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 193);
            this.panel1.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(99, 163);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // cbJumpFollowing
            // 
            this.cbJumpFollowing.AutoSize = true;
            this.cbJumpFollowing.Enabled = false;
            this.cbJumpFollowing.Location = new System.Drawing.Point(4, 103);
            this.cbJumpFollowing.Name = "cbJumpFollowing";
            this.cbJumpFollowing.Size = new System.Drawing.Size(112, 17);
            this.cbJumpFollowing.TabIndex = 7;
            this.cbJumpFollowing.Text = "Nhảy khi theo sau";
            this.cbJumpFollowing.UseVisualStyleBackColor = true;
            // 
            // cbFollowingKey
            // 
            this.cbFollowingKey.AutoSize = true;
            this.cbFollowingKey.Location = new System.Drawing.Point(4, 80);
            this.cbFollowingKey.Name = "cbFollowingKey";
            this.cbFollowingKey.Size = new System.Drawing.Size(91, 17);
            this.cbFollowingKey.TabIndex = 6;
            this.cbFollowingKey.Text = "Theo sau key";
            this.cbFollowingKey.UseVisualStyleBackColor = true;
            // 
            // ccbKeyMP
            // 
            this.ccbKeyMP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ccbKeyMP.Enabled = false;
            this.ccbKeyMP.FormattingEnabled = true;
            this.ccbKeyMP.Items.AddRange(new object[] {
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "D1",
            "D2",
            "D3",
            "D4",
            "D5",
            "D6",
            "D7",
            "D8",
            "D9"});
            this.ccbKeyMP.Location = new System.Drawing.Point(125, 25);
            this.ccbKeyMP.Name = "ccbKeyMP";
            this.ccbKeyMP.Size = new System.Drawing.Size(50, 21);
            this.ccbKeyMP.TabIndex = 5;
            // 
            // ccbKeyHP
            // 
            this.ccbKeyHP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ccbKeyHP.Enabled = false;
            this.ccbKeyHP.FormattingEnabled = true;
            this.ccbKeyHP.Items.AddRange(new object[] {
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "D1",
            "D2",
            "D3",
            "D4",
            "D5",
            "D6",
            "D7",
            "D8",
            "D9"});
            this.ccbKeyHP.Location = new System.Drawing.Point(125, 2);
            this.ccbKeyHP.Name = "ccbKeyHP";
            this.ccbKeyHP.Size = new System.Drawing.Size(50, 21);
            this.ccbKeyHP.TabIndex = 4;
            // 
            // ccbPercentMP
            // 
            this.ccbPercentMP.Enabled = false;
            this.ccbPercentMP.FormattingEnabled = true;
            this.ccbPercentMP.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90"});
            this.ccbPercentMP.Location = new System.Drawing.Point(51, 25);
            this.ccbPercentMP.Name = "ccbPercentMP";
            this.ccbPercentMP.Size = new System.Drawing.Size(50, 21);
            this.ccbPercentMP.TabIndex = 3;
            // 
            // ccbPercentHP
            // 
            this.ccbPercentHP.Enabled = false;
            this.ccbPercentHP.FormattingEnabled = true;
            this.ccbPercentHP.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90"});
            this.ccbPercentHP.Location = new System.Drawing.Point(51, 2);
            this.ccbPercentHP.Name = "ccbPercentHP";
            this.ccbPercentHP.Size = new System.Drawing.Size(50, 21);
            this.ccbPercentHP.TabIndex = 2;
            // 
            // cbMP
            // 
            this.cbMP.AutoSize = true;
            this.cbMP.Location = new System.Drawing.Point(4, 27);
            this.cbMP.Name = "cbMP";
            this.cbMP.Size = new System.Drawing.Size(42, 17);
            this.cbMP.TabIndex = 1;
            this.cbMP.Text = "MP";
            this.cbMP.UseVisualStyleBackColor = true;
            // 
            // cbHP
            // 
            this.cbHP.AutoSize = true;
            this.cbHP.Location = new System.Drawing.Point(4, 4);
            this.cbHP.Name = "cbHP";
            this.cbHP.Size = new System.Drawing.Size(41, 17);
            this.cbHP.TabIndex = 0;
            this.cbHP.Text = "HP";
            this.cbHP.UseVisualStyleBackColor = true;
            // 
            // lblAuto
            // 
            this.lblAuto.AutoSize = true;
            this.lblAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuto.Location = new System.Drawing.Point(4, 8);
            this.lblAuto.Name = "lblAuto";
            this.lblAuto.Size = new System.Drawing.Size(33, 13);
            this.lblAuto.TabIndex = 1;
            this.lblAuto.Text = "Auto";
            // 
            // PopedContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAuto);
            this.Controls.Add(this.panel1);
            this.Name = "PopedContainer";
            this.Size = new System.Drawing.Size(190, 217);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox ccbPercentMP;
        private System.Windows.Forms.ComboBox ccbPercentHP;
        private System.Windows.Forms.CheckBox cbMP;
        private System.Windows.Forms.CheckBox cbHP;
        private System.Windows.Forms.Label lblAuto;
        private System.Windows.Forms.ComboBox ccbKeyMP;
        private System.Windows.Forms.ComboBox ccbKeyHP;
        private System.Windows.Forms.CheckBox cbFollowingKey;
        private System.Windows.Forms.CheckBox cbJumpFollowing;
        private System.Windows.Forms.Button btnStart;
    }
}
