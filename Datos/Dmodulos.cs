using SistemaAsistencias.Logica;
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
		public void MostrarModulos(ref DataTable dt)
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

		public bool InsertarModulos(Lmodulos parametros)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("InsertarModulos", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Modulo", parametros.Modulo);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return true;
			}
			finally
			{
				CONEXIONMAESTRA.cerrar();
			}
		}
	}
}
