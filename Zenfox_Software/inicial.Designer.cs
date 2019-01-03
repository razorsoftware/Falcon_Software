namespace Zenfox_Software
{
    partial class inicial
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
            this.button1 = new System.Windows.Forms.Button();
            this.label_titulo = new System.Windows.Forms.Label();
            this.label_descricao = new System.Windows.Forms.Label();
            this.label_campo1 = new System.Windows.Forms.Label();
            this.txt_senha = new System.Windows.Forms.TextBox();
            this.label_campo2 = new System.Windows.Forms.Label();
            this.txt_confirmacao_senha = new System.Windows.Forms.TextBox();
            this.txt_senha2 = new System.Windows.Forms.Label();
            this.txt_cnpj = new System.Windows.Forms.MaskedTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(70, 286);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Vincular";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_titulo
            // 
            this.label_titulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_titulo.Location = new System.Drawing.Point(9, 28);
            this.label_titulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_titulo.Name = "label_titulo";
            this.label_titulo.Size = new System.Drawing.Size(308, 35);
            this.label_titulo.TabIndex = 1;
            this.label_titulo.Text = "Seja Bem Vindo !";
            this.label_titulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_descricao
            // 
            this.label_descricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_descricao.Location = new System.Drawing.Point(52, 72);
            this.label_descricao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_descricao.Name = "label_descricao";
            this.label_descricao.Size = new System.Drawing.Size(207, 56);
            this.label_descricao.TabIndex = 2;
            this.label_descricao.Text = "Agora você só precisa inserir seu usuario e senha para que possamos sincronizar s" +
    "eus dados nessa instancia.";
            this.label_descricao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_campo1
            // 
            this.label_campo1.AutoSize = true;
            this.label_campo1.Location = new System.Drawing.Point(43, 164);
            this.label_campo1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_campo1.Name = "label_campo1";
            this.label_campo1.Size = new System.Drawing.Size(34, 13);
            this.label_campo1.TabIndex = 3;
            this.label_campo1.Text = "CNPJ";
            // 
            // txt_senha
            // 
            this.txt_senha.Location = new System.Drawing.Point(45, 217);
            this.txt_senha.Margin = new System.Windows.Forms.Padding(2);
            this.txt_senha.Name = "txt_senha";
            this.txt_senha.Size = new System.Drawing.Size(229, 20);
            this.txt_senha.TabIndex = 2;
            this.txt_senha.UseSystemPasswordChar = true;
            // 
            // label_campo2
            // 
            this.label_campo2.AutoSize = true;
            this.label_campo2.Location = new System.Drawing.Point(43, 201);
            this.label_campo2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_campo2.Name = "label_campo2";
            this.label_campo2.Size = new System.Drawing.Size(38, 13);
            this.label_campo2.TabIndex = 5;
            this.label_campo2.Text = "Senha";
            // 
            // txt_confirmacao_senha
            // 
            this.txt_confirmacao_senha.Location = new System.Drawing.Point(45, 254);
            this.txt_confirmacao_senha.Margin = new System.Windows.Forms.Padding(2);
            this.txt_confirmacao_senha.Name = "txt_confirmacao_senha";
            this.txt_confirmacao_senha.Size = new System.Drawing.Size(229, 20);
            this.txt_confirmacao_senha.TabIndex = 3;
            this.txt_confirmacao_senha.UseSystemPasswordChar = true;
            this.txt_confirmacao_senha.Visible = false;
            // 
            // txt_senha2
            // 
            this.txt_senha2.AutoSize = true;
            this.txt_senha2.Location = new System.Drawing.Point(43, 237);
            this.txt_senha2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_senha2.Name = "txt_senha2";
            this.txt_senha2.Size = new System.Drawing.Size(115, 13);
            this.txt_senha2.TabIndex = 7;
            this.txt_senha2.Text = "Confirmação de Senha";
            this.txt_senha2.Visible = false;
            // 
            // txt_cnpj
            // 
            this.txt_cnpj.Location = new System.Drawing.Point(45, 180);
            this.txt_cnpj.Margin = new System.Windows.Forms.Padding(2);
            this.txt_cnpj.Mask = "00.000.000/0000-00";
            this.txt_cnpj.Name = "txt_cnpj";
            this.txt_cnpj.Size = new System.Drawing.Size(229, 20);
            this.txt_cnpj.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(70, 319);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Habilitar Versão de Teste";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // inicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 353);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txt_cnpj);
            this.Controls.Add(this.txt_confirmacao_senha);
            this.Controls.Add(this.txt_senha2);
            this.Controls.Add(this.txt_senha);
            this.Controls.Add(this.label_campo2);
            this.Controls.Add(this.label_campo1);
            this.Controls.Add(this.label_descricao);
            this.Controls.Add(this.label_titulo);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "inicial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicializando Sistema";
            this.Load += new System.EventHandler(this.inicial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_titulo;
        private System.Windows.Forms.Label label_descricao;
        private System.Windows.Forms.Label label_campo1;
        private System.Windows.Forms.TextBox txt_senha;
        private System.Windows.Forms.Label label_campo2;
        private System.Windows.Forms.TextBox txt_confirmacao_senha;
        private System.Windows.Forms.Label txt_senha2;
        private System.Windows.Forms.MaskedTextBox txt_cnpj;
        private System.Windows.Forms.Button button2;
    }
}

