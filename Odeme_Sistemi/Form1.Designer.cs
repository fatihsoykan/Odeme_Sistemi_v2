namespace Odeme_Sistemi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtTutar = new TextBox();
            cmbOdemeTipi = new ComboBox();
            btnOde = new Button();
            lblSonuc = new Label();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtTutar
            // 
            txtTutar.Location = new Point(99, 91);
            txtTutar.Margin = new Padding(3, 2, 3, 2);
            txtTutar.Name = "txtTutar";
            txtTutar.Size = new Size(110, 23);
            txtTutar.TabIndex = 0;
            // 
            // cmbOdemeTipi
            // 
            cmbOdemeTipi.FormattingEnabled = true;
            cmbOdemeTipi.Location = new Point(222, 91);
            cmbOdemeTipi.Margin = new Padding(3, 2, 3, 2);
            cmbOdemeTipi.Name = "cmbOdemeTipi";
            cmbOdemeTipi.Size = new Size(133, 23);
            cmbOdemeTipi.TabIndex = 1;
            cmbOdemeTipi.SelectedIndexChanged += cmbOdemeTuru_SelectedIndexChanged;
            // 
            // btnOde
            // 
            btnOde.Location = new Point(386, 91);
            btnOde.Margin = new Padding(3, 2, 3, 2);
            btnOde.Name = "btnOde";
            btnOde.Size = new Size(82, 22);
            btnOde.TabIndex = 2;
            btnOde.Text = "Öde";
            btnOde.UseVisualStyleBackColor = true;
            btnOde.Click += btnode_Click;
            // 
            // lblSonuc
            // 
            lblSonuc.AutoSize = true;
            lblSonuc.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblSonuc.Location = new Point(23, 142);
            lblSonuc.Name = "lblSonuc";
            lblSonuc.Size = new Size(68, 25);
            lblSonuc.TabIndex = 3;
            lblSonuc.Text = "Sonuç";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label1.Location = new Point(23, 91);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 4;
            label1.Text = "Tutar";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.Location = new Point(56, 17);
            label2.Name = "label2";
            label2.Size = new Size(265, 45);
            label2.TabIndex = 5;
            label2.Text = "ÖDEME SİSTEMİ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSalmon;
            ClientSize = new Size(700, 338);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblSonuc);
            Controls.Add(btnOde);
            Controls.Add(cmbOdemeTipi);
            Controls.Add(txtTutar);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTutar;
        private ComboBox cmbOdemeTipi;
        private Button btnOde;
        private Label lblSonuc;
        private Label label1;
        private Label label2;
    }
}
