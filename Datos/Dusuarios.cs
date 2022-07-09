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
	public class Dusuarios
	{
		public bool InsertarUsuario(Lusuarios parametros)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("InsertarUsuario", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@nombres", parametros.Nombre);
				cmd.Parameters.AddWithValue("@Login", parametros.Login);
				cmd.Parameters.AddWithValue("@Password", parametros.Password);
				cmd.Parameters.AddWithValue("@Icono", parametros.Icono);
				cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
			finally
			{
				CONEXIONMAESTRA.cerrar();
			}

		}

		public void MostrarUsuarios(ref DataTable dt)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlDataAdapter da = new SqlDataAdapter("Select * from Usuarios", CONEXIONMAESTRA.conectar);
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

		public void ObtenerIdUsuario(ref int Idusuario, string Login)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("ObtenerIdUsuario", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Login", Login);
				Idusuario = Convert.ToInt32(cmd.ExecuteScalar());
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

		public bool EliminarUsuario(Lusuarios parametros)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("EliminarUsuario", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
				cmd.Parameters.AddWithValue("@login", parametros.Login);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
			finally
			{
				CONEXIONMAESTRA.cerrar();
			}
		}

		public bool RestaurarUsuario(Lusuarios parametros)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("RestaurarUsuario", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
			finally
			{
				CONEXIONMAESTRA.cerrar();
			}
		}

		public bool EditarUsuario(Lusuarios parametros)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("EditarUsuario", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
				cmd.Parameters.AddWithValue("@nombres", parametros.Nombre);
				cmd.Parameters.AddWithValue("@Login", parametros.Login);
				cmd.Parameters.AddWithValue("@Password", parametros.Password);
				cmd.Parameters.AddWithValue("@Icono", parametros.Icono);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
			finally
			{
				CONEXIONMAESTRA.cerrar();
			}
		}

		public void BuscarUsuarios(ref DataTable dt, string buscador)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlDataAdapter da = new SqlDataAdapter("Buscarusuarios", CONEXIONMAESTRA.conectar);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
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

		public void VerificarUsuario(ref string Indicador)
		{
			try
			{
				int Iduser;
				CONEXIONMAESTRA.abrir();
				SqlCommand da = new SqlCommand("Select idUsuario From Usuarios", CONEXIONMAESTRA.conectar);
				Iduser = Convert.ToInt32(da.ExecuteScalar());
				CONEXIONMAESTRA.cerrar();
				Indicador = "Correcto";
			}
			catch (Exception)
			{
				Indicador = "Incorrecto";
			}
		}

		public void ValidarUsuario(Lusuarios parametros, ref int id)
		{
			try
			{
				CONEXIONMAESTRA.abrir();
				SqlCommand cmd = new SqlCommand("ValidarUsuario", CONEXIONMAESTRA.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@password", parametros.Password);
				cmd.Parameters.AddWithValue("@login", parametros.Login);
				id = Convert.ToInt32(cmd.ExecuteScalar());
			}
			catch (Exception ex)
			{
				id = 0;
			}
			finally
			{
				CONEXIONMAESTRA.cerrar();
			}
		}
	}
}