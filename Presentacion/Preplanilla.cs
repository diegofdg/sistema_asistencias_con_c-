using SistemaAsistencias.Datos;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SistemaAsistencias.Presentacion
{
    public partial class Preplanilla : UserControl
    {
        public Preplanilla()
        {
            InitializeComponent();
        }

        private void Preplanilla_Load(object sender, EventArgs e)
        {
            calcular_numero_de_semana();
        }

        private void ReporteAsistencias()
        {
            Reportes.ReportAsistencias rpt = new Reportes.ReportAsistencias();
            DataTable dt = new DataTable();
            Dasistencias funcion = new Dasistencias();
            funcion.MostrarAsistenciasDiarias(ref dt, txtdesde.Value, txthasta.Value, Convert.ToInt32(lblnumerosemana.Text));
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;

            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }

        private void txtdesde_ValueChanged(object sender, EventArgs e)
        {
            calcular_numero_de_semana();
            ReporteAsistencias();
        }

        private void calcular_numero_de_semana()
        {
            DateTime v2 = txthasta.Value;
            lblnumerosemana.Text = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(v2, CalendarWeekRule.FirstDay, v2.DayOfWeek).ToString();
        }

        private void txthasta_ValueChanged(object sender, EventArgs e)
        {
            calcular_numero_de_semana();
            ReporteAsistencias();
        }
    }
}