namespace XClipboard
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
            this.components = new System.ComponentModel.Container();
            this.sharpclipboard = new WK.Libraries.SharpClipboardNS.SharpClipboard(this.components);
            this.ShowTip = new System.Windows.Forms.TextBox();
            this.ShowPic = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.testsqlite = new System.Windows.Forms.Button();
            this.testsqlite2 = new System.Windows.Forms.Button();
            this.testsqlite3 = new System.Windows.Forms.Button();
            this.testsqlite4 = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.TxtID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxtType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxtTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxtText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testsqlite5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ShowPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // sharpclipboard
            // 
            this.sharpclipboard.MonitorClipboard = true;
            this.sharpclipboard.ObservableFormats.All = true;
            this.sharpclipboard.ObservableFormats.Files = true;
            this.sharpclipboard.ObservableFormats.Images = true;
            this.sharpclipboard.ObservableFormats.Others = true;
            this.sharpclipboard.ObservableFormats.Texts = true;
            this.sharpclipboard.ObserveLastEntry = true;
            this.sharpclipboard.Tag = null;
            // 
            // ShowTip
            // 
            this.ShowTip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ShowTip.Location = new System.Drawing.Point(12, 12);
            this.ShowTip.Multiline = true;
            this.ShowTip.Name = "ShowTip";
            this.ShowTip.ReadOnly = true;
            this.ShowTip.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ShowTip.Size = new System.Drawing.Size(286, 296);
            this.ShowTip.TabIndex = 0;
            // 
            // ShowPic
            // 
            this.ShowPic.Location = new System.Drawing.Point(304, 12);
            this.ShowPic.Name = "ShowPic";
            this.ShowPic.Size = new System.Drawing.Size(249, 216);
            this.ShowPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ShowPic.TabIndex = 1;
            this.ShowPic.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(432, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "这仅是一个使用了SharpClipboard库的示例使用测试程序！";
            // 
            // testsqlite
            // 
            this.testsqlite.Location = new System.Drawing.Point(460, 234);
            this.testsqlite.Name = "testsqlite";
            this.testsqlite.Size = new System.Drawing.Size(93, 23);
            this.testsqlite.TabIndex = 3;
            this.testsqlite.Text = "初始化数据库";
            this.testsqlite.UseVisualStyleBackColor = true;
            this.testsqlite.Click += new System.EventHandler(this.testsqlite_Click);
            // 
            // testsqlite2
            // 
            this.testsqlite2.Location = new System.Drawing.Point(460, 292);
            this.testsqlite2.Name = "testsqlite2";
            this.testsqlite2.Size = new System.Drawing.Size(93, 23);
            this.testsqlite2.TabIndex = 4;
            this.testsqlite2.Text = "写库数据";
            this.testsqlite2.UseVisualStyleBackColor = true;
            this.testsqlite2.Click += new System.EventHandler(this.testsqlite2_Click);
            // 
            // testsqlite3
            // 
            this.testsqlite3.Location = new System.Drawing.Point(460, 318);
            this.testsqlite3.Name = "testsqlite3";
            this.testsqlite3.Size = new System.Drawing.Size(93, 23);
            this.testsqlite3.TabIndex = 5;
            this.testsqlite3.Text = "读库数据";
            this.testsqlite3.UseVisualStyleBackColor = true;
            this.testsqlite3.Click += new System.EventHandler(this.testsqlite3_Click);
            // 
            // testsqlite4
            // 
            this.testsqlite4.Location = new System.Drawing.Point(361, 263);
            this.testsqlite4.Name = "testsqlite4";
            this.testsqlite4.Size = new System.Drawing.Size(93, 23);
            this.testsqlite4.TabIndex = 6;
            this.testsqlite4.Text = "硬清空库数据";
            this.testsqlite4.UseVisualStyleBackColor = true;
            this.testsqlite4.Click += new System.EventHandler(this.testsqlite4_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TxtID,
            this.TxtType,
            this.TxtTime,
            this.TxtText});
            this.dataGridView.Location = new System.Drawing.Point(12, 347);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(541, 358);
            this.dataGridView.TabIndex = 7;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            // 
            // TxtID
            // 
            this.TxtID.HeaderText = "ID";
            this.TxtID.Name = "TxtID";
            this.TxtID.ReadOnly = true;
            this.TxtID.Width = 30;
            // 
            // TxtType
            // 
            this.TxtType.HeaderText = "Type";
            this.TxtType.Name = "TxtType";
            this.TxtType.ReadOnly = true;
            this.TxtType.Width = 55;
            // 
            // TxtTime
            // 
            this.TxtTime.HeaderText = "Time";
            this.TxtTime.Name = "TxtTime";
            this.TxtTime.ReadOnly = true;
            this.TxtTime.Width = 150;
            // 
            // TxtText
            // 
            this.TxtText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TxtText.HeaderText = "Text";
            this.TxtText.Name = "TxtText";
            this.TxtText.ReadOnly = true;
            // 
            // testsqlite5
            // 
            this.testsqlite5.Location = new System.Drawing.Point(460, 263);
            this.testsqlite5.Name = "testsqlite5";
            this.testsqlite5.Size = new System.Drawing.Size(93, 23);
            this.testsqlite5.TabIndex = 8;
            this.testsqlite5.Text = "软清空库数据";
            this.testsqlite5.UseVisualStyleBackColor = true;
            this.testsqlite5.Click += new System.EventHandler(this.testsqlite5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 717);
            this.Controls.Add(this.testsqlite5);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.testsqlite4);
            this.Controls.Add(this.testsqlite3);
            this.Controls.Add(this.testsqlite2);
            this.Controls.Add(this.testsqlite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShowPic);
            this.Controls.Add(this.ShowTip);
            this.Name = "Form1";
            this.Text = "测试案例";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ShowPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private WK.Libraries.SharpClipboardNS.SharpClipboard sharpclipboard;
        private System.Windows.Forms.TextBox ShowTip;
        private System.Windows.Forms.PictureBox ShowPic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button testsqlite;
        private System.Windows.Forms.Button testsqlite2;
        private System.Windows.Forms.Button testsqlite3;
        private System.Windows.Forms.Button testsqlite4;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn TxtID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TxtType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TxtTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn TxtText;
        private System.Windows.Forms.Button testsqlite5;
    }
}

