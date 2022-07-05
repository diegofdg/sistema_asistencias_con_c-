using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaAsistencias.Logica;

namespace SistemaAsistencias.Datos
{
    public class Dpersonal
    {
        public bool InsertarPersonal(Lpersonal parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("InsertarPersonal", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@Pais", parametros.Pais);
                cmd.Parameters.AddWithValue("@Id_cargo", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
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

        public bool EditarPersonal(Lpersonal parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("EditarPersonal", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@Pais", parametros.Pais);
                cmd.Parameters.AddWithValue("@Id_cargo", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
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

        public bool EliminarPersonal(Lpersonal parametros)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("EliminarPersonal", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
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
    }
}
