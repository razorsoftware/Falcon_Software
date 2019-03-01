namespace Zenfox_Software.caixa
{
    partial class Caixa
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Caixa));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txt_descricao_produto = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txt_valor_produto = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_quantidade = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_codigo_barras = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btn_delete_venda = new System.Windows.Forms.Button();
            this.dg_venda = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_cabecalho_caixa = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_nome_operador = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_logoff = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_nome_cliente = new System.Windows.Forms.Label();
            this.txtTotalFinal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.verifica_focus = new System.Windows.Forms.Timer(this.components);
            this.lbl_dia_semana = new System.Windows.Forms.Label();
            this.lbl_data = new System.Windows.Forms.Label();
            this.lbl_hora = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_minimizar = new System.Windows.Forms.Button();
            this.btn_fechar = new System.Windows.Forms.Button();
            this.btn_balanca = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.lbl_cpf = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_venda)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Location = new System.Drawing.Point(9, 174);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 314);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.txt_descricao_produto);
            this.groupBox6.Location = new System.Drawing.Point(4, 205);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(500, 51);
            this.groupBox6.TabIndex = 447;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Descrição Produto";
            this.groupBox6.Enter += new System.EventHandler(this.groupBox6_Enter);
            // 
            // txt_descricao_produto
            // 
            this.txt_descricao_produto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_descricao_produto.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.txt_descricao_produto.Location = new System.Drawing.Point(4, 17);
            this.txt_descricao_produto.Margin = new System.Windows.Forms.Padding(2);
            this.txt_descricao_produto.Name = "txt_descricao_produto";
            this.txt_descricao_produto.ReadOnly = true;
            this.txt_descricao_produto.Size = new System.Drawing.Size(491, 28);
            this.txt_descricao_produto.TabIndex = 0;
            this.txt_descricao_produto.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.txt_valor_produto);
            this.groupBox5.Location = new System.Drawing.Point(256, 261);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(249, 51);
            this.groupBox5.TabIndex = 446;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Valor R$";
            // 
            // txt_valor_produto
            // 
            this.txt_valor_produto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_valor_produto.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.txt_valor_produto.Location = new System.Drawing.Point(4, 17);
            this.txt_valor_produto.Margin = new System.Windows.Forms.Padding(2);
            this.txt_valor_produto.Name = "txt_valor_produto";
            this.txt_valor_produto.ReadOnly = true;
            this.txt_valor_produto.Size = new System.Drawing.Size(240, 28);
            this.txt_valor_produto.TabIndex = 1;
            this.txt_valor_produto.Text = "0.00";
            this.txt_valor_produto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_valor_produto.TextChanged += new System.EventHandler(this.txt_valor_produto_TextChanged);
            this.txt_valor_produto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_valor_produto_KeyPress);
            this.txt_valor_produto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_valor_produto_KeyUp);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.txt_quantidade);
            this.groupBox4.Location = new System.Drawing.Point(2, 261);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(249, 51);
            this.groupBox4.TabIndex = 445;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Quantidade";
            // 
            // txt_quantidade
            // 
            this.txt_quantidade.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_quantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.txt_quantidade.Location = new System.Drawing.Point(4, 17);
            this.txt_quantidade.Margin = new System.Windows.Forms.Padding(2);
            this.txt_quantidade.Name = "txt_quantidade";
            this.txt_quantidade.Size = new System.Drawing.Size(244, 28);
            this.txt_quantidade.TabIndex = 0;
            this.txt_quantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.txt_quantidade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_quantidade_KeyUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.btn_search);
            this.groupBox3.Controls.Add(this.txt_codigo_barras);
            this.groupBox3.Location = new System.Drawing.Point(2, 149);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(502, 51);
            this.groupBox3.TabIndex = 444;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Código de Barras";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // btn_search
            // 
            this.btn_search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_search.FlatAppearance.BorderSize = 0;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.ForeColor = System.Drawing.Color.Black;
            this.btn_search.Image = global::Zenfox_Software.Properties.Resources.search__1_;
            this.btn_search.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_search.Location = new System.Drawing.Point(379, 10);
            this.btn_search.Margin = new System.Windows.Forms.Padding(2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(119, 35);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "F1 - Pesquisar";
            this.btn_search.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_codigo_barras
            // 
            this.txt_codigo_barras.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_codigo_barras.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.txt_codigo_barras.Location = new System.Drawing.Point(4, 17);
            this.txt_codigo_barras.Margin = new System.Windows.Forms.Padding(2);
            this.txt_codigo_barras.Name = "txt_codigo_barras";
            this.txt_codigo_barras.Size = new System.Drawing.Size(371, 28);
            this.txt_codigo_barras.TabIndex = 0;
            this.txt_codigo_barras.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txt_codigo_barras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codigo_barras_KeyPress);
            this.txt_codigo_barras.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_codigo_barras_KeyUp);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.button8);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Controls.Add(this.btn_delete_venda);
            this.panel3.Controls.Add(this.dg_venda);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Location = new System.Drawing.Point(520, 128);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(528, 420);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button8.BackgroundImage = global::Zenfox_Software.Properties.Resources._001_atm1;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(254, 364);
            this.button8.Margin = new System.Windows.Forms.Padding(2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(127, 43);
            this.button8.TabIndex = 447;
            this.button8.Text = "Acrescentar Caixa";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button7.BackgroundImage = global::Zenfox_Software.Properties.Resources._002_atm_11;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(138, 364);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(112, 43);
            this.button7.TabIndex = 446;
            this.button7.Text = "Retirar Caixa";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btn_delete_venda
            // 
            this.btn_delete_venda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete_venda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete_venda.ForeColor = System.Drawing.Color.Black;
            this.btn_delete_venda.Image = global::Zenfox_Software.Properties.Resources.remove_cart;
            this.btn_delete_venda.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_delete_venda.Location = new System.Drawing.Point(425, 364);
            this.btn_delete_venda.Margin = new System.Windows.Forms.Padding(2);
            this.btn_delete_venda.Name = "btn_delete_venda";
            this.btn_delete_venda.Size = new System.Drawing.Size(99, 43);
            this.btn_delete_venda.TabIndex = 2;
            this.btn_delete_venda.Text = "Remover";
            this.btn_delete_venda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_delete_venda.UseVisualStyleBackColor = true;
            this.btn_delete_venda.Click += new System.EventHandler(this.btn_delete_venda_Click);
            // 
            // dg_venda
            // 
            this.dg_venda.AllowUserToDeleteRows = false;
            this.dg_venda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_venda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg_venda.BackgroundColor = System.Drawing.Color.White;
            this.dg_venda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_venda.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dg_venda.Location = new System.Drawing.Point(2, 32);
            this.dg_venda.Margin = new System.Windows.Forms.Padding(2);
            this.dg_venda.Name = "dg_venda";
            this.dg_venda.ReadOnly = true;
            this.dg_venda.RowHeadersVisible = false;
            this.dg_venda.RowTemplate.Height = 24;
            this.dg_venda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_venda.Size = new System.Drawing.Size(522, 328);
            this.dg_venda.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackgroundImage = global::Zenfox_Software.Properties.Resources.eraser;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 364);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 43);
            this.button1.TabIndex = 445;
            this.button1.Text = "F3 - Limpar Venda";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_cabecalho_caixa
            // 
            this.lbl_cabecalho_caixa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_cabecalho_caixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.5F);
            this.lbl_cabecalho_caixa.Location = new System.Drawing.Point(9, 10);
            this.lbl_cabecalho_caixa.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_cabecalho_caixa.Name = "lbl_cabecalho_caixa";
            this.lbl_cabecalho_caixa.Size = new System.Drawing.Size(1039, 50);
            this.lbl_cabecalho_caixa.TabIndex = 0;
            this.lbl_cabecalho_caixa.Text = "Caixa Disponível";
            this.lbl_cabecalho_caixa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_cabecalho_caixa.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbl_nome_operador);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btn_logoff);
            this.groupBox1.Location = new System.Drawing.Point(520, 63);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(528, 61);
            this.groupBox1.TabIndex = 442;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operador(a)";
            // 
            // lbl_nome_operador
            // 
            this.lbl_nome_operador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_nome_operador.Font = new System.Drawing.Font("Segoe UI Light", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nome_operador.Location = new System.Drawing.Point(46, 17);
            this.lbl_nome_operador.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_nome_operador.Name = "lbl_nome_operador";
            this.lbl_nome_operador.Size = new System.Drawing.Size(437, 37);
            this.lbl_nome_operador.TabIndex = 4;
            this.lbl_nome_operador.Text = "Nome Operador";
            this.lbl_nome_operador.Click += new System.EventHandler(this.label2_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = global::Zenfox_Software.Properties.Resources.users;
            this.button2.Location = new System.Drawing.Point(4, 21);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 35);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btn_logoff
            // 
            this.btn_logoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_logoff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_logoff.ForeColor = System.Drawing.Color.White;
            this.btn_logoff.Image = global::Zenfox_Software.Properties.Resources.exit__3_;
            this.btn_logoff.Location = new System.Drawing.Point(486, 17);
            this.btn_logoff.Margin = new System.Windows.Forms.Padding(2);
            this.btn_logoff.Name = "btn_logoff";
            this.btn_logoff.Size = new System.Drawing.Size(38, 35);
            this.btn_logoff.TabIndex = 2;
            this.btn_logoff.UseVisualStyleBackColor = true;
            this.btn_logoff.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_nome_cliente);
            this.groupBox2.Location = new System.Drawing.Point(9, 63);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(507, 61);
            this.groupBox2.TabIndex = 443;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cliente";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // lbl_nome_cliente
            // 
            this.lbl_nome_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lbl_nome_cliente.Location = new System.Drawing.Point(4, 15);
            this.lbl_nome_cliente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_nome_cliente.Name = "lbl_nome_cliente";
            this.lbl_nome_cliente.Size = new System.Drawing.Size(498, 44);
            this.lbl_nome_cliente.TabIndex = 0;
            this.lbl_nome_cliente.Text = "Nenhum Cliente Selecionado";
            this.lbl_nome_cliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotalFinal
            // 
            this.txtTotalFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalFinal.BackColor = System.Drawing.Color.Transparent;
            this.txtTotalFinal.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.txtTotalFinal.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTotalFinal.Location = new System.Drawing.Point(218, 9);
            this.txtTotalFinal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtTotalFinal.Name = "txtTotalFinal";
            this.txtTotalFinal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalFinal.Size = new System.Drawing.Size(302, 59);
            this.txtTotalFinal.TabIndex = 429;
            this.txtTotalFinal.Text = "R$ 0.00";
            this.txtTotalFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTotalFinal.Click += new System.EventHandler(this.txtTotalFinal_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(2, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 44);
            this.label1.TabIndex = 430;
            this.label1.Text = "F2 - Finalizar Venda";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtTotalFinal);
            this.panel2.Location = new System.Drawing.Point(520, 570);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(528, 76);
            this.panel2.TabIndex = 444;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // verifica_focus
            // 
            this.verifica_focus.Enabled = true;
            this.verifica_focus.Interval = 1000;
            this.verifica_focus.Tick += new System.EventHandler(this.verifica_focus_Tick);
            // 
            // lbl_dia_semana
            // 
            this.lbl_dia_semana.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_dia_semana.AutoSize = true;
            this.lbl_dia_semana.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_dia_semana.Font = new System.Drawing.Font("Segoe UI Light", 17.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_dia_semana.ForeColor = System.Drawing.Color.Black;
            this.lbl_dia_semana.Location = new System.Drawing.Point(136, 616);
            this.lbl_dia_semana.Name = "lbl_dia_semana";
            this.lbl_dia_semana.Size = new System.Drawing.Size(141, 32);
            this.lbl_dia_semana.TabIndex = 450;
            this.lbl_dia_semana.Text = "Quinta-Feira";
            // 
            // lbl_data
            // 
            this.lbl_data.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_data.AutoSize = true;
            this.lbl_data.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_data.Font = new System.Drawing.Font("Segoe UI Light", 17.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_data.ForeColor = System.Drawing.Color.Black;
            this.lbl_data.Location = new System.Drawing.Point(136, 584);
            this.lbl_data.Name = "lbl_data";
            this.lbl_data.Size = new System.Drawing.Size(69, 32);
            this.lbl_data.TabIndex = 449;
            this.lbl_data.Text = "16/05";
            // 
            // lbl_hora
            // 
            this.lbl_hora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_hora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_hora.Font = new System.Drawing.Font("Segoe UI Light", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hora.ForeColor = System.Drawing.Color.Black;
            this.lbl_hora.Location = new System.Drawing.Point(12, 584);
            this.lbl_hora.Name = "lbl_hora";
            this.lbl_hora.Size = new System.Drawing.Size(134, 62);
            this.lbl_hora.TabIndex = 448;
            this.lbl_hora.Text = "00:43";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.BackgroundImage = global::Zenfox_Software.Properties.Resources.profit__2_;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button5.Location = new System.Drawing.Point(401, 492);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(115, 43);
            this.button5.TabIndex = 452;
            this.button5.Text = "Fechar Caixa";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.BackgroundImage = global::Zenfox_Software.Properties.Resources.receipt__1_;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(11, 539);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 43);
            this.button3.TabIndex = 451;
            this.button3.Text = "Painel de Crediário";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.BackgroundImage = global::Zenfox_Software.Properties.Resources.analytics;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(11, 492);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(134, 43);
            this.button4.TabIndex = 447;
            this.button4.Text = "Painel de Vendas";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_minimizar
            // 
            this.btn_minimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_minimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_minimizar.ForeColor = System.Drawing.Color.White;
            this.btn_minimizar.Image = global::Zenfox_Software.Properties.Resources._001_minimize;
            this.btn_minimizar.Location = new System.Drawing.Point(990, 10);
            this.btn_minimizar.Margin = new System.Windows.Forms.Padding(2);
            this.btn_minimizar.Name = "btn_minimizar";
            this.btn_minimizar.Size = new System.Drawing.Size(27, 29);
            this.btn_minimizar.TabIndex = 440;
            this.btn_minimizar.UseVisualStyleBackColor = true;
            this.btn_minimizar.Click += new System.EventHandler(this.btn_minimizar_Click);
            // 
            // btn_fechar
            // 
            this.btn_fechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_fechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fechar.ForeColor = System.Drawing.Color.White;
            this.btn_fechar.Image = global::Zenfox_Software.Properties.Resources._002_error1;
            this.btn_fechar.Location = new System.Drawing.Point(1021, 11);
            this.btn_fechar.Margin = new System.Windows.Forms.Padding(2);
            this.btn_fechar.Name = "btn_fechar";
            this.btn_fechar.Size = new System.Drawing.Size(27, 29);
            this.btn_fechar.TabIndex = 441;
            this.btn_fechar.UseVisualStyleBackColor = true;
            this.btn_fechar.Click += new System.EventHandler(this.btn_fechar_Click);
            // 
            // btn_balanca
            // 
            this.btn_balanca.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_balanca.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_balanca.BackgroundImage")));
            this.btn_balanca.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_balanca.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_balanca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_balanca.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btn_balanca.Location = new System.Drawing.Point(149, 539);
            this.btn_balanca.Margin = new System.Windows.Forms.Padding(2);
            this.btn_balanca.Name = "btn_balanca";
            this.btn_balanca.Size = new System.Drawing.Size(111, 43);
            this.btn_balanca.TabIndex = 453;
            this.btn_balanca.Text = "Balança";
            this.btn_balanca.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_balanca.UseVisualStyleBackColor = true;
            this.btn_balanca.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button9.BackgroundImage")));
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button9.Location = new System.Drawing.Point(149, 492);
            this.button9.Margin = new System.Windows.Forms.Padding(2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(111, 43);
            this.button9.TabIndex = 454;
            this.button9.Text = "Incluir CPF";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click_1);
            // 
            // lbl_cpf
            // 
            this.lbl_cpf.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lbl_cpf.Location = new System.Drawing.Point(15, 128);
            this.lbl_cpf.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_cpf.Name = "lbl_cpf";
            this.lbl_cpf.Size = new System.Drawing.Size(498, 44);
            this.lbl_cpf.TabIndex = 455;
            this.lbl_cpf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button10.BackgroundImage")));
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button10.Location = new System.Drawing.Point(401, 539);
            this.button10.Margin = new System.Windows.Forms.Padding(2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(115, 43);
            this.button10.TabIndex = 456;
            this.button10.Text = "Configurações";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click_1);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Caixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1059, 655);
            this.ControlBox = false;
            this.Controls.Add(this.button10);
            this.Controls.Add(this.lbl_cpf);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.btn_balanca);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbl_dia_semana);
            this.Controls.Add(this.lbl_data);
            this.Controls.Add(this.lbl_hora);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btn_minimizar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_fechar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_cabecalho_caixa);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Caixa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Caixa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Caixa_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Caixa_KeyUp);
            this.panel1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_venda)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_cabecalho_caixa;
        private System.Windows.Forms.Button btn_fechar;
        private System.Windows.Forms.Button btn_minimizar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dg_venda;
        private System.Windows.Forms.Label txtTotalFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_codigo_barras;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txt_quantidade;
        private System.Windows.Forms.Label lbl_nome_cliente;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txt_descricao_produto;
        private System.Windows.Forms.Button btn_logoff;
        private System.Windows.Forms.Label lbl_nome_operador;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer verifica_focus;
        private System.Windows.Forms.Button btn_delete_venda;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lbl_dia_semana;
        private System.Windows.Forms.Label lbl_data;
        private System.Windows.Forms.Label lbl_hora;
        private System.Windows.Forms.TextBox txt_valor_produto;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btn_balanca;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label lbl_cpf;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Timer timer1;
    }
}