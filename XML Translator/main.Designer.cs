namespace XML_Translator
{
    partial class main
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.openFileBtn = new System.Windows.Forms.Button();
            this.sourceList = new System.Windows.Forms.ListBox();
            this.sourceText = new System.Windows.Forms.RichTextBox();
            this.sourceEncoding = new System.Windows.Forms.ComboBox();
            this.addToDestBtn = new System.Windows.Forms.Button();
            this.destList = new System.Windows.Forms.ListBox();
            this.destText = new System.Windows.Forms.RichTextBox();
            this.destSave = new System.Windows.Forms.Button();
            this.saveFileBtn = new System.Windows.Forms.Button();
            this.removeFromDestBtn = new System.Windows.Forms.Button();
            this.destEncoding = new System.Windows.Forms.ComboBox();
            this.sourceItemCountText = new System.Windows.Forms.Label();
            this.destItemCountText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileBtn
            // 
            this.openFileBtn.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.openFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.openFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.openFileBtn.ForeColor = System.Drawing.Color.White;
            this.openFileBtn.Location = new System.Drawing.Point(12, 12);
            this.openFileBtn.Name = "openFileBtn";
            this.openFileBtn.Size = new System.Drawing.Size(112, 35);
            this.openFileBtn.TabIndex = 0;
            this.openFileBtn.Text = "Open File";
            this.openFileBtn.UseVisualStyleBackColor = false;
            this.openFileBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // sourceList
            // 
            this.sourceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sourceList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sourceList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sourceList.ForeColor = System.Drawing.Color.White;
            this.sourceList.FormattingEnabled = true;
            this.sourceList.ItemHeight = 18;
            this.sourceList.Location = new System.Drawing.Point(12, 516);
            this.sourceList.Name = "sourceList";
            this.sourceList.Size = new System.Drawing.Size(553, 218);
            this.sourceList.TabIndex = 1;
            this.sourceList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // sourceText
            // 
            this.sourceText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sourceText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sourceText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sourceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sourceText.ForeColor = System.Drawing.Color.White;
            this.sourceText.Location = new System.Drawing.Point(12, 102);
            this.sourceText.Name = "sourceText";
            this.sourceText.ReadOnly = true;
            this.sourceText.Size = new System.Drawing.Size(553, 342);
            this.sourceText.TabIndex = 2;
            this.sourceText.Text = "";
            // 
            // sourceEncoding
            // 
            this.sourceEncoding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sourceEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceEncoding.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.sourceEncoding.FormattingEnabled = true;
            this.sourceEncoding.Location = new System.Drawing.Point(12, 68);
            this.sourceEncoding.Name = "sourceEncoding";
            this.sourceEncoding.Size = new System.Drawing.Size(170, 21);
            this.sourceEncoding.TabIndex = 3;
            this.sourceEncoding.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // addToDestBtn
            // 
            this.addToDestBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addToDestBtn.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.addToDestBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addToDestBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.addToDestBtn.ForeColor = System.Drawing.Color.White;
            this.addToDestBtn.Location = new System.Drawing.Point(595, 363);
            this.addToDestBtn.Name = "addToDestBtn";
            this.addToDestBtn.Size = new System.Drawing.Size(42, 81);
            this.addToDestBtn.TabIndex = 4;
            this.addToDestBtn.Text = ">";
            this.addToDestBtn.UseVisualStyleBackColor = false;
            this.addToDestBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // destList
            // 
            this.destList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.destList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.destList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.destList.ForeColor = System.Drawing.Color.White;
            this.destList.FormattingEnabled = true;
            this.destList.ItemHeight = 18;
            this.destList.Location = new System.Drawing.Point(662, 516);
            this.destList.Name = "destList";
            this.destList.Size = new System.Drawing.Size(553, 218);
            this.destList.TabIndex = 5;
            this.destList.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // destText
            // 
            this.destText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.destText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.destText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.destText.ForeColor = System.Drawing.Color.White;
            this.destText.Location = new System.Drawing.Point(662, 102);
            this.destText.Name = "destText";
            this.destText.Size = new System.Drawing.Size(553, 342);
            this.destText.TabIndex = 6;
            this.destText.Text = "";
            this.destText.TextChanged += new System.EventHandler(this.destText_TextChanged);
            // 
            // destSave
            // 
            this.destSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.destSave.BackColor = System.Drawing.Color.DarkGray;
            this.destSave.Enabled = false;
            this.destSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.destSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.destSave.ForeColor = System.Drawing.Color.White;
            this.destSave.Location = new System.Drawing.Point(662, 740);
            this.destSave.Name = "destSave";
            this.destSave.Size = new System.Drawing.Size(553, 35);
            this.destSave.TabIndex = 7;
            this.destSave.Text = "Save";
            this.destSave.UseVisualStyleBackColor = false;
            this.destSave.Click += new System.EventHandler(this.button3_Click);
            // 
            // saveFileBtn
            // 
            this.saveFileBtn.BackColor = System.Drawing.Color.DarkGray;
            this.saveFileBtn.Enabled = false;
            this.saveFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.saveFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.saveFileBtn.ForeColor = System.Drawing.Color.White;
            this.saveFileBtn.Location = new System.Drawing.Point(130, 12);
            this.saveFileBtn.Name = "saveFileBtn";
            this.saveFileBtn.Size = new System.Drawing.Size(115, 35);
            this.saveFileBtn.TabIndex = 8;
            this.saveFileBtn.Text = "Save File";
            this.saveFileBtn.UseVisualStyleBackColor = false;
            this.saveFileBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // removeFromDestBtn
            // 
            this.removeFromDestBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.removeFromDestBtn.BackColor = System.Drawing.Color.DarkGray;
            this.removeFromDestBtn.Enabled = false;
            this.removeFromDestBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.removeFromDestBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.removeFromDestBtn.ForeColor = System.Drawing.Color.White;
            this.removeFromDestBtn.Location = new System.Drawing.Point(595, 516);
            this.removeFromDestBtn.Name = "removeFromDestBtn";
            this.removeFromDestBtn.Size = new System.Drawing.Size(42, 78);
            this.removeFromDestBtn.TabIndex = 9;
            this.removeFromDestBtn.Text = "<";
            this.removeFromDestBtn.UseVisualStyleBackColor = false;
            this.removeFromDestBtn.Click += new System.EventHandler(this.button5_Click);
            // 
            // destEncoding
            // 
            this.destEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.destEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destEncoding.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.destEncoding.FormattingEnabled = true;
            this.destEncoding.Location = new System.Drawing.Point(662, 68);
            this.destEncoding.Name = "destEncoding";
            this.destEncoding.Size = new System.Drawing.Size(170, 21);
            this.destEncoding.TabIndex = 10;
            this.destEncoding.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // sourceItemCountText
            // 
            this.sourceItemCountText.AutoSize = true;
            this.sourceItemCountText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sourceItemCountText.ForeColor = System.Drawing.Color.White;
            this.sourceItemCountText.Location = new System.Drawing.Point(8, 476);
            this.sourceItemCountText.Name = "sourceItemCountText";
            this.sourceItemCountText.Size = new System.Drawing.Size(39, 20);
            this.sourceItemCountText.TabIndex = 11;
            this.sourceItemCountText.Text = "0 / 0";
            // 
            // destItemCountText
            // 
            this.destItemCountText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.destItemCountText.AutoSize = true;
            this.destItemCountText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.destItemCountText.ForeColor = System.Drawing.Color.White;
            this.destItemCountText.Location = new System.Drawing.Point(658, 476);
            this.destItemCountText.Name = "destItemCountText";
            this.destItemCountText.Size = new System.Drawing.Size(39, 20);
            this.destItemCountText.TabIndex = 12;
            this.destItemCountText.Text = "0 / 0";
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1227, 796);
            this.Controls.Add(this.destItemCountText);
            this.Controls.Add(this.sourceItemCountText);
            this.Controls.Add(this.destEncoding);
            this.Controls.Add(this.removeFromDestBtn);
            this.Controls.Add(this.saveFileBtn);
            this.Controls.Add(this.destSave);
            this.Controls.Add(this.destText);
            this.Controls.Add(this.destList);
            this.Controls.Add(this.addToDestBtn);
            this.Controls.Add(this.sourceEncoding);
            this.Controls.Add(this.sourceText);
            this.Controls.Add(this.sourceList);
            this.Controls.Add(this.openFileBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XML Translator";
            this.Load += new System.EventHandler(this.main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openFileBtn;
        private System.Windows.Forms.ListBox sourceList;
        private System.Windows.Forms.RichTextBox sourceText;
        private System.Windows.Forms.ComboBox sourceEncoding;
        private System.Windows.Forms.Button addToDestBtn;
        private System.Windows.Forms.ListBox destList;
        private System.Windows.Forms.RichTextBox destText;
        private System.Windows.Forms.Button destSave;
        private System.Windows.Forms.Button saveFileBtn;
        private System.Windows.Forms.Button removeFromDestBtn;
        private System.Windows.Forms.ComboBox destEncoding;
        private System.Windows.Forms.Label sourceItemCountText;
        private System.Windows.Forms.Label destItemCountText;
    }
}

