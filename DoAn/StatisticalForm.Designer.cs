namespace DoAn
{
    partial class StatisticalForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMaxScore = new System.Windows.Forms.Label();
            this.lblMinScore = new System.Windows.Forms.Label();
            this.lblMaxAge = new System.Windows.Forms.Label();
            this.lblMinAge = new System.Windows.Forms.Label();
            this.lblMaxBack = new System.Windows.Forms.Label();
            this.pnlStatisticalStaff = new System.Windows.Forms.Panel();
            this.lblMinSalary = new System.Windows.Forms.Label();
            this.lblMaxSalary = new System.Windows.Forms.Label();
            this.lblMinAgeStaff = new System.Windows.Forms.Label();
            this.lblMaxAgeStaff = new System.Windows.Forms.Label();
            this.lblMinYOE = new System.Windows.Forms.Label();
            this.lblMaxYOE = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlStatisticalStaff.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(111)))), ((int)(((byte)(153)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 108);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(309, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "THỐNG KÊ";
            // 
            // lblMaxScore
            // 
            this.lblMaxScore.AutoSize = true;
            this.lblMaxScore.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxScore.ForeColor = System.Drawing.Color.Black;
            this.lblMaxScore.Location = new System.Drawing.Point(37, 150);
            this.lblMaxScore.Name = "lblMaxScore";
            this.lblMaxScore.Size = new System.Drawing.Size(333, 30);
            this.lblMaxScore.TabIndex = 1;
            this.lblMaxScore.Text = "Khách hàng có nhiều điểm nhất:";
            // 
            // lblMinScore
            // 
            this.lblMinScore.AutoSize = true;
            this.lblMinScore.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinScore.ForeColor = System.Drawing.Color.Black;
            this.lblMinScore.Location = new System.Drawing.Point(79, 235);
            this.lblMinScore.Name = "lblMinScore";
            this.lblMinScore.Size = new System.Drawing.Size(291, 30);
            this.lblMinScore.TabIndex = 2;
            this.lblMinScore.Text = "Khách hàng có ít điểm nhất:";
            // 
            // lblMaxAge
            // 
            this.lblMaxAge.AutoSize = true;
            this.lblMaxAge.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxAge.ForeColor = System.Drawing.Color.Black;
            this.lblMaxAge.Location = new System.Drawing.Point(99, 405);
            this.lblMaxAge.Name = "lblMaxAge";
            this.lblMaxAge.Size = new System.Drawing.Size(271, 30);
            this.lblMaxAge.TabIndex = 4;
            this.lblMaxAge.Text = "Khách hàng lớn tuổi nhất:";
            // 
            // lblMinAge
            // 
            this.lblMinAge.AutoSize = true;
            this.lblMinAge.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinAge.ForeColor = System.Drawing.Color.Black;
            this.lblMinAge.Location = new System.Drawing.Point(104, 320);
            this.lblMinAge.Name = "lblMinAge";
            this.lblMinAge.Size = new System.Drawing.Size(266, 30);
            this.lblMinAge.TabIndex = 3;
            this.lblMinAge.Text = "Khách hàng trẻ tuối nhất:";
            // 
            // lblMaxBack
            // 
            this.lblMaxBack.AutoSize = true;
            this.lblMaxBack.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxBack.ForeColor = System.Drawing.Color.Black;
            this.lblMaxBack.Location = new System.Drawing.Point(38, 490);
            this.lblMaxBack.Name = "lblMaxBack";
            this.lblMaxBack.Size = new System.Drawing.Size(332, 30);
            this.lblMaxBack.TabIndex = 5;
            this.lblMaxBack.Text = "Khách hàng quay lại nhiều nhất:";
            // 
            // pnlStatisticalStaff
            // 
            this.pnlStatisticalStaff.Controls.Add(this.lblMinSalary);
            this.pnlStatisticalStaff.Controls.Add(this.lblMaxSalary);
            this.pnlStatisticalStaff.Controls.Add(this.lblMinAgeStaff);
            this.pnlStatisticalStaff.Controls.Add(this.lblMaxAgeStaff);
            this.pnlStatisticalStaff.Controls.Add(this.lblMinYOE);
            this.pnlStatisticalStaff.Controls.Add(this.lblMaxYOE);
            this.pnlStatisticalStaff.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatisticalStaff.Location = new System.Drawing.Point(0, 108);
            this.pnlStatisticalStaff.Name = "pnlStatisticalStaff";
            this.pnlStatisticalStaff.Size = new System.Drawing.Size(800, 485);
            this.pnlStatisticalStaff.TabIndex = 6;
            // 
            // lblMinSalary
            // 
            this.lblMinSalary.AutoSize = true;
            this.lblMinSalary.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinSalary.ForeColor = System.Drawing.Color.Black;
            this.lblMinSalary.Location = new System.Drawing.Point(151, 433);
            this.lblMinSalary.Name = "lblMinSalary";
            this.lblMinSalary.Size = new System.Drawing.Size(256, 30);
            this.lblMinSalary.TabIndex = 11;
            this.lblMinSalary.Text = "Nhân viên lương ít nhất:";
            // 
            // lblMaxSalary
            // 
            this.lblMaxSalary.AutoSize = true;
            this.lblMaxSalary.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxSalary.ForeColor = System.Drawing.Color.Black;
            this.lblMaxSalary.Location = new System.Drawing.Point(109, 349);
            this.lblMaxSalary.Name = "lblMaxSalary";
            this.lblMaxSalary.Size = new System.Drawing.Size(298, 30);
            this.lblMaxSalary.TabIndex = 10;
            this.lblMaxSalary.Text = "Nhân viên lương nhiều nhất:";
            // 
            // lblMinAgeStaff
            // 
            this.lblMinAgeStaff.AutoSize = true;
            this.lblMinAgeStaff.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinAgeStaff.ForeColor = System.Drawing.Color.Black;
            this.lblMinAgeStaff.Location = new System.Drawing.Point(157, 265);
            this.lblMinAgeStaff.Name = "lblMinAgeStaff";
            this.lblMinAgeStaff.Size = new System.Drawing.Size(250, 30);
            this.lblMinAgeStaff.TabIndex = 9;
            this.lblMinAgeStaff.Text = "Nhân viên trẻ tuổi nhất:";
            // 
            // lblMaxAgeStaff
            // 
            this.lblMaxAgeStaff.AutoSize = true;
            this.lblMaxAgeStaff.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxAgeStaff.ForeColor = System.Drawing.Color.Black;
            this.lblMaxAgeStaff.Location = new System.Drawing.Point(152, 181);
            this.lblMaxAgeStaff.Name = "lblMaxAgeStaff";
            this.lblMaxAgeStaff.Size = new System.Drawing.Size(255, 30);
            this.lblMaxAgeStaff.TabIndex = 8;
            this.lblMaxAgeStaff.Text = "Nhân viên lớn tuối nhất:";
            // 
            // lblMinYOE
            // 
            this.lblMinYOE.AutoSize = true;
            this.lblMinYOE.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinYOE.ForeColor = System.Drawing.Color.Black;
            this.lblMinYOE.Location = new System.Drawing.Point(56, 97);
            this.lblMinYOE.Name = "lblMinYOE";
            this.lblMinYOE.Size = new System.Drawing.Size(351, 30);
            this.lblMinYOE.TabIndex = 7;
            this.lblMinYOE.Text = "Nhân viên có ít kinh nghiệm nhất:";
            // 
            // lblMaxYOE
            // 
            this.lblMaxYOE.AutoSize = true;
            this.lblMaxYOE.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxYOE.ForeColor = System.Drawing.Color.Black;
            this.lblMaxYOE.Location = new System.Drawing.Point(14, 30);
            this.lblMaxYOE.Name = "lblMaxYOE";
            this.lblMaxYOE.Size = new System.Drawing.Size(393, 30);
            this.lblMaxYOE.TabIndex = 6;
            this.lblMaxYOE.Text = "Nhân viên có nhiều kinh nghiệm nhất:";
            // 
            // StatisticalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 593);
            this.Controls.Add(this.pnlStatisticalStaff);
            this.Controls.Add(this.lblMaxBack);
            this.Controls.Add(this.lblMaxAge);
            this.Controls.Add(this.lblMinAge);
            this.Controls.Add(this.lblMinScore);
            this.Controls.Add(this.lblMaxScore);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "StatisticalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StatisticalForm";
            this.Load += new System.EventHandler(this.StatisticalForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlStatisticalStaff.ResumeLayout(false);
            this.pnlStatisticalStaff.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMaxScore;
        private System.Windows.Forms.Label lblMinScore;
        private System.Windows.Forms.Label lblMaxAge;
        private System.Windows.Forms.Label lblMinAge;
        private System.Windows.Forms.Label lblMaxBack;
        private System.Windows.Forms.Panel pnlStatisticalStaff;
        private System.Windows.Forms.Label lblMaxSalary;
        private System.Windows.Forms.Label lblMinAgeStaff;
        private System.Windows.Forms.Label lblMaxAgeStaff;
        private System.Windows.Forms.Label lblMinYOE;
        private System.Windows.Forms.Label lblMaxYOE;
        private System.Windows.Forms.Label lblMinSalary;
    }
}