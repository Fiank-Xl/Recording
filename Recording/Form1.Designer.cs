
namespace Recording
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tb_httpUrl = new System.Windows.Forms.TextBox();
            this.bt_Start = new System.Windows.Forms.Button();
            this.bt_Stop = new System.Windows.Forms.Button();
            this.tb_Message = new System.Windows.Forms.TextBox();
            this.tb_Save = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_Select = new System.Windows.Forms.Button();
            this.lb_time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "直播链接：";
            // 
            // tb_httpUrl
            // 
            this.tb_httpUrl.Location = new System.Drawing.Point(94, 13);
            this.tb_httpUrl.Name = "tb_httpUrl";
            this.tb_httpUrl.Size = new System.Drawing.Size(645, 21);
            this.tb_httpUrl.TabIndex = 1;
            // 
            // bt_Start
            // 
            this.bt_Start.Location = new System.Drawing.Point(242, 406);
            this.bt_Start.Name = "bt_Start";
            this.bt_Start.Size = new System.Drawing.Size(118, 35);
            this.bt_Start.TabIndex = 2;
            this.bt_Start.Text = "开始录制";
            this.bt_Start.UseVisualStyleBackColor = true;
            this.bt_Start.Click += new System.EventHandler(this.bt_Start_Click);
            // 
            // bt_Stop
            // 
            this.bt_Stop.Location = new System.Drawing.Point(395, 406);
            this.bt_Stop.Name = "bt_Stop";
            this.bt_Stop.Size = new System.Drawing.Size(118, 35);
            this.bt_Stop.TabIndex = 3;
            this.bt_Stop.Text = "停止录制";
            this.bt_Stop.UseVisualStyleBackColor = true;
            this.bt_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
            // 
            // tb_Message
            // 
            this.tb_Message.Location = new System.Drawing.Point(13, 80);
            this.tb_Message.Multiline = true;
            this.tb_Message.Name = "tb_Message";
            this.tb_Message.Size = new System.Drawing.Size(496, 318);
            this.tb_Message.TabIndex = 4;
            // 
            // tb_Save
            // 
            this.tb_Save.Enabled = false;
            this.tb_Save.Location = new System.Drawing.Point(94, 43);
            this.tb_Save.Name = "tb_Save";
            this.tb_Save.Size = new System.Drawing.Size(552, 21);
            this.tb_Save.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "存放路径：";
            // 
            // bt_Select
            // 
            this.bt_Select.Location = new System.Drawing.Point(652, 41);
            this.bt_Select.Name = "bt_Select";
            this.bt_Select.Size = new System.Drawing.Size(87, 23);
            this.bt_Select.TabIndex = 7;
            this.bt_Select.Text = "选择";
            this.bt_Select.UseVisualStyleBackColor = true;
            this.bt_Select.Click += new System.EventHandler(this.bt_Select_Click);
            // 
            // lb_time
            // 
            this.lb_time.AutoSize = true;
            this.lb_time.Location = new System.Drawing.Point(12, 417);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(65, 12);
            this.lb_time.TabIndex = 8;
            this.lb_time.Text = "录制时间：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 449);
            this.Controls.Add(this.lb_time);
            this.Controls.Add(this.bt_Select);
            this.Controls.Add(this.tb_Save);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Message);
            this.Controls.Add(this.bt_Stop);
            this.Controls.Add(this.bt_Start);
            this.Controls.Add(this.tb_httpUrl);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "直播录制";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_httpUrl;
        private System.Windows.Forms.Button bt_Start;
        private System.Windows.Forms.Button bt_Stop;
        private System.Windows.Forms.TextBox tb_Message;
        private System.Windows.Forms.TextBox tb_Save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_Select;
        private System.Windows.Forms.Label lb_time;
    }
}

