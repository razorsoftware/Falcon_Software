namespace Zenfox_Software.caixa
{
    partial class Retirada_caixa
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_valor = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_descricao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 147);
            this.groupBox1.TabIndex = 434;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Retirar Dinheiro do Caixa";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_valor);
            this.groupBox2.Location = new System.Drawing.Point(99, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(422, 64);
            this.groupBox2.TabIndex = 436;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "R$ Valor";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txt_valor
            // 
            this.txt_valor.BackColor = System.Drawing.SystemColors.Control;
            this.txt_valor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_valor.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_valor.Location = new System.Drawing.Point(6, 19);
            this.txt_valor.Name = "txt_valor";
            this.txt_valor.Size = new System.Drawing.Size(410, 37);
            this.txt_valor.TabIndex = 2;
            this.txt_valor.Text = "0.00";
            this.txt_valor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_valor.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.txt_valor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            this.txt_valor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_descricao);
            this.groupBox3.Location = new System.Drawing.Point(99, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(422, 48);
            this.groupBox3.TabIndex = 437;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Descrição";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // txt_descricao
            // 
            this.txt_descricao.BackColor = System.Drawing.SystemColors.Control;
            this.txt_descricao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_descricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_descricao.Location = new System.Drawing.Point(6, 15);
            this.txt_descricao.Name = "txt_descricao";
            this.txt_descricao.Size = new System.Drawing.Size(410, 24);
            this.txt_descricao.TabIndex = 1;
            this.txt_descricao.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // label1
            // 
            this.label1.Image = global::Zenfox_Software.Properties.Resources.cash_back;
            this.label1.Location = new System.Drawing.Point(6, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 84);
            this.label1.TabIndex = 432;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Retirada_caixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 171);
            this.Controls.Add(this.groupBox1);
            this.Name = "Retirada_caixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retirada_caixa";
            this.Load += new System.EventHandler(this.Retirada_caixa_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_descricao;
        private System.Windows.Forms.TextBox txt_valor;
    }
}