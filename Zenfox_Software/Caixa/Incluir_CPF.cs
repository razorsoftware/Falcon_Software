﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenfox_Software.caixa
{
    public partial class Incluir_CPF : Form
    {
        public Incluir_CPF()
        {
            InitializeComponent();
        }

        private void Incluir_CPF_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            this.Close();
        }

        private void txt_cpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txt_cpf_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Close();
        }
    }
}
