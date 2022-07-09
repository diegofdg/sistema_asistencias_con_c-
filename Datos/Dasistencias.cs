using SistemaAsistencias.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaAsistencias.Datos
{
	public class Dasistencias
	{
		public void BuscarAsistenciasId(ref DataTable dt, int Idpersonal)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlDataAdapter da = new SqlDataAdapter("BuscarAsistenciasId", CONEXIONMAESTRA.conectar);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@Idpersonal", Idpersonal);
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

		public bool InsertarAsistencias(Lasistencias parametros)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("InsertarAsistencias", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
				cmd.Parameters.AddWithValue("@Fecha_entrada", parametros.Fecha_entrada);
				cmd.Parameters.AddWithValue("@Fecha_salida", parametros.Fecha_salida);
				cmd.Parameters.AddWithValue("@Estado", parametros.Estado);
				cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
				cmd.Parameters.AddWithValue("@Observacion", parametros.Observacion);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
				return false;
			}
			finally
			{
				CONEXIONMAESTRA.cerrar();
			}
		}

		public bool ConfirmarSalida(Lasistencias parametros)
		{
			try
			{
				CONEXIONMAESTRA.abrir();

				SqlCommand cmd = new SqlCommand("ConfirmarSalida", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
				cmd.Parameters.AddWithValue("@Fecha_salida", parametros.Fecha_salida);
				cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
				return false;
			}
			finally
			{
				CONEXIONMAESTRA.cerrar();
			}
		}

		public void MostrarAsistenciasDiarias(ref DataTable dt, DateTime desde, DateTime hasta, int semana)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlDataAdapter da = new SqlDataAdapter("MostrarAsistenciasDiarias", CONEXIONMAESTRA.conectar);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@desde", desde);
				da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
				da.SelectCommand.Parameters.AddWithValue("@semana", semana);
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