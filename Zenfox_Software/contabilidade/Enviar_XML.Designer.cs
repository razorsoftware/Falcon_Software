namespace Zenfox_Software.contabilidade
{
    partial class Enviar_XML
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.lbl_email = new System.Windows.Forms.Label();
            this.lbl_progresso = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_processar = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_total_vendas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(618, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enviar fechamento de xml\'s ao Contador";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_email
            // 
            this.txt_email.Location = new System.Drawing.Point(15, 186);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(614, 20);
            this.txt_email.TabIndex = 1;
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.Location = new System.Drawing.Point(12, 170);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(93, 13);
            this.lbl_email.TabIndex = 2;
            this.lbl_email.Text = "Email do Contador";
            // 
            // lbl_progresso
            // 
            this.lbl_progresso.Location = new System.Drawing.Point(16, 42);
            this.lbl_progresso.Name = "lbl_progresso";
            this.lbl_progresso.Size = new System.Drawing.Size(614, 43);
            this.lbl_progresso.TabIndex = 3;
            this.lbl_progresso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_progresso.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 183);
            this.progressBar1.MarqueeAnimationSpeed = 5000;
            this.progressBar1.Maximum = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(614, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // btn_processar
            // 
            this.btn_processar.Location = new System.Drawing.Point(263, 233);
            this.btn_processar.Name = "btn_processar";
            this.btn_processar.Size = new System.Drawing.Size(122, 23);
            this.btn_processar.TabIndex = 5;
            this.btn_processar.Text = "Processar";
            this.btn_processar.UseVisualStyleBackColor = true;
            this.btn_processar.Click += new System.EventHandler(this.btn_processar_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_total_vendas
            // 
            this.lbl_total_vendas.AutoSize = true;
            this.lbl_total_vendas.Location = new System.Drawing.Point(140, 89);
            this.lbl_total_vendas.Name = "lbl_total_vendas";
            this.lbl_total_vendas.Size = new System.Drawing.Size(45, 13);
            this.lbl_total_vendas.TabIndex = 6;
            this.lbl_total_vendas.Text = "R$ 0,00";
            // 
            // Enviar_XML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 268);
            this.Controls.Add(this.lbl_total_vendas);
            this.Controls.Add(this.btn_processar);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lbl_progresso);
            this.Controls.Add(this.lbl_email);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.label1);
            this.Name = "Enviar_XML";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enviar_XML";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.Label lbl_progresso;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btn_processar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_total_vendas;
    }
}