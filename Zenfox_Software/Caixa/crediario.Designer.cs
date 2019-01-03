namespace Zenfox_Software.caixa
{
    partial class Crediario
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
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.txt_cliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_salvar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_descricao = new System.Windows.Forms.TextBox();
            this.txt_qtd_parcelas = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_data = new System.Windows.Forms.TextBox();
            this.txt_valor_venda = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_ver_parcelas = new System.Windows.Forms.Button();
            this.btn_ver_produtos = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.btn_gerar_parcelas = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_parcelas_aberto = new System.Windows.Forms.Label();
            this.lbl_valor_parcelas = new System.Windows.Forms.Label();
            this.btn_dar_baixa = new System.Windows.Forms.Button();
            this.btn_baixa_parcial = new System.Windows.Forms.Button();
            this.btn_imprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 172);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(677, 212);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.dataGridView2.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView2_RowPrePaint);
            // 
            // txt_cliente
            // 
            this.txt_cliente.Location = new System.Drawing.Point(12, 25);
            this.txt_cliente.Name = "txt_cliente";
            this.txt_cliente.ReadOnly = true;
            this.txt_cliente.Size = new System.Drawing.Size(403, 20);
            this.txt_cliente.TabIndex = 3;
            this.txt_cliente.TextChanged += new System.EventHandler(this.txt_cliente_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cliente";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = global::Zenfox_Software.Properties.Resources.search;
            this.button2.Location = new System.Drawing.Point(420, 25);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 225;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = global::Zenfox_Software.Properties.Resources.add__1_;
            this.button3.Location = new System.Drawing.Point(444, 25);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(20, 20);
            this.button3.TabIndex = 224;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_salvar
            // 
            this.btn_salvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btn_salvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_salvar.ForeColor = System.Drawing.Color.White;
            this.btn_salvar.Image = global::Zenfox_Software.Properties.Resources._002_diskette;
            this.btn_salvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_salvar.Location = new System.Drawing.Point(603, 390);
            this.btn_salvar.Margin = new System.Windows.Forms.Padding(2);
            this.btn_salvar.Name = "btn_salvar";
            this.btn_salvar.Size = new System.Drawing.Size(87, 37);
            this.btn_salvar.TabIndex = 2;
            this.btn_salvar.Text = "Salvar";
            this.btn_salvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_salvar.UseVisualStyleBackColor = false;
            this.btn_salvar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 228;
            this.label3.Text = "Quantidade Parcelas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(319, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 229;
            this.label4.Text = "Descrição do Crediário";
            // 
            // txt_descricao
            // 
            this.txt_descricao.Location = new System.Drawing.Point(319, 73);
            this.txt_descricao.MaxLength = 200;
            this.txt_descricao.Multiline = true;
            this.txt_descricao.Name = "txt_descricao";
            this.txt_descricao.Size = new System.Drawing.Size(370, 63);
            this.txt_descricao.TabIndex = 230;
            // 
            // txt_qtd_parcelas
            // 
            this.txt_qtd_parcelas.Location = new System.Drawing.Point(186, 116);
            this.txt_qtd_parcelas.MaxLength = 2;
            this.txt_qtd_parcelas.Name = "txt_qtd_parcelas";
            this.txt_qtd_parcelas.Size = new System.Drawing.Size(127, 20);
            this.txt_qtd_parcelas.TabIndex = 231;
            this.txt_qtd_parcelas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(548, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 232;
            this.label5.Text = "Data Criação";
            // 
            // txt_data
            // 
            this.txt_data.Location = new System.Drawing.Point(551, 26);
            this.txt_data.Name = "txt_data";
            this.txt_data.ReadOnly = true;
            this.txt_data.Size = new System.Drawing.Size(138, 20);
            this.txt_data.TabIndex = 233;
            // 
            // txt_valor_venda
            // 
            this.txt_valor_venda.Location = new System.Drawing.Point(14, 116);
            this.txt_valor_venda.Name = "txt_valor_venda";
            this.txt_valor_venda.ReadOnly = true;
            this.txt_valor_venda.Size = new System.Drawing.Size(166, 20);
            this.txt_valor_venda.TabIndex = 235;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 234;
            this.label6.Text = "Valor Venda";
            // 
            // btn_ver_parcelas
            // 
            this.btn_ver_parcelas.Location = new System.Drawing.Point(12, 390);
            this.btn_ver_parcelas.Name = "btn_ver_parcelas";
            this.btn_ver_parcelas.Size = new System.Drawing.Size(75, 23);
            this.btn_ver_parcelas.TabIndex = 236;
            this.btn_ver_parcelas.Text = "Ver Parcelas";
            this.btn_ver_parcelas.UseVisualStyleBackColor = true;
            this.btn_ver_parcelas.Visible = false;
            // 
            // btn_ver_produtos
            // 
            this.btn_ver_produtos.Location = new System.Drawing.Point(93, 390);
            this.btn_ver_produtos.Name = "btn_ver_produtos";
            this.btn_ver_produtos.Size = new System.Drawing.Size(138, 23);
            this.btn_ver_produtos.TabIndex = 237;
            this.btn_ver_produtos.Text = "Ver produtos vendidos";
            this.btn_ver_produtos.UseVisualStyleBackColor = true;
            this.btn_ver_produtos.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 238;
            this.label2.Text = "Status Crediário";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(14, 76);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(43, 13);
            this.lbl_status.TabIndex = 239;
            this.lbl_status.Text = "Criando";
            // 
            // btn_gerar_parcelas
            // 
            this.btn_gerar_parcelas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btn_gerar_parcelas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btn_gerar_parcelas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gerar_parcelas.ForeColor = System.Drawing.Color.White;
            this.btn_gerar_parcelas.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_gerar_parcelas.Location = new System.Drawing.Point(11, 141);
            this.btn_gerar_parcelas.Margin = new System.Windows.Forms.Padding(2);
            this.btn_gerar_parcelas.Name = "btn_gerar_parcelas";
            this.btn_gerar_parcelas.Size = new System.Drawing.Size(122, 26);
            this.btn_gerar_parcelas.TabIndex = 240;
            this.btn_gerar_parcelas.Text = "Gerar Parcelas";
            this.btn_gerar_parcelas.UseVisualStyleBackColor = false;
            this.btn_gerar_parcelas.Click += new System.EventHandler(this.btn_gerar_parcelas_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(116, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 13);
            this.label8.TabIndex = 241;
            this.label8.Text = "Parcelas em Aberto";
            // 
            // lbl_parcelas_aberto
            // 
            this.lbl_parcelas_aberto.AutoSize = true;
            this.lbl_parcelas_aberto.Location = new System.Drawing.Point(116, 76);
            this.lbl_parcelas_aberto.Name = "lbl_parcelas_aberto";
            this.lbl_parcelas_aberto.Size = new System.Drawing.Size(43, 13);
            this.lbl_parcelas_aberto.TabIndex = 242;
            this.lbl_parcelas_aberto.Text = "Criando";
            // 
            // lbl_valor_parcelas
            // 
            this.lbl_valor_parcelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_valor_parcelas.Location = new System.Drawing.Point(322, 390);
            this.lbl_valor_parcelas.Name = "lbl_valor_parcelas";
            this.lbl_valor_parcelas.Size = new System.Drawing.Size(275, 37);
            this.lbl_valor_parcelas.TabIndex = 243;
            this.lbl_valor_parcelas.Text = "R$ 0.00";
            this.lbl_valor_parcelas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_dar_baixa
            // 
            this.btn_dar_baixa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btn_dar_baixa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btn_dar_baixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_dar_baixa.ForeColor = System.Drawing.Color.White;
            this.btn_dar_baixa.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_dar_baixa.Location = new System.Drawing.Point(603, 141);
            this.btn_dar_baixa.Margin = new System.Windows.Forms.Padding(2);
            this.btn_dar_baixa.Name = "btn_dar_baixa";
            this.btn_dar_baixa.Size = new System.Drawing.Size(86, 26);
            this.btn_dar_baixa.TabIndex = 244;
            this.btn_dar_baixa.Text = "Dar Baixa";
            this.btn_dar_baixa.UseVisualStyleBackColor = false;
            this.btn_dar_baixa.Visible = false;
            this.btn_dar_baixa.Click += new System.EventHandler(this.btn_dar_baixa_Click);
            // 
            // btn_baixa_parcial
            // 
            this.btn_baixa_parcial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btn_baixa_parcial.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btn_baixa_parcial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_baixa_parcial.ForeColor = System.Drawing.Color.White;
            this.btn_baixa_parcial.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_baixa_parcial.Location = new System.Drawing.Point(511, 141);
            this.btn_baixa_parcial.Margin = new System.Windows.Forms.Padding(2);
            this.btn_baixa_parcial.Name = "btn_baixa_parcial";
            this.btn_baixa_parcial.Size = new System.Drawing.Size(86, 26);
            this.btn_baixa_parcial.TabIndex = 246;
            this.btn_baixa_parcial.Text = "Baixa Parcial";
            this.btn_baixa_parcial.UseVisualStyleBackColor = false;
            this.btn_baixa_parcial.Visible = false;
            this.btn_baixa_parcial.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_imprimir
            // 
            this.btn_imprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btn_imprimir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btn_imprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_imprimir.ForeColor = System.Drawing.Color.White;
            this.btn_imprimir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_imprimir.Location = new System.Drawing.Point(420, 141);
            this.btn_imprimir.Margin = new System.Windows.Forms.Padding(2);
            this.btn_imprimir.Name = "btn_imprimir";
            this.btn_imprimir.Size = new System.Drawing.Size(86, 26);
            this.btn_imprimir.TabIndex = 247;
            this.btn_imprimir.Text = "Imprimir";
            this.btn_imprimir.UseVisualStyleBackColor = false;
            this.btn_imprimir.Visible = false;
            this.btn_imprimir.Click += new System.EventHandler(this.btn_imprimir_Click);
            // 
            // Crediario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 432);
            this.Controls.Add(this.btn_imprimir);
            this.Controls.Add(this.btn_baixa_parcial);
            this.Controls.Add(this.btn_dar_baixa);
            this.Controls.Add(this.lbl_valor_parcelas);
            this.Controls.Add(this.lbl_parcelas_aberto);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_gerar_parcelas);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_ver_produtos);
            this.Controls.Add(this.btn_ver_parcelas);
            this.Controls.Add(this.txt_valor_venda);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_data);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_qtd_parcelas);
            this.Controls.Add(this.txt_descricao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_cliente);
            this.Controls.Add(this.btn_salvar);
            this.Controls.Add(this.dataGridView2);
            this.Name = "Crediario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "crediario";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_salvar;
        private System.Windows.Forms.TextBox txt_cliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_descricao;
        private System.Windows.Forms.TextBox txt_qtd_parcelas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_data;
        private System.Windows.Forms.TextBox txt_valor_venda;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_ver_parcelas;
        private System.Windows.Forms.Button btn_ver_produtos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Button btn_gerar_parcelas;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_parcelas_aberto;
        private System.Windows.Forms.Label lbl_valor_parcelas;
        private System.Windows.Forms.Button btn_dar_baixa;
        public System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btn_baixa_parcial;
        private System.Windows.Forms.Button btn_imprimir;
    }
}