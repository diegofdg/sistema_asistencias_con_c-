using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaAsistencias.Datos
{
    public class Dmodulos
    {
		public void mostrar_Modulos(ref DataTable dt)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlDataAdapter da = new SqlDataAdapter("select * from Modulos", CONEXIONMAESTRA.conectar);
				da.Fill(dt);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);

			}
			finally
			{
				CONEXIONMAESTRA.cerrar();
			}

		}
	}
}
