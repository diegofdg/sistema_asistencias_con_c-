﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaAsistencias.Presentacion.AsistenteInstalacion
{
    public partial class EleccionServidor : Form
    {
        public EleccionServidor()
        {
            InitializeComponent();
        }

        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            Dispose();
            InstalacionBd frm = new InstalacionBd();
            frm.ShowDialog();
        }

        private void BtnRemoto_Click(object sender, EventArgs e)
        {
            Dispose();
            ConexionRemota frm = new ConexionRemota();
            frm.ShowDialog();
        }
    }
}