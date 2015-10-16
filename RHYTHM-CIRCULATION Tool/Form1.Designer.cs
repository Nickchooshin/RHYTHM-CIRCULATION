namespace RHYTHM_CIRCULATION_Tool
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_Screen = new System.Windows.Forms.Panel();
            this.groupBox_Page = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_Beat = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_Bar = new System.Windows.Forms.NumericUpDown();
            this.groupBox_Setting = new System.Windows.Forms.GroupBox();
            this.textBox_MaxBeat = new System.Windows.Forms.TextBox();
            this.textBox_BPM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox_Type = new System.Windows.Forms.GroupBox();
            this.radioButton_Slide = new System.Windows.Forms.RadioButton();
            this.radioButton_Long = new System.Windows.Forms.RadioButton();
            this.radioButton_Tap = new System.Windows.Forms.RadioButton();
            this.radioButton_Shake = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.groupBox_Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Beat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Bar)).BeginInit();
            this.groupBox_Setting.SuspendLayout();
            this.groupBox_Type.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.fileToolStripMenuItem.Text = "파일";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "새 파일";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "열기";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "저장";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.editToolStripMenuItem.Text = "편집";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.helpToolStripMenuItem.Text = "도움말";
            // 
            // panel_Screen
            // 
            this.panel_Screen.BackColor = System.Drawing.Color.Black;
            this.panel_Screen.Location = new System.Drawing.Point(12, 27);
            this.panel_Screen.Name = "panel_Screen";
            this.panel_Screen.Size = new System.Drawing.Size(405, 720);
            this.panel_Screen.TabIndex = 1;
            // 
            // groupBox_Page
            // 
            this.groupBox_Page.Controls.Add(this.label2);
            this.groupBox_Page.Controls.Add(this.numericUpDown_Beat);
            this.groupBox_Page.Controls.Add(this.label1);
            this.groupBox_Page.Controls.Add(this.numericUpDown_Bar);
            this.groupBox_Page.Location = new System.Drawing.Point(433, 92);
            this.groupBox_Page.Name = "groupBox_Page";
            this.groupBox_Page.Size = new System.Drawing.Size(270, 102);
            this.groupBox_Page.TabIndex = 2;
            this.groupBox_Page.TabStop = false;
            this.groupBox_Page.Text = "페이지";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "비트 :";
            // 
            // numericUpDown_Beat
            // 
            this.numericUpDown_Beat.Location = new System.Drawing.Point(58, 47);
            this.numericUpDown_Beat.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Beat.Name = "numericUpDown_Beat";
            this.numericUpDown_Beat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDown_Beat.Size = new System.Drawing.Size(100, 21);
            this.numericUpDown_Beat.TabIndex = 2;
            this.numericUpDown_Beat.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Beat.ValueChanged += new System.EventHandler(this.numericUpDown_Beat_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "마디 : ";
            // 
            // numericUpDown_Bar
            // 
            this.numericUpDown_Bar.Location = new System.Drawing.Point(58, 20);
            this.numericUpDown_Bar.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Bar.Name = "numericUpDown_Bar";
            this.numericUpDown_Bar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDown_Bar.Size = new System.Drawing.Size(100, 21);
            this.numericUpDown_Bar.TabIndex = 0;
            this.numericUpDown_Bar.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Bar.ValueChanged += new System.EventHandler(this.numericUpDown_Bar_ValueChanged);
            // 
            // groupBox_Setting
            // 
            this.groupBox_Setting.Controls.Add(this.textBox_MaxBeat);
            this.groupBox_Setting.Controls.Add(this.textBox_BPM);
            this.groupBox_Setting.Controls.Add(this.label4);
            this.groupBox_Setting.Controls.Add(this.label3);
            this.groupBox_Setting.Location = new System.Drawing.Point(433, 32);
            this.groupBox_Setting.Name = "groupBox_Setting";
            this.groupBox_Setting.Size = new System.Drawing.Size(270, 54);
            this.groupBox_Setting.TabIndex = 3;
            this.groupBox_Setting.TabStop = false;
            this.groupBox_Setting.Text = "곡 설정";
            // 
            // textBox_MaxBeat
            // 
            this.textBox_MaxBeat.Location = new System.Drawing.Point(202, 19);
            this.textBox_MaxBeat.Name = "textBox_MaxBeat";
            this.textBox_MaxBeat.Size = new System.Drawing.Size(50, 21);
            this.textBox_MaxBeat.TabIndex = 9;
            this.textBox_MaxBeat.TextChanged += new System.EventHandler(this.textBox_MaxBeat_TextChanged);
            // 
            // textBox_BPM
            // 
            this.textBox_BPM.Location = new System.Drawing.Point(57, 19);
            this.textBox_BPM.Name = "textBox_BPM";
            this.textBox_BPM.Size = new System.Drawing.Size(50, 21);
            this.textBox_BPM.TabIndex = 8;
            this.textBox_BPM.TextChanged += new System.EventHandler(this.textBox_BPM_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "최대 비트 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "BPM :";
            // 
            // groupBox_Type
            // 
            this.groupBox_Type.Controls.Add(this.radioButton_Shake);
            this.groupBox_Type.Controls.Add(this.radioButton_Slide);
            this.groupBox_Type.Controls.Add(this.radioButton_Long);
            this.groupBox_Type.Controls.Add(this.radioButton_Tap);
            this.groupBox_Type.Location = new System.Drawing.Point(433, 200);
            this.groupBox_Type.Name = "groupBox_Type";
            this.groupBox_Type.Size = new System.Drawing.Size(270, 123);
            this.groupBox_Type.TabIndex = 4;
            this.groupBox_Type.TabStop = false;
            this.groupBox_Type.Text = "노트 타입";
            // 
            // radioButton_Slide
            // 
            this.radioButton_Slide.AutoSize = true;
            this.radioButton_Slide.Location = new System.Drawing.Point(13, 69);
            this.radioButton_Slide.Name = "radioButton_Slide";
            this.radioButton_Slide.Size = new System.Drawing.Size(99, 16);
            this.radioButton_Slide.TabIndex = 2;
            this.radioButton_Slide.Text = "슬라이드 노트";
            this.radioButton_Slide.UseVisualStyleBackColor = true;
            this.radioButton_Slide.Click += new System.EventHandler(this.radioButton_Slide_Click);
            // 
            // radioButton_Long
            // 
            this.radioButton_Long.AutoSize = true;
            this.radioButton_Long.Location = new System.Drawing.Point(13, 47);
            this.radioButton_Long.Name = "radioButton_Long";
            this.radioButton_Long.Size = new System.Drawing.Size(63, 16);
            this.radioButton_Long.TabIndex = 1;
            this.radioButton_Long.Text = "롱 노트";
            this.radioButton_Long.UseVisualStyleBackColor = true;
            this.radioButton_Long.Click += new System.EventHandler(this.radioButton_Long_Click);
            // 
            // radioButton_Tap
            // 
            this.radioButton_Tap.AutoSize = true;
            this.radioButton_Tap.Location = new System.Drawing.Point(13, 25);
            this.radioButton_Tap.Name = "radioButton_Tap";
            this.radioButton_Tap.Size = new System.Drawing.Size(63, 16);
            this.radioButton_Tap.TabIndex = 0;
            this.radioButton_Tap.Text = "탭 노트";
            this.radioButton_Tap.UseVisualStyleBackColor = true;
            this.radioButton_Tap.Click += new System.EventHandler(this.radioButton_Tap_Click);
            // 
            // radioButton_Shake
            // 
            this.radioButton_Shake.AutoSize = true;
            this.radioButton_Shake.Location = new System.Drawing.Point(13, 91);
            this.radioButton_Shake.Name = "radioButton_Shake";
            this.radioButton_Shake.Size = new System.Drawing.Size(59, 16);
            this.radioButton_Shake.TabIndex = 3;
            this.radioButton_Shake.Text = "흔들기";
            this.radioButton_Shake.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 759);
            this.Controls.Add(this.groupBox_Type);
            this.Controls.Add(this.groupBox_Setting);
            this.Controls.Add(this.groupBox_Page);
            this.Controls.Add(this.panel_Screen);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "RHYTHM-CIRCULATION Tool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox_Page.ResumeLayout(false);
            this.groupBox_Page.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Beat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Bar)).EndInit();
            this.groupBox_Setting.ResumeLayout(false);
            this.groupBox_Setting.PerformLayout();
            this.groupBox_Type.ResumeLayout(false);
            this.groupBox_Type.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel panel_Screen;
        private System.Windows.Forms.GroupBox groupBox_Page;
        private System.Windows.Forms.NumericUpDown numericUpDown_Bar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_Beat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_Setting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_MaxBeat;
        private System.Windows.Forms.TextBox textBox_BPM;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox_Type;
        private System.Windows.Forms.RadioButton radioButton_Slide;
        private System.Windows.Forms.RadioButton radioButton_Long;
        private System.Windows.Forms.RadioButton radioButton_Tap;
        private System.Windows.Forms.RadioButton radioButton_Shake;
    }
}

