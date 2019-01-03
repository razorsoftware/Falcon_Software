namespace Zenfox_Software.Suporte
{
    partial class Ticket_Detalhamento
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
            this.conversa = new System.Windows.Forms.RichTextBox();
            this.btn_enviar = new System.Windows.Forms.Button();
            this.mensagem = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // conversa
            // 
            this.conversa.BackColor = System.Drawing.Color.White;
            this.conversa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.conversa.BulletIndent = 3;
            this.conversa.Location = new System.Drawing.Point(12, 12);
            this.conversa.Name = "conversa";
            this.conversa.ReadOnly = true;
            this.conversa.Size = new System.Drawing.Size(629, 451);
            this.conversa.TabIndex = 5;
            this.conversa.Text = "";
            // 
            // btn_enviar
            // 
            this.btn_enviar.Location = new System.Drawing.Point(543, 469);
            this.btn_enviar.Name = "btn_enviar";
            this.btn_enviar.Size = new System.Drawing.Size(98, 27);
            this.btn_enviar.TabIndex = 2;
            this.btn_enviar.Text = "Enviar";
            this.btn_enviar.UseVisualStyleBackColor = true;
            this.btn_enviar.Click += new System.EventHandler(this.btn_enviar_Click);
            // 
            // mensagem
            // 
            this.mensagem.Location = new System.Drawing.Point(12, 473);
            this.mensagem.Name = "mensagem";
            this.mensagem.Size = new System.Drawing.Size(525, 20);
            this.mensagem.TabIndex = 0;
            this.mensagem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mensagem_KeyPress);
            this.mensagem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mensagem_KeyUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Ticket_Detalhamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 505);
            this.Controls.Add(this.mensagem);
            this.Controls.Add(this.btn_enviar);
            this.Controls.Add(this.conversa);
            this.Name = "Ticket_Detalhamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Suporte";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox conversa;
        private System.Windows.Forms.Button btn_enviar;
        private System.Windows.Forms.TextBox mensagem;
        private System.Windows.Forms.Timer timer1;
    }
}