using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SistemaAsistencias
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Presentacion.Login frm = new Presentacion.Login();
            frm.FormClosed += Frm_FormClosed;
            frm.ShowDialog();
            Application.Run();            
            //Application.Run(new Presentacion.TomarAsistencia());
        }

        private static void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }
    }
}