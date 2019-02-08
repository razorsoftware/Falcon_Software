namespace Zenfox_Software.caixa
{
    partial class Caixa_configuracao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Caixa_configuracao));
            this.label1 = new System.Windows.Forms.Label();
            this.combo_tipo_funcionamento_impressora = new System.Windows.Forms.ComboBox();
            this.cb_exibir_balanca_pdv = new System.Windows.Forms.CheckBox();
            this.cmb_qtd_caracteres_peso = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo de funcionamento da balança";
            // 
            // combo_tipo_funcionamento_impressora
            // 
            this.combo_tipo_funcionamento_impressora.FormattingEnabled = true;
            this.combo_tipo_funcionamento_impressora.Location = new System.Drawing.Point(87, 76);
            this.combo_tipo_funcionamento_impressora.Name = "combo_tipo_funcionamento_impressora";
            this.combo_tipo_funcionamento_impressora.Size = new System.Drawing.Size(428, 21);
            this.combo_tipo_funcionamento_impressora.TabIndex = 1;
            this.combo_tipo_funcionamento_impressora.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cb_exibir_balanca_pdv
            // 
            this.cb_exibir_balanca_pdv.AutoSize = true;
            this.cb_exibir_balanca_pdv.Location = new System.Drawing.Point(87, 33);
            this.cb_exibir_balanca_pdv.Name = "cb_exibir_balanca_pdv";
            this.cb_exibir_balanca_pdv.Size = new System.Drawing.Size(162, 17);
            this.cb_exibir_balanca_pdv.TabIndex = 2;
            this.cb_exibir_balanca_pdv.Text = "Exibir botão balança no PDV";
            this.cb_exibir_balanca_pdv.UseVisualStyleBackColor = true;
            this.cb_exibir_balanca_pdv.CheckedChanged += new System.EventHandler(this.cb_exibir_balanca_pdv_CheckedChanged);
            // 
            // cmb_qtd_caracteres_peso
            // 
            this.cmb_qtd_caracteres_peso.FormattingEnabled = true;
            this.cmb_qtd_caracteres_peso.Location = new System.Drawing.Point(87, 124);
            this.cmb_qtd_caracteres_peso.Name = "cmb_qtd_caracteres_peso";
            this.cmb_qtd_caracteres_peso.Size = new System.Drawing.Size(169, 21);
            this.cmb_qtd_caracteres_peso.TabIndex = 4;
            this.cmb_qtd_caracteres_peso.SelectedIndexChanged += new System.EventHandler(this.cmb_qtd_caracteres_peso_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quantidade de Caracteres para peso";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.cmb_qtd_caracteres_peso);
            this.groupBox1.Controls.Add(this.cb_exibir_balanca_pdv);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.combo_tipo_funcionamento_impressora);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(581, 151);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurações da Balança";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // Caixa_configuracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 175);
            this.Controls.Add(this.groupBox1);
            this.Name = "Caixa_configuracao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caixa_configuracao";
            this.Load += new System.EventHandler(this.Caixa_configuracao_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_tipo_funcionamento_impressora;
        private System.Windows.Forms.CheckBox cb_exibir_balanca_pdv;
        private System.Windows.Forms.ComboBox cmb_qtd_caracteres_peso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}